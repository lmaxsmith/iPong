using System;
using Argyle.UnclesToolkit.Base;
using Unity.VisualScripting;

namespace DefaultNamespace
{

	public class SimpleOpponent : ArgyleComponent
	{

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
		
		private void Update()
		{
			if (Ball)
			{
				if(Ball.TForm.position.x < _paddle.TForm.position.x)
					_paddle.SetPaddleMovement(-1);
				else if(Ball.TForm.position.x > _paddle.TForm.position.x)
					_paddle.SetPaddleMovement(1);
				else
					_paddle.SetPaddleMovement(0);
			}
		}
	}
}