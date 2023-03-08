using System.Collections;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Support;
using Unity.Barracuda;
using UnityEngine;

public class PlayData
{
	[Export] public float MyPaddlePosition;
	[Export] public float MyPaddleIsRight;
	[Export] public float MyPaddleIsLeft;
	[Export] public float MyPaddleIsStill;
	[Export] public float MyPaddleTimeAsIs;
	[Export] public float OpponentPaddle;
	[Export] public float OpponentVelocity;
	[Export] public float BallPositionX;
	[Export] public float BallPositionY;
	[Export] public float BallVelocityX;
	[Export] public float BallVelocityY;

	/*
	public PlayData(float myPaddle, float myPaddleVelocity, float opponentPaddle, float opponentVelocity, Vector2 ballPosition, Vector2 ballVelocity)
	{
		MyPaddle = myPaddle;
		MyPaddleVelocity = myPaddleVelocity;
		OpponentPaddle = opponentPaddle;
		OpponentVelocity = opponentVelocity;
		BallPosition = ballPosition;
		BallVelocity = ballVelocity;
	}

	public PlayData(float myPaddle, float myPaddleVelocity, float opponentPaddle, float opponentVelocity)
	{
		MyPaddle = myPaddle;
		MyPaddleVelocity = myPaddleVelocity;
		OpponentPaddle = opponentPaddle;
		OpponentVelocity = opponentVelocity;
		BallPosition = Vector2.zero;
		BallVelocity = Vector2.zero;
	}
	*/

	public PlayData(Paddle p1, Paddle p2, float timeAsIs)
	{
		MyPaddlePosition = p1.TForm.localPosition.x;
		MyPaddleIsRight = p1._rb.velocity.x > 0 ? 1 : 0;
		MyPaddleIsLeft = p1._rb.velocity.x < 0 ? 1 : 0;
		MyPaddleIsStill = p1._rb.velocity.x == 0 ? 1 : 0;
		MyPaddleTimeAsIs = timeAsIs; 
		OpponentPaddle = p2.TForm.localPosition.x;
		OpponentVelocity = p2._rb.velocity.x;

		Ball ball = GameObject.FindObjectOfType<Ball>();
		BallPositionX = ball ? ball.TForm.position.x : 0;
		BallPositionY = ball ? ball.TForm.position.y : 0;
		BallVelocityX = ball ? ball._rb.velocity.x : 0;
		BallVelocityY = ball ? ball._rb.velocity.y : 0;
	}

	public Tensor ToTensor()
	{
		return new Tensor(1, 8, new []
		{
			MyPaddlePosition, MyPaddleTimeAsIs, 
			OpponentPaddle, OpponentVelocity,
			BallPositionX, BallPositionY, BallVelocityX, BallVelocityY
		});
	}
	
}
