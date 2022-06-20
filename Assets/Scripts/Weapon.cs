using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int[] damagePoint = {100, 2 ,3 ,4 ,5 ,6 ,7};
    public float[] knockback = { 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3f, 3.2f };

    //upgrade
    public int level = 0;
    private SpriteRenderer spriteRender;

    private float cooldown = 0.5f;
    private float lastSwing;

    //Animation
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        spriteRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    public void UpgradeWeapon()
    {
        level++;
        spriteRender.sprite = GameManager.instance.weaponSprites[level];
    }

    protected override void OnCollide(Collider2D coll)
    {
        
        if(coll.tag == "Fighter")
        {
            if (coll.name == "Player") return;

            //Create damage
            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[level],
                origin = transform.position,
                knockback = knockback[level]
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }

    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void Reset()
    {
        level = 0;
        spriteRender.sprite = GameManager.instance.weaponSprites[level];
    }
}
