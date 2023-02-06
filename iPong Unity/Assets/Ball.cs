using System;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Base;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : ArgyleComponent
{
	public Rigidbody2D _rb;
	public float _verticalSpeed;

	[SerializeField]
	private List<Wall> _walls;

	[SerializeField] 
	private List<Paddle> _paddles;

	private BoxCollider2D col;
	public BoxCollider2D Col
	{
		get
		{
			if (col == null)
				col = GetComponent<BoxCollider2D>();

			return col;
		}
	}

	private Vector2 velocity;
	
	
	protected override void PostStart()
	{
		base.PostStart();
		_rb.velocity = new Vector2(0, -_verticalSpeed);
	}

	private void Update()
	{
		if(_rb.velocity != Vector2.zero)
			velocity = _rb.velocity;
		
		foreach (var p in _paddles)
		{
			if(p.Col.bounds.Contains(Col.bounds.center))
			{
				Log($"Hit the paddle");
				_rb.velocity = new Vector2(_rb.velocity.x * p.GetAngleMod(), -_rb.velocity.y);
			}
		}

		foreach (var w in _walls)
		{
			if (w.Col.bounds.Contains(Col.bounds.center))
			{
				Log("Hit a wall!");
				_rb.velocity = new Vector2(-_rb.velocity.x, _rb.velocity.y);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		Log("Collided! 2D!");
		if(col.collider.GetComponent<Wall>() )
			_rb.velocity = new Vector2(-velocity.x, velocity.y);
		else
		{
			var p = col.collider.GetComponent<Paddle>();
			_rb.velocity = new Vector2(velocity.x + p.GetAngleMod(), -velocity.y * 1.05f);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		var goal = col.GetComponent<Goal>();
		if(goal)
		{
			goal.MarkPoint();
			Instantiate(GO, Vector2.zero, Quaternion.identity);
			Destroy(GO);
		}		
	}
}
