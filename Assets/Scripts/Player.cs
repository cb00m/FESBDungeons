using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRender;
    private bool alive = true;
    private int health = 20;
    public int level = 0;
    protected override void Start()
    {
        base.Start();
        this.alive = true;
        spriteRender = GetComponent<SpriteRenderer>();
        
        DontDestroyOnLoad(gameObject); //keep player in all screens
    }

    //Movement
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    //Heal an amount given, printing information or not
    public void Heal(int amount, bool showInfo)
    {
        int healed;
        if (health + amount <= GameManager.instance.maxHealth)
        {
            health += amount;
            healed = amount;
        }
        else
        {
            healed = GameManager.instance.maxHealth - health;
            health = 20;
        }
        if(showInfo) //print only if it is a partial heal
            GameManager.instance.ShowText("+" + healed + "hp", 20, Color.green, transform.position, Vector3.up * 30, 1f);
        GameManager.instance.CheckHealt();
    }

    //Hurt an amount given
    public void Hurt(int damage)
    {
        if (health - damage >= GameManager.instance.minhealth)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        GameManager.instance.CheckHealt();
    }

    //LevelUp
    public void LevelUp()
    {
        GameManager.instance.maxHealth += 2;
        Heal(2, false);
        GameManager.instance.CheckHealt();
    }

    public bool isAlive()
    {
        return alive;
    }

    public void Die()
    {
        alive = false;
    }

    public void Revive()
    {
        Heal(GameManager.instance.maxHealth, false);
        alive = true;
    }

    public int getHealth()
    {
        return health;
    }
}
