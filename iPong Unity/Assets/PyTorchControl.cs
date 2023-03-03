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
				Debug.Log($"Model input: {input}");
				var execution = engine.Execute(input);
				var output = execution.PeekOutput();

				int move = Mathf.RoundToInt(Mathf.Clamp(output[0] * 4, -1, 1));
				_paddle.SetPaddleMovement(move);
			
				Log($"Model output: {output[0]}");
			}
			
		}
	}
}