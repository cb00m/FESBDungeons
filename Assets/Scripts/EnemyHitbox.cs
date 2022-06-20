using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage = 1;
    public float knockback = 5;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name == "Player")
        {
            //Create damage
            Damage dmg = new Damage()
            {
                damageAmount = damage,
                origin = transform.position,
                knockback = knockback
            };

            coll.SendMessage("ReceiveDamage", dmg);
            GameManager.instance.CheckHealt();
        }
    }
}
