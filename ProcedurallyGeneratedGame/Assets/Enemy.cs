using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //public Transform target;//set target from inspector instead of looking in Update
    public float speed = 3f;

    Rigidbody2D rigidbody;
    Vector2 velocity;
    Animator anim;
    Vector3 enemyPosition;
    bool facingRight = true;
    Vector3 playerPosition;
    int health;

    void Start()
    {
        health = 3;
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        enemyPosition = transform.localScale;
       playerPosition = GameObject.Find("Player").transform.position;
        Debug.Log(enemyPosition + " " + playerPosition);
        //velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 10;
        //transform.LookAt(playerPosition);

        //move towards the player
        if (Vector3.Distance(transform.position, enemyPosition) > 1f)
        {//move if distance from target is greater than 1
         // transform.Translate(new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0));
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed/20);
        }

        //CheckIfDamaged();
    }

    private void CheckIfDamaged()
    {
        throw new NotImplementedException();
    }

    void FixedUpdate()
    {
        //float h = Input.GetAxis("Horizontal");
        //if (h > 0 && !facingRight)
        //    Flip();
        //else if (h < 0 && facingRight)
        //    Flip();
              float h = Input.GetAxis("Horizontal");
        if (gameObject.transform.position.x > 0 && !facingRight)
            Flip();
        else if (gameObject.transform.position.x < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
