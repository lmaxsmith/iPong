using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Unity.Barracuda;
using UnityEngine;

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
				using var output = execution.PeekOutput();
				{
					var arr = output.ToReadOnlyArray();
					softmax = SoftMax(arr);
				}
				outputArray = softmax.ToList();

				if (softmax[0] > softmax[1] && softmax[0] > softmax[2])
					direction = 1;
				else if (softmax[1] > softmax[0] && softmax[1] > softmax[2])
					direction = -1;
				else
					direction = 0;
				
				_paddle.SetPaddleMovement(direction);
			
				Log($"Model output: {softmax[0]}, {softmax[1]}, {softmax[2]}: direction: {direction}");
			}
			
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