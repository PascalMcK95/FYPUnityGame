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
    float sphereRadius= 20;
    int count = 0;

    private GameObject  enemy;
    private GameObject mesh;

    Collider2D enemyCollider;
    MeshCollider meshCollider;
    private bool alreadySpawned= false;
    GenerateEnemies enemyGen;
    Vector2 position;
   // public Enemy newEnemy;

    public int min;
    public int max;
    float randomX;
    float randomY;


    void Start()
    {
  


        //CheckSpawnPosition();
        base.Start();
    }

    private void CheckSpawnPosition()
    {
        if (Physics.CheckSphere(transform.position, sphereRadius))
        {
            GameObject.Destroy(this);
            count++;
            Debug.Log("Destroyed " + count);
        }
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if (collision.gameObject.tag == "CaveMesh" && alreadySpawned == false)
    //    //{
    //    //    //alreadySpawned = true;

    //    //    Debug.Log("I spawned in wall");
    //    //    //Destroy(this.gameObject);
    //    //    //enemyGen.SpawnEnemies();
    //    //    Respawn();

    //    //    Debug.Log("Destroy");
    //    //}
    //    //alreadySpawned = true;

    //    while (collision.gameObject.tag == "CaveMesh" && alreadySpawned == false)
    //    {
    //        //alreadySpawned = true;

    //        Debug.Log("I spawned in wall");
    //        //Destroy(this.gameObject);
    //        //enemyGen.SpawnEnemies();
    //       // Respawn();

    //        //Debug.Log("Destroy");
    //    }
    //    //alreadySpawned = true;
    //}

    private void Respawn()
    {
        randomY = UnityEngine.Random.Range(min, max);
        randomX = UnityEngine.Random.Range(min, max);

        position = new Vector3(randomX, randomY, 0);
        //Instantiate(newEnemy, position, Quaternion.identity);
        transform.Translate(position);
        Debug.Log("Respawn");
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
