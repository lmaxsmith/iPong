using System.Collections;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Base;
using UnityEngine;

public class PlayerInput : ArgyleComponent
{
    public int score;
    public Paddle _paddle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
            _paddle.SetPaddleMovement(-1);
        else if (Input.GetKey(KeyCode.RightArrow))
            _paddle.SetPaddleMovement(1);
        else 
            _paddle.SetPaddleMovement(0);
    }
}
