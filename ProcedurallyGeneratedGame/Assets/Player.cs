
using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rigidbody;
    Vector2 velocity;
    Animator anim;
    Vector3 scale;
    public bool facingRight = true;

    int health;

    void Start()
    {
        health = 3;
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scale = transform.localScale;
    }

    void Update()
    {
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 10;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {

            anim.ResetTrigger("Idle");
            anim.SetTrigger("Run");
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            anim.ResetTrigger("Run");
            anim.SetTrigger("Idle");
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown("space"))
        {
            anim.SetTrigger("Attack");
        }

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

