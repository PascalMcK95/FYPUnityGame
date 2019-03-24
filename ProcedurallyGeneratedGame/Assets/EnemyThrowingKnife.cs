using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowingKnife : MonoBehaviour {

    public float speed;
    //public float range;
    private Rigidbody2D rigidBody;
    private Vector2 direction;
    GameObject player;
    private float startTime = 3;
    //Vector2 playerDirection;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if(player.transform.position.x < transform.position.x)
        {

            Debug.Log("Enemy knife going left");
            direction = Vector2.left;
        }
        else{
            Debug.Log("Enemy knife going right");
            direction = Vector2.right;
        }
       rigidBody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
   
        if (startTime >= 0)
        {
            startTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("destroying knife");
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);// when outside view destroy
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy knife hit palyer");
            Destroy(this.gameObject);
        }
    }
}

