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

    //Vector2 moveEnemy;
    int chanceOfDrop;

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

    // dropping health
    public GameObject healthDrop;
    Vector3 dropPosition;


    //throwing knife
    public GameObject throwingKnife;
    private float timeSinceLastAttack;
    public float timeBetweenThrowKnife;
    //RaycastHit2D hitLeft;
    //RaycastHit2D hitRight;

    void Start()
    {
        timeBetweenAttack = startTimeBetweenAttack;
        dead = false;
        playerDead = false;
       // moveEnemy = new Vector2();
       
        base.Start();

        //CheckSpawnPosition();
       CheckForCollisions();
    }

    private void CheckForCollisions()
    {
        var hitColliders = Physics2D.OverlapCircleAll(attackPosition.position, 6);

        foreach (var collider in hitColliders)
        {
            if(collider.tag == "CaveMesh")
            {
                Debug.Log("touching walls need to repsawn");
                Respawn();
            }
        }
    }

    void Update()
    {
        position = transform.position;

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
        // collider check
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, 5);
    }  

    private void Death()
    {
        anim.SetBool("Death", true);
        dropPosition = transform.position;
        Destroy(this.gameObject, 3);
        DropHealth();
        dead = true;
        Debug.Log("Enemy died");
    }

    private void DropHealth()
    {
         chanceOfDrop = UnityEngine.Random.Range(1, 4);
         Debug.Log(chanceOfDrop + " drop chance");

        if(chanceOfDrop == 1)
        {
            Instantiate(healthDrop, dropPosition, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detected = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ThrowingKnife")
        {
            Debug.Log("hit by knife");
            this.TakeDamage(damage);
        }
    }

    private void Respawn()
    {
        count++;
        randomY = UnityEngine.Random.Range(min, max);
        randomX = UnityEngine.Random.Range(min, max);

        position = new Vector2(randomX, randomY);
        transform.SetPositionAndRotation(position,Quaternion.identity);
        Debug.Log(randomX + " " + randomY + " Respawn " + count);
       // CheckOutsideMap();
       // CheckSpawnPosition();
        CheckForCollisions();
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

    private void ThrowKnife()
    {
        if (timeBetweenThrowKnife <= 0)
        {
            if (facingLeft)
            {
                Debug.Log("Enemy throwing left");
                Vector2 knifePosition = new Vector2(transform.position.x - 3, transform.position.y + 5);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 90)));
            }
            else
            {
                Debug.Log("Enemy throwing right " + transform.position.y + " " + (transform.position.y  +8));
                Vector2 knifePosition = new Vector2(transform.position.x + 5, transform.position.y + 5);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 180, 90)));
            }
            timeBetweenThrowKnife = 4;
        }
        timeBetweenThrowKnife -= Time.deltaTime;

        //Debug.Log("time " + timeBetweenThrowKnife);
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

                    WalkToPlayer();

                    Vector2 left = new Vector2(transform.position.x - 5, transform.position.y + 5);
                    Vector2 right = new Vector2(transform.position.x + 5, transform.position.y + 5);

                    Debug.DrawRay(left, Vector2.left * 50f, Color.green);
                    Debug.DrawRay(right, Vector2.right * 50f, Color.red);

                    if (Vector3.Distance(position, playerPosition) >= 6f)
                    {
                        RaycastHit2D []  hitLeft = Physics2D.RaycastAll(left, Vector2.left * 50f);
                        RaycastHit2D[] hitRight = Physics2D.RaycastAll(right, Vector2.right * 50f);

                        if (hitLeft != null)
                        {
                            for (int i = 0; i < hitLeft.Length; i++)
                            {
                                if(hitLeft[i].rigidbody!= null)
                                {
                                    if (hitLeft[i].rigidbody.tag == "Player")
                                    {
                                        ThrowKnife();
                                    }
                                }
                            }
                        }
                        if(hitRight!= null)
                        {
                            for (int i = 0; i < hitRight.Length; i++)
                            {
                                if (hitRight[i].rigidbody != null)
                                {
                                    if (hitRight[i].rigidbody.tag == "Player")
                                    {
                                        ThrowKnife();
                                    }
                                }
                            }
                        }
                    }
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
            anim.ResetTrigger("Damaged");
            anim.SetTrigger("Idle");
        }
    }

    private void WalkToPlayer()
    {
        transform.position = Vector3.MoveTowards(position, playerPosition, speed / 20);
        anim.SetTrigger("Run");
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
    }

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

    //private void CheckSpawnPosition()
    //{
    //    directionCheck = Vector2.down;
    //    position = transform.position;
    //    float distance = 10;

    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //   // Debug.DrawLine(position, directionCheck);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log(hit.distance + " down green");
    //         Debug.DrawRay(transform.position, Vector2.down * 15, Color.green,20);
    //        //  Debug.Log("need to move up");
    //       // moveEnemy.y += 15;
    //         Respawn();
    //    }

    //    directionCheck = Vector2.up;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //   // Debug.DrawLine(position, directionCheck);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log(hit.distance + " up red");

    //         Debug.DrawRay(transform.position, Vector2.up*15, Color.red,20);
    //        Respawn();
    //      //  moveEnemy.y += -10;
    //    }
    //    directionCheck = Vector2.left;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //   // Debug.DrawLine(position, directionCheck);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log(hit.distance + " left blue");
    //        Debug.DrawRay(transform.position, Vector2.left * 15, Color.blue,20);
    //        Respawn();
    //        //  Debug.Log("need to move right");
    //       // moveEnemy.x += 15;
    //    }
    //    directionCheck = Vector2.right;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //   // Debug.DrawLine(position, directionCheck);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log(hit.distance + " right yellow");
    //        Debug.DrawRay(transform.position, Vector2.right * 15, Color.yellow,20);
    //        Respawn();
    //        //Debug.Log("need to move left");
    //       // moveEnemy.x += -10;
    //    }

    //   // CheckSpawnPosition();
    //}

    //private void CheckOutsideMap()
    //{
    //    directionCheck = Vector2.down;
    //    position = transform.position;
    //    float distance = 1000;

    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //    // Debug.DrawLine(position, directionCheck);
    //    if (hit.collider == null)
    //    {
    //        Debug.Log("outside down");
    //        Respawn();
    //    }
    //    directionCheck = Vector2.up;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //    // Debug.DrawLine(position, directionCheck);
    //    if (hit.collider == null)
    //    {
    //        Debug.Log("outside up");
    //        Respawn();
    //    }
    //    directionCheck = Vector2.left;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //    // Debug.DrawLine(position, directionCheck);
    //    if (hit.collider == null)
    //    {
    //        Debug.Log("outside left");
    //        Respawn();
    //    }
    //    directionCheck = Vector2.right;
    //    hit = Physics2D.Raycast(position, directionCheck, distance, caveLayer);
    //    // Debug.DrawLine(position, directionCheck);
    //    if (hit.collider == null)
    //    {
    //        Debug.Log("outside right");
    //        Respawn();
    //    }

    //}
}
