using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Vector3 enemyDirection;
   Vector3 position;
    Vector3 playerPosition;
    bool detected = false;
    public float sphereRadius;
    int count = 0;

    private GameObject  enemy;
    private GameObject mesh;

    Collider2D enemyCollider;
    MeshCollider meshCollider;
    //private bool alreadySpawned= false;
    GenerateEnemies enemyGen;
   // Vector2 position;
    Vector2 directionCheck;
    // public Enemy newEnemy;
   // Vector3 test = new Vector3( 0, 0, 0 );

    Vector2 moveEnemy;

    public int min;
    public int max;
    float randomX;
    float randomY;

    public LayerMask caveLayer;
    RaycastHit2D hit;

    public int health;

    private bool dead;
    private bool playerDead;

    // damage player
    Collider2D[] playersToDamage;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsAPlayer;
    public int damage;

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

    void Start()
    {
        timeBetweenAttack = startTimeBetweenAttack;
        dead = false;
        playerDead = false;
        moveEnemy = new Vector2();
        base.Start();
        //CheckSpawnPosition();
       // CheckForCollisons();
    }

    void Update()
    {
        position = transform.position;
        //hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
        //CheckSpawnPosition();
        if(health > 0)
        {
            MoveTowardsPlayer();
        }
     

        if (health <= 0 && dead == false)
        {
            Death();
        }
    }

    // attack range sphere
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }  

    private void MoveEnemy(Vector2 moveEnemy)
    {
        transform.Translate(moveEnemy);
        Debug.Log("Enemy Moved ******* " + moveEnemy.x + " " + moveEnemy.y);
        CheckSpawnPosition();
    }

    private void Death()
    {
        dead = true;

        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Run");
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Damaged");
        anim.SetBool("Death", true);
        Destroy(this.gameObject, 3);
        Debug.Log("Enemy died");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detected = true;
        }
    }

    private void Respawn()
    {
        count++;
        randomY = UnityEngine.Random.Range(min, max);
        randomX = UnityEngine.Random.Range(min, max);

        position = new Vector2(randomX, randomY);
        transform.Translate(position);
        Debug.Log(randomX + " " + randomY + " Respawn " + count);
        //CheckSpawnPosition();
       // CheckForCollisons();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detected = false;
            anim.ResetTrigger("Run");
            anim.SetTrigger("Idle");
        }
    }


    protected void MoveTowardsPlayer()
    {
        CheckIfPlayerAlive();
        if (dead == false && playerDead == false)
        {
            if (detected)
            {
                position = transform.position;
                enemyDirection = transform.localScale;
                playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                CheckIfFacingRightDirection();
                anim.ResetTrigger("Run");
                anim.ResetTrigger("Attack");
                anim.ResetTrigger("Damaged");

                if (Vector3.Distance(position, playerPosition) > 4f)
                {
                    anim.ResetTrigger("Attack");
                    transform.position = Vector3.MoveTowards(position, playerPosition, speed / 20);
                    anim.SetTrigger("Run");
                }
                else if (Vector3.Distance(position, playerPosition) <= 4f)
                {
                    AttackPlayer();

                }
            }
        }
        else
        {
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Attack");
            anim.ResetTrigger("Death");
            anim.ResetTrigger("Damaged");
            anim.SetTrigger("Idle");
        }
    }

    private void CheckIfPlayerAlive()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerDead = false;
        }
        else
        {
            anim.ResetTrigger("Attack");
            playerDead = true;
        }
    }

    private void AttackPlayer()
    {
       // Debug.Log("Time since attack" + timeBetweenAttack);
        if (timeBetweenAttack <= 0)
        {
            Debug.Log("Attacking Player");
            anim.ResetTrigger("Run");
            anim.SetTrigger("Attack");

            playersToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsAPlayer);
            if (playersToDamage.Length > 0)
            {
                for (int i = 0; i < playersToDamage.Length; i++)
                {
                    if (playersToDamage[i].tag != "Untagged")
                    {
                        playersToDamage[i].GetComponent<Player>().TakeDamage(damage);
                    }
                }
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
        //if(timeBetweenAttack >= 2)
        //{
        //    anim.ResetTrigger("Attack");
        //}
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("Damaged");
       // WaitForAnimationToFinish("Damaged");
        Debug.Log("Damage taken " + damage + " health remaining " + health);
    }

    //private void WaitForAnimationToFinish(string animation)
    //{
    //    if (this.anim.GetCurrentAnimatorStateInfo(0).IsName(animation))
    //    {
    //        Debug.Log("Animation finished");
    //        anim.ResetTrigger(animation);
    //    }
    //}



    //private void CheckOutsideSpawn()
    //{
    //    position = transform.position;
    //    float distance = 2000.0f;

    //    directionCheck = Vector2.down;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log("outside down *****");
    //        Respawn();
    //        CheckSpawnPosition();
    //    }

    //    directionCheck = Vector2.up;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log("outside up");
    //        Respawn();
    //        CheckSpawnPosition();
    //    }

    //    directionCheck = Vector2.left;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log("outside left *********");
    //        Respawn();
    //        CheckSpawnPosition();
    //    }

    //    directionCheck = Vector2.right;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log("outside right");
    //        Respawn();
    //        CheckSpawnPosition();
    //    }
    //}

    private void CheckSpawnPosition()
    {
        directionCheck = Vector2.down;
        position = transform.position;
        float distance = 10;

        hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.distance + " down green");
            // Debug.DrawRay(transform.position, Vector2.down * 15, Color.green,20);
            //  Debug.Log("need to move up");
            moveEnemy.y += 15;
            // Respawn();
        }

        directionCheck = Vector2.up;
        hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.distance + " up red");

            // Debug.DrawRay(transform.position, Vector2.up*15, Color.red,20);
            //Respawn();
            moveEnemy.y += -10;
        }
        directionCheck = Vector2.left;
        hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.distance + " left blue");
            //Debug.DrawRay(transform.position, Vector2.left * 15, Color.blue,20);
            //Respawn();
            //  Debug.Log("need to move right");
            moveEnemy.x += 15;
        }
        directionCheck = Vector2.right;
        hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.distance + " right yellow");
            //Debug.DrawRay(transform.position, Vector2.right * 15, Color.yellow,20);
            //Respawn();
            //Debug.Log("need to move left");
            moveEnemy.x += -10;
        }
        // Debug.Log("moving enemy " + moveEnemy.x + " " + moveEnemy.y);
        // 

        //    MoveEnemy(moveEnemy);
        //   // Debug.Log("Enemy Moved ******* " + moveEnemy.x + " " + moveEnemy.y);
    }


}
