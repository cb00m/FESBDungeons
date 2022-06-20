using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    private int healingPower = 1;
    private float lastHeal;
    private float cooldown = 0.5f;


    protected override void Start()
    {
        base.Start();
        lastHeal = -cooldown;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player") return;

        base.OnCollide(coll);

        if(Time.time - lastHeal > cooldown)
        {
            lastHeal = Time.time;
            bool print = GameManager.instance.player.getHealth() < GameManager.instance.maxHealth;
            GameManager.instance.player.Heal(healingPower, print);
        }
    }
}
