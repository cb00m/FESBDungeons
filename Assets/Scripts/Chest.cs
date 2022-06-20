using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coins = 5; //coins granted, by default 5

    protected override void OnCollect()
    {
        if (!collected)
        {
            base.OnCollect();
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += coins;
            GameManager.instance.ShowText("+" + coins + " coins", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
            GameManager.instance.TryUpgradeWeapon();
        }
        
        
    }
}
