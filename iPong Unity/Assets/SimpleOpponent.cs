using System;
using Argyle.UnclesToolkit.Base;
using Argyle.UnclesToolkit.Jobs;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{

	public class SimpleOpponent : ArgyleComponent
	{

		#region ==== Settings ====------------------

		private Ball _ball;

		private Ball Ball
		{
			get
			{
				if (!_ball)
					_ball = FindObjectOfType<Ball>();

				return _ball;
			}
		}

		public Paddle _paddle;
		public float threshold = .1f;
		
		
		#region == name ==----

		public float _loopDuration = .5f;

		private Looper controllLoop = new Looper("SimpleAIControlLoop", 1);

		#endregion ----/name ==

		#endregion -----------------/Settings ====


		#region ==== Monobehavior ====------------------

		protected override void PostStart()
		{
			base.PostStart();
			controllLoop.LoopDuration = _loopDuration;
			controllLoop.Add(Control);
		}

		private void OnEnable()
		{
			controllLoop.StartLoop();
		}

		private void OnDisable()
		{
			controllLoop.StopLoop();
		}

		#endregion -----------------/Monobehavior ====
		
		
		private async UniTask Control()
		{
			if (Ball)
			{
				if (IsOutOfThreshold())
				{
					if(isLeft())
					{
						_paddle.SetPaddleMovement(-1);
						while (!isRight())
							await UniTask.Delay(100);
					}					
					else if (isRight())
					{
						_paddle.SetPaddleMovement(1);
						while (!isLeft())
							await UniTask.Delay(100);
					}			
					
					_paddle.SetPaddleMovement(0);
				}
			}
		}

		public bool IsOutOfThreshold() => Mathf.Abs(Ball.TForm.position.x - _paddle.TForm.position.x) > threshold;
		public bool isLeft() => Ball.TForm.position.x < _paddle.TForm.position.x - threshold;
		public bool isRight() => Ball.TForm.position.x > _paddle.TForm.position.x + threshold;
	}
}