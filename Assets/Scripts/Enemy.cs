using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // xp
    public int xpValue = 1;

    //AI
    public float triggerLength = 0.7f;
    public float chaseLength = 1;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;


    // hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        ySpeed = 0.35f;
        xSpeed = 0.5f;

    }

    protected void FixedUpdate()
    {
        if (GameManager.instance.player.isAlive())
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        collidingWithPlayer = false;

        //Collidable
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
                collidingWithPlayer = true;

            hits[i] = null;
        }

    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.xp += xpValue;
        GameManager.instance.ShowText("+xp", 15, Color.magenta, transform.position, Vector3.up * 30, 0.5f);
    }
}
