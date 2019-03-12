using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Vector3 enemyDirection;
    Vector3 enemyPosition;
    Vector3 playerPosition;
    bool detected = false;


    void Start()
    {
        base.Start();
    }

     void Update()
    {
        MoveTowardsPlayer();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Detected");
        if (collision.gameObject.tag == "Player")
        {
            detected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Escaped");
            detected = false;
            anim.ResetTrigger("Run");
            anim.SetTrigger("Idle");
        }
    }


    protected void MoveTowardsPlayer()
    {
        if (detected)
        {
            enemyPosition = transform.position;
            enemyDirection = transform.localScale;
            playerPosition = GameObject.Find("Player(Clone)").transform.position;
            CheckIfFacingRightDirection();
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Attack");

            if (Vector3.Distance(enemyPosition, playerPosition) > 4f)
            {
                anim.ResetTrigger("Attack");
                transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, speed / 20);
                anim.SetTrigger("Run");
            }
            else if (Vector3.Distance(enemyPosition, playerPosition) <= 4f)
            {
                anim.ResetTrigger("Run");
                anim.SetTrigger("Attack");
            }
        }
    }

    private void CheckIfFacingRightDirection()
    {
        if (transform.position.x > playerPosition.x && !facingLeft)
        {
            FlipAsset();
        }
        else if (transform.position.x < playerPosition.x && facingLeft)
        {
            FlipAsset();
        }
    }

    void FixedUpdate()
    {

    }

}
