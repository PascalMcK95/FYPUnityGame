using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowingKnife : MonoBehaviour {

    public float speed;
    private Rigidbody2D rigidBody;
    private Vector2 direction;
    public float startTime = 3;
    Vector3 player;
    Vector3 movementVector;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
       player = GameObject.FindGameObjectWithTag("Player").transform.position;
        player.y += 3;
        movementVector = (player - transform.position);
        movementVector.z = 0;
        movementVector = movementVector.normalized;


        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, player - transform.position);

    }

    void Update()
    {
        transform.position += movementVector * (speed +2) * Time.deltaTime;
        if (startTime >= 0)
        {
            startTime -= Time.deltaTime;
        }
        else
        {
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
           // Debug.Log("Enemy knife hit palyer");
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "CaveMesh")
        {
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}

