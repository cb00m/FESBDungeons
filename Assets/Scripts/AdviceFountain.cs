using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdviceFountain : Collidable
{

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player") { //only work for player
            GameManager.instance.ShowText("That fountain looks healthy...", 30, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.up * 30, 2f);
            Destroy(gameObject);
        }

    }
}
