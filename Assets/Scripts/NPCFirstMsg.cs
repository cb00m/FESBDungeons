using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFirstMsg : Collidable
{
    private float cooldown = 5f;
    private float lastShown = -5f;


    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            Debug.Log(lastShown + " lastshown");
            Debug.Log(Time.time - lastShown + " time");
            if (Time.time - lastShown > cooldown)
            {
                lastShown = Time.time;
                GameManager.instance.ShowText("Hey you, come here!", 30, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.up * 30, 2f);
            }
        }

    }
}
