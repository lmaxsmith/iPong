using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Unity.Barracuda;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
	public class PyTorchControl : AIControl
	{
		public DataManager _dataManager;

		public NNModel nnModel;
		public Model model;
		public IWorker engine;

		protected override void PostStart()
		{
			base.PostStart();
			LoadModel();
		}
		
		public void LoadModel()
		{
			model = ModelLoader.Load(nnModel);
			engine = WorkerFactory.CreateWorker(model);
		}

		

		public List<float> inputArray = new List<float>();
		public List<float> outputArray = new List<float>();
		public int direction = 0;

		protected override async UniTask Control()
		{
			using var input = _dataManager.LastData.ToTensor();
			{
				var inputArr = input.ToReadOnlyArray();
				inputArray = inputArr.ToList();
				Debug.Log($"Model input: {inputArr}");
				var execution = engine.Execute(input);
				
				float[] softmax;
				float[] outputArray;
				using var output = execution.PeekOutput();
				{
					outputArray = output.ToReadOnlyArray();
					softmax = SoftMax(outputArray);
				}
				
				int valueI = SelectByProbability(outputArray);
				if (valueI == 0)
					direction = 1;
				else if (valueI == 1)
					direction = -1;
				else
					direction = 0;
				
				
				_paddle.SetPaddleMovement(direction);
			
				Log($"Model output: {softmax[0]}, {softmax[1]}, {softmax[2]}: direction: {direction}");
			}
			
		}


		private static int SelectByProbability(float[] inputs)
		{
			float[] softmax = SoftMax(inputs);

			float runningTotal = 0;
			float r = Random.Range(0f, 1f);
			for (int i = 0; i < softmax.Length; i++)
			{
				runningTotal += softmax[i];
				if (r < runningTotal)
					return i;
			}

			throw new IndexOutOfRangeException($"Number not found in 1 probability.");
		}
		
		static float[] SoftMax(float[] inputs)
		{
			float[] result = new float[inputs.Length];
			float sum = 0;
			for (int i = 0; i < inputs.Length; i++)
			{
				result[i] = Mathf.Exp(inputs[i]);
				sum += result[i];
			}
			for (int i = 0; i < inputs.Length; i++)
			{
				result[i] /= sum;
			}
			return result;
		}
	}
}