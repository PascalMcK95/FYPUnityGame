
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
    public bool dead;
    Health healthBar;
    public Score scoreBoard;

    //throwing knife
    public GameObject throwingKnife;
    private float timeSinceLastAttack;
    public int timeBetweenThrowKnife;

    public static string finalScore;
    float attackReset;

    LoadSceneOnClick loadScene;
    void Start()
    {
        loadScene = new LoadSceneOnClick();
        healthBar = gameObject.GetComponent<Health>();
        //scoreBoard = gameObject.GetComponent<Score>();
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

        timeSinceLastAttack -= Time.deltaTime;

        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HealthDrop")
        {
            Debug.Log("Health picked up");
            Destroy(collision.gameObject);
            healthBar.AddHeart();
            health += 10;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyThrowingKnife")
        {
            Debug.Log("hit by knife");
            this.TakeDamage(damage);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Run");
        // anim.ResetTrigger("Attack");
        anim.SetBool("Attack", false);
        anim.SetTrigger("Damaged");
        healthBar.RemoveHeart();
    }

    private void ThrowKnife()
    {
        if(timeSinceLastAttack <= 0)
        {
            if (!facingLeft)
            {
                Vector2 knifePosition = new Vector2(transform.position.x - 3, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 90)));
            }
            else
            {
                Vector2 knifePosition = new Vector2(transform.position.x + 4, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 180, 90)));
            }
            timeSinceLastAttack = timeBetweenThrowKnife;
        }

        Debug.Log("time " + timeSinceLastAttack);
    }

    private void Death()
    {
        dead = true;

        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Run");
        //anim.ResetTrigger("Attack");
        anim.SetBool("Attack", false);
        anim.ResetTrigger("Damaged");
        anim.SetBool("Death", true);
        // Destroy(this.gameObject, 2);
        finalScore = scoreBoard.scoreText.text;
        loadScene.LoadSceneByIndex(2);
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
        //anim.SetBool("Attack", false);
        if (dead == false)
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
                anim.ResetTrigger("Damaged");
                anim.ResetTrigger("Run");
                //Debug.Log("Attacking");
                //anim.SetTrigger("Attack");
                anim.SetBool("Attack", true);
                attackReset = 1.1f;
                enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsAnEnemy);
                if (enemiesToDamage.Length > 0)
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].tag != "Untagged")
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                            scoreBoard.AddDamageToScore();
                        }
                    }
                }
                
            }
            else if(Input.GetMouseButtonDown(0))
            {
                ThrowKnife();
            }
        }
        else
        {
            anim.ResetTrigger("Run");
            // anim.ResetTrigger("Attack");
            anim.SetBool("Attack",false);
            //anim.ResetTrigger("Death");
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Damaged");
            anim.SetBool("Death", true);
        }

        if(attackReset >= 0)
        {
            attackReset -= Time.deltaTime;
        }
        else{
            anim.SetBool("Attack", false);
        }
    }
}

