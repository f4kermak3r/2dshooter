using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLATFORMER.Combat;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float moveSpeed;
    public float chaseRange = 5f;

    Rigidbody2D rb2d;
    public Animator animator;
    Health health;
    Weapon weapon;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < chaseRange && !health.IsDead() && weapon.GetRange() < distToPlayer)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
        // else
        // {
        //     StopChasingPlayer();
        // }


    }

    private void AttackPlayer()
    {
        animator.SetTrigger("isPunching");
    }

    private void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        animator.SetFloat("speed", 0);
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(4, 4);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        animator.SetFloat("speed", Mathf.Abs(moveSpeed));
    }
}