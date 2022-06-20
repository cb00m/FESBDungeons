using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    private float lapsus = 4.0f;
    private float last;
    private bool firstTime = true;
    protected override void Start()
    {
        base.Start();
        last = -lapsus;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (firstTime)
        {
            Destroy(GameObject.Find("NPCFirstMsg"));
            firstTime = false;
        }

        if (Time.time - last > lapsus)
        {
            last = Time.time;
            GameManager.instance.ShowText("Watch out there, swing that thing with a nice SPACE", 30, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.up * 10, 3f);
        }
    }
}
