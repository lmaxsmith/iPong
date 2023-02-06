using System;
using System.Collections;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Base;
using UnityEngine;
using UnityEngine.Serialization;

public class Paddle : ArgyleComponent
{

    public Rigidbody2D _rb;
    public float _speed = 1;
    public float _angleMultiplier;
    private Ball _ball;

    protected Ball Ball
    {
	    get
	    {
		    if (_ball == null)
			    _ball = FindObjectOfType<Ball>();

		    return _ball;
	    }
    }

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

    #region ==== Monobehavior ====------------------

    private void Update()
    {
	    
    }

    #endregion -----------------/Monobehavior ====
    
    public void SetPaddleMovement(int direction)
    {
	    _rb.velocity = new Vector2(direction * _speed, 0f);
    }

    public float GetAngleMod()
    {
	    var mod = _angleMultiplier * (Ball.TForm.position.x - TForm.position.x) / TForm.localScale.x;
	    return mod;
    }
}
