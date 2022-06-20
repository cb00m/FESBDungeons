using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Start()
    {
        base.Start();
        ySpeed = 0.4f; //change speed variables
        xSpeed = 0.6f;
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.winScreen.Setup(); //launch win screen
    }
}
