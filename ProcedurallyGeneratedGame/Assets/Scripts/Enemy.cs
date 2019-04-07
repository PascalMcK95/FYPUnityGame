using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    Vector3 enemyDirection;
   Vector3 position;
    Vector3 playerPosition;
    Vector3 newPosition;
    bool detected = false;
    public float sphereRadius;
    int count = 0;

    private GameObject  enemy;
    private GameObject mesh;

    Collider2D enemyCollider;
    MeshCollider meshCollider;
    GenerateEnemies enemyGen;
    Vector2 directionCheck;

    public int chanceOfDrop;

    public int min;
    public int max;
    float randomX;
    float randomY;

    public LayerMask caveLayer;
    public LayerMask playerLayer;
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

    RaycastHit2D [] walk;
    bool lineOfSight = false;


    //navmesh
    public NavMeshAgent agent;

    Vector2 hright;
    Vector2 hleft;
    //GameObject player1;
    Vector3 rayPosition;
    Vector3 playerRayPosition;

    Quaternion targetRotation;
    float str;
    Quaternion rotation;

    void Start()
    {
       
        timeBetweenAttack = startTimeBetweenAttack;
        dead = false;
        playerDead = false;

        //resets z position to 0;
        var newPosition = transform.position;
        newPosition.z = 0;
        transform.position = newPosition;
       
       base.Start();
       CheckForCollisions();
    }

    void Update()
    {
        //CheckRotation();
        rayPosition = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        position = transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerRayPosition = new Vector3(playerPosition.x, playerPosition.y + 4, playerPosition.z);

        hleft = new Vector2(playerPosition.x - 30, playerPosition.y);
        hright = new Vector2(playerPosition.x + 30, playerPosition.y);

        var newPosition = transform.position;
        newPosition.z = 0;
        transform.position = newPosition;

        if (health > 0)
        {
            MoveTowardsPlayer();
        }
     
        if (health <= 0 && dead == false)
        {
            Death();
        }
    }

    //private void CheckRotation()
    //{
    //    targetRotation = Quaternion.LookRotation(playerPosition - transform.position);
    //     str = Mathf.Min(10 * Time.deltaTime, 1);
    //     rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    //}

    private void CheckForCollisions()
    {
        var hitColliders = Physics2D.OverlapCircleAll(attackPosition.position, 6);

        foreach (var collider in hitColliders)
        {
            if (collider.tag == "CaveMesh")
            {
                Debug.Log("touching walls need to repsawn");
                Respawn();
            }
        }
    }

    // attack range sphere
    private void OnDrawGizmos()
    {
        //attack radius
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPosition.position, attackRange);

        // line of sight
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rayPosition, playerRayPosition);

        // blocking object
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(hleft, rayPosition);
        Gizmos.DrawLine(hright, rayPosition);
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
         chanceOfDrop = UnityEngine.Random.Range(1, chanceOfDrop);
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
            Vector2 knifePosition;
            var xDiff = playerPosition.x - transform.position.x;
            var YDiff = playerPosition.y - transform.position.y;
            //if (xDiff >= 0 && YDiff >= 0)
            //{
            //    knifePosition = new Vector2(transform.position.x + 7, transform.position.y + 3);
            //    Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, -135)));//tr
            //   // Debug.Log("enemy " + transform.position + " player " + playerPosition + " xdiff " + xDiff + " yDiff " + YDiff);
            //}
            //else if (xDiff < 0 && YDiff < 0)
            //{
            //    knifePosition = new Vector2(transform.position.x - 7, transform.position.y + 3);
            //    Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 135)));//bl
            //}
            //else if (xDiff > 0 && YDiff < 0)
            //{
            //    knifePosition = new Vector2(transform.position.x + 7, transform.position.y + 3);
            //    Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 315)));//br
            //}

            //else
            //{
            //    knifePosition = new Vector2(transform.position.x - 7, transform.position.y + 3);
            //    Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 45)));//tl
            //}


            Debug.Log(xDiff + "  " + YDiff);

            if (((xDiff < 10 && xDiff > -10) || (xDiff > -10 && xDiff < 10)) && YDiff > 10)
            {
                knifePosition = new Vector2(transform.position.x, transform.position.y + 9);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 180)));//up
            }
            else if (xDiff < -15 && YDiff > 15)
            {
                knifePosition = new Vector2(transform.position.x - 7, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 45)));//tl
            }
            else if (xDiff < -15 && YDiff < 15 && YDiff > -15)
            {
                knifePosition = new Vector2(transform.position.x - 5, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(0, 0, 90)));//left
            }
            else if (xDiff > 15 && YDiff < 15 && YDiff < -15)
            {
                knifePosition = new Vector2(transform.position.x + 7, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 315)));//br 
            }
            else if (xDiff > 15 && (YDiff < 15 && YDiff > -15))
            {
                knifePosition = new Vector2(transform.position.x + 5, transform.position.y + 3);
                Instantiate(throwingKnife, knifePosition, Quaternion.Euler(new Vector3(180, 0, 270)));//right
            }
            else if ((xDiff > -10 && xDiff < 10) && (YDiff < -15))
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

            timeBetweenThrowKnife = 3;
        }
        timeBetweenThrowKnife -= Time.deltaTime;
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
              
                CheckIfFacingRightDirection();
                anim.ResetTrigger("Run");
                anim.ResetTrigger("Attack");
                anim.ResetTrigger("Damaged");

                if (Vector3.Distance(position, playerPosition) > 4f)
                {
                    anim.ResetTrigger("Attack");

                     WalkToPlayer();

                    RaycastHit2D hits;
                    hits = Physics2D.Linecast(rayPosition, playerRayPosition, playerLayer);

                    if (hits.collider != null)
                    {
                        ThrowKnife();
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
      
        RaycastHit2D hits;
        hits = Physics2D.Linecast(rayPosition, playerRayPosition, caveLayer);
        if(hits.collider == null)
        {
            transform.position = Vector3.MoveTowards(position, playerPosition, speed /10);
            anim.SetTrigger("Run");
        }
        else{
            MoveAroundObject();
            anim.ResetTrigger("Run");
        
        }
    }

    private void MoveAroundObject()
    {
        // ray from centre of enemy
        //rayPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        RaycastHit2D rayLeft;
        rayLeft = Physics2D.Linecast(hleft, rayPosition, caveLayer);

        RaycastHit2D rayRight;
        rayRight = Physics2D.Linecast(hright, rayPosition, caveLayer);
        if (rayLeft.collider == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, hleft, speed / 10);
        }
        else if (rayLeft.collider != null && rayRight.collider == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, hright, speed / 10);
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
                    if (playersToDamage[i].tag == "Player")
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
}
