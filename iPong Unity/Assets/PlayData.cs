using System.Collections;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Support;
using UnityEngine;

public class PlayData
{
	[Export] public float MyPaddle;
	[Export] public float MyPaddleVelocity;
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

	public PlayData(Paddle p1, Paddle p2)
	{
		MyPaddle = p1.TForm.localPosition.x;
		MyPaddleVelocity = p1._rb.velocity.x;
		OpponentPaddle = p2.TForm.localPosition.x;
		OpponentVelocity = p2._rb.velocity.x;

		Ball ball = GameObject.FindObjectOfType<Ball>();
		BallPositionX = ball ? ball.TForm.position.x : 0;
		BallPositionY = ball ? ball.TForm.position.y : 0;
		BallVelocityX = ball ? ball._rb.velocity.x : 0;
		BallPositionY = ball ? ball._rb.velocity.y : 0;
	}
}
