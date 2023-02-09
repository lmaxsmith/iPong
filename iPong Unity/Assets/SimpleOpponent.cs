using System;
using Argyle.UnclesToolkit.Base;
using Argyle.UnclesToolkit.Jobs;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{

	public class SimpleOpponent : AIControl
	{
		protected override async UniTask Control()
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