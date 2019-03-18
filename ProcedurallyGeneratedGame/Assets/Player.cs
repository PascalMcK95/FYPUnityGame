
using System;
using UnityEngine;

public class Player : Character
{
    Vector3 scale;

    //private float timeBetweenAttack;
    //public float startTimeBetweenAttack;
    public int damage;
    Collider2D[] enemiesToDamage;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsAnEnemy;
    public int health;
    bool dead;
    Health healthBar;

    void Start()
    {
        healthBar = gameObject.GetComponent<Health>();
        dead = false;
        scale = transform.localScale;
        base.Start();
    }

    // shows area in range of taking damage
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    protected override void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 10;
        CheckDirection();
        Animate();

        if (health <= 0 && dead == false)
        {
            Death();
        }

        base.Update();
    }

    void FixedUpdate()
    {
     
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Run");
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Damaged");
        healthBar.RemoveHeart();
    }

    private void Death()
    {
        dead = true;

        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Run");
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Damaged");
        anim.SetBool("Death", true);
        Destroy(this.gameObject, 2);

        //Debug.Log("Player Died");
    }

    private void CheckDirection()
    {
        if(anim.GetBool("Attack") == false)
        {
            float h = Input.GetAxis("Horizontal");
            if (h > 0 && !facingLeft)
            {
                FlipAsset();
            }
            else if (h < 0 && facingLeft)
            {
                FlipAsset();
            }
        }
    }


    void Animate()
    {
        if(dead == false)
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
                anim.ResetTrigger("Idle");
                anim.ResetTrigger("Run");
                //Debug.Log("Attacking");
                anim.SetTrigger("Attack");
                enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsAnEnemy);
                if (enemiesToDamage.Length > 0)
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].tag != "Untagged")
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                }
            }
        }
        else
        {
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Attack");
            //anim.ResetTrigger("Death");
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Damaged");
            anim.SetBool("Death", true);
        }
    }
}

