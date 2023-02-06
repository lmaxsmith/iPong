using System;
using Argyle.UnclesToolkit.Base;
using TMPro;

namespace DefaultNamespace
{
	public class ScoreKeeper : ArgyleComponent
	{
		public TMP_Text player1Text;
		public TMP_Text player2Text;

		public PlayerInput player1;
		public PlayerInput player2;
		
		private void Update()
		{
			player1Text.text = player1.score.ToString();
			player2Text.text = player2.score.ToString();
		}
	}
}