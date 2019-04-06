using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnife : MonoBehaviour {

    public Score scoreBoard;
    public float speed;
    //public float range;
    private Rigidbody2D rigidBody;
    private Vector3 direction;
    GameObject player;
    private float startTime = 3;
    Camera camera;
    //Vector2 playerDirection;
    // Use this for initialization
    void Start () {
        //scoreBoard = new Score();
        camera = Camera.main;
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        //if (player.transform.rotation.y > 0)
        //{
        //    direction = Vector2.right;
        //}
        //else
        //{
        //    direction = Vector2.left;
        //}

        direction = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
        //direction = Input.mousePosition;
       // transform.rotation = Quaternion.LookRotation(direction);
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
       //rigidBody.velocity = direction * speed;
       transform.position = Vector3.MoveTowards(transform.position, direction, speed / 10);
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
           // scoreBoard.AddDamageToScore();
        }

        if (collision.gameObject.tag == "CaveMesh")
        {
            Destroy(this.gameObject);
        }
    }
}
