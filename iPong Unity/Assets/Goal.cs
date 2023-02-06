using System.Collections;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Base;
using TMPro;
using UnityEngine;

public class Goal : ArgyleComponent
{
    //for collision
    public PlayerInput Player;
    public TMP_Text ScoreText;

    protected override void PostStart()
    {
        base.PostStart();
        ScoreText.text = 0.ToString();
    }

    public void MarkPoint()
    {
        Player.score++;
        ScoreText.text = Player.score.ToString();
    }
}
