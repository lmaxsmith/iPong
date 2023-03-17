using System.IO;
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

		[Button]
		public void LoadModel()
		{
			model = ModelLoader.Load(nnModel);
			engine = WorkerFactory.CreateWorker(model);


		}

		[Button]
		public void RunModel()
		{
			var input = _dataManager.LastData.ToTensor();
			var output = engine.Execute(input).PeekOutput();
			
			Debug.Log($"Model Output: {output}");
		}

		protected override async UniTask Control()
		{
			using var input = _dataManager.LastData.ToTensor();
			{
				var inputArr = input.ToReadOnlyArray();
				Debug.Log($"Model input: {inputArr}");
				var execution = engine.Execute(input);
				var output = execution.PeekOutput();
				var arr = output.ToReadOnlyArray();
				
				int direction;
				
				
				if (arr[0] > arr[1] && arr[0] > arr[2])
					direction = 1;
				else if (arr[1] > arr[0] && arr[1] > arr[2])
					direction = -1;
				else
					direction = 0;
				
				
				
				_paddle.SetPaddleMovement(direction = 0);
			
				Log($"Model output: {arr[0]}, {arr[1]}, {arr[2]}: direction: {direction}");
			}
			
		}
	}
}