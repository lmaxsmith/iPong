using Argyle.UnclesToolkit.Base;
using Argyle.UnclesToolkit.Jobs;
using Cysharp.Threading.Tasks;

namespace DefaultNamespace
{
	public class AIControl : ArgyleComponent
	{
		#region ==== Settings ====------------------

		private Ball _ball;

		protected Ball Ball
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
		
		
		#region == Loops ==----

		public float _loopDuration = .5f;

		private Looper controllLoop = new Looper("SimpleAIControlLoop", 1);

		#endregion ----/Loops ==

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

		protected virtual async UniTask Control()
		{
			//override please
		}

	}
}