﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnife : MonoBehaviour {

    public Score scoreBoard;
    public float speed;
    private Rigidbody2D rigidBody;
    private Vector3 direction;
    GameObject player;
    private float startTime = 3;
    Camera camera;
    Vector3 movementVector;
    GameObject score;

    private Quaternion _facing;


    void Start ()
    { 
        score = GameObject.FindWithTag("Score");
        scoreBoard = score.GetComponent<Score>();
        camera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
        movementVector = (direction - transform.position);

        //transform.rotation = Quaternion.Slerp(transform.rotation,
        //                                   Quaternion.LookRotation(movementVector),
        //                                   Time.deltaTime);

        movementVector = movementVector.normalized;
    }
	
	// Update is called once per frame
	void Update () {
        if(startTime >= 0)
        {
            startTime -= Time.deltaTime;
        }
        else
        {
           // Debug.Log("destroying knife");
            Destroy(this.gameObject);
        }
	}

    private void FixedUpdate()
    {
        transform.position += movementVector * (Time.deltaTime * speed *5);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);// when outside view destroy
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit");
            Destroy(this.gameObject);
            scoreBoard.AddDamageToScore(10);
        }

        if (collision.gameObject.tag == "CaveMesh")
        {
            Destroy(this.gameObject);
        }
    }
}