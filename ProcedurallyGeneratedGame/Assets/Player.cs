
using System;
using UnityEngine;

public class Player : Character
{
    Vector3 scale;

    void Start()
    {
        scale = transform.localScale;
        base.Start();
    }

    protected override void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 10;
        CheckDirection();
        Animate();

        base.Update();
    }

    void FixedUpdate()
    {
     
    }

    private void CheckDirection()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !facingLeft)
            FlipAsset();
        else if (h < 0 && facingLeft)
            FlipAsset();
    }


    void Animate()
    {
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

    }
}

