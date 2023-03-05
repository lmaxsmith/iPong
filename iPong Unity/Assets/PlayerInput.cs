using System.Collections;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Base;
using UnityEngine;

public class PlayerInput : ArgyleComponent
{
    public int score;
    public Paddle _paddle;
    
    public KeyCode leftKey;
    public KeyCode rightKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(leftKey))
            _paddle.SetPaddleMovement(-1);
        else if (Input.GetKey(rightKey))
            _paddle.SetPaddleMovement(1);
        else 
            _paddle.SetPaddleMovement(0);
    }
}
