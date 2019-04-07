
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
    Camera camera;
    //throwing knife
    public GameObject throwingKnife;
    private float timeSinceLastAttack;
    public int timeBetweenThrowKnife;

    public static string finalScore;
    float attackReset;
    public LoadSceneOnClick loadScene;
    float timeSinceHit;

    void Start()
    {
        timeSinceHit = 0f;
        camera = Camera.main;
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
        ResetHit();
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

    private void ResetHit()
    {
        if (timeSinceHit <= 0)
        {
            anim.SetBool("Damaged", false);
            timeSinceHit -= Time.deltaTime;
        }
        else
        {
            timeSinceHit -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HealthDrop")
        {
           // Debug.Log("Health picked up");
            Destroy(collision.gameObject);
            healthBar.AddHeart();

            if(health < 50)
            {
                health += 10;
            }
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
        anim.SetBool("Damaged", true);
        timeSinceHit = 0.5f;
        healthBar.RemoveHeart();
    }

    private void ThrowKnife()
    {
        if(timeSinceLastAttack <= 0)
        {
            Vector3 finalPosition =  camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
            Vector2 knifePosition;
            var xDiff = finalPosition.x - transform.position.x;
            var YDiff = finalPosition.y - transform.position.y;
            //if(xDiff >=0 && YDiff >= 0)
            //{
            //    knifePosition = new Vector2(transform.position.x + 7, transform.position.y + 3);
            //    Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, -135)));//tr
            //}
            //else if(xDiff < 0 && YDiff <0)
            //{
            //    knifePosition = new Vector2(transform.position.x - 7, transform.position.y + 3);
            //    Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 135)));//bl
            //}
            //else if(xDiff > 0 && YDiff < 0)
            //{
            //    knifePosition = new Vector2(transform.position.x + 7, transform.position.y + 3);
            //    Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 315)));//br
            //}
            //else
            //{
            //    knifePosition = new Vector2(transform.position.x - 7, transform.position.y + 3);
            //    Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 45)));//tl
            //}

            //Debug.Log(xDiff + "  " + YDiff);

            if (((xDiff < 20 && xDiff > -30) || (xDiff > -20 && xDiff < 20)) && YDiff > 10)
            {
                knifePosition = new Vector2(transform.position.x, transform.position.y + 8);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 180)));//up
            }
            else if (xDiff < -20 && YDiff > 30)
            {
                knifePosition = new Vector2(transform.position.x - 7, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 45)));//tl
            }
            else if (xDiff < -30 && YDiff < 30 && YDiff > -30)
            {
                knifePosition = new Vector2(transform.position.x - 5, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 90)));//left
            }
            else if (xDiff > 20 && YDiff < 30 && YDiff < -30)
            {
                knifePosition = new Vector2(transform.position.x + 7, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 315)));//br 
            }
            else if (xDiff > 30 && (YDiff < 30 && YDiff > -30))
            {
                knifePosition = new Vector2(transform.position.x + 5, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 270)));//right
            }
            else if ((xDiff > -20 && xDiff < 20) && (YDiff < -30))
            {
                knifePosition = new Vector2(transform.position.x, transform.position.y - 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 360)));//down
            }
            else if (xDiff >= 0 && YDiff >= 0)
            {
                knifePosition = new Vector2(transform.position.x + 7, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, -135)));//tr
            }
            else if (xDiff < 0 && YDiff < 0)
            {
                knifePosition = new Vector2(transform.position.x - 7, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 135)));//bl
            }



            timeSinceLastAttack = timeBetweenThrowKnife;
        }

        //Debug.Log("time " + timeSinceLastAttack);
    }

    private void Death()
    {
        dead = true;

        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Run");
        //anim.ResetTrigger("Attack");
        anim.SetBool("Attack", false);
        anim.SetBool("Damaged", false);
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
                anim.SetBool("Damaged", false);
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
                        if (enemiesToDamage[i].tag == "Enemy")
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                            scoreBoard.AddDamageToScore(20);
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
            anim.SetBool("Damaged", false);
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

