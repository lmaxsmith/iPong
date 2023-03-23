using System.Collections;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Support;
using Unity.Barracuda;
using UnityEngine;

public class PlayData
{
	//input data
	[Export] public float MyPaddlePosition;
	[Export] public float MyPaddleTimeAsIs;
	[Export] public float OpponentPaddle;
	[Export] public float OpponentVelocity;
	[Export] public float BallPositionX;
	[Export] public float BallPositionY;
	[Export] public float BallVelocityX;
	[Export] public float BallVelocityY;
	
	//output data probability
	[Export] public float IsPaddleStart; //probability
	[Export] public float IsPaddleStop; //probability
	[Export] public float IsPaddleContinuing; //probability
	//output data direction classificaiton
	[Export] public float IsPaddleLeft; //probability
	[Export] public float IsPaddleRight; //probability
	
	//support trackers
	private float myPaddleLastVelocity = 0;

	public PlayData(Paddle p1, Paddle p2, float timeAsIs)
	{
		//paddle model input
		MyPaddlePosition = p1.TForm.localPosition.x;

		MyPaddleTimeAsIs = timeAsIs; 
		OpponentPaddle = p2.TForm.localPosition.x;
		OpponentVelocity = p1.TForm.InverseTransformDirection(p2._rb.velocity).x;

		//ball model input
		Ball ball = GameObject.FindObjectOfType<Ball>();
		BallPositionX = ball ? p1.TForm.InverseTransformPoint(ball.TForm.position).x : 0;
		BallPositionY = ball ? p1.TForm.InverseTransformPoint(ball.TForm.position).y : 0;
		BallVelocityX = ball ? p1.TForm.InverseTransformDirection(ball._rb.velocity).x : 0;
		BallVelocityY = ball ? p1.TForm.InverseTransformDirection(ball._rb.velocity).y : 0;
		
		//paddle model output
		IsPaddleStart = timeAsIs == 0 && p1.TForm.InverseTransformDirection(p1._rb.velocity).x != 0 ? 1 : 0;
		IsPaddleStop = timeAsIs == 0 && p1.TForm.InverseTransformDirection(p1._rb.velocity).x == 0 ? 1 : 0;
		IsPaddleContinuing = timeAsIs > 0 ? 1 : 0;
		IsPaddleLeft = p1.TForm.InverseTransformDirection(p1._rb.velocity).x < 0 ? 1 : 0;
		IsPaddleRight = p1.TForm.InverseTransformDirection(p1._rb.velocity).x > 0 ? 1 : 0;
		
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
