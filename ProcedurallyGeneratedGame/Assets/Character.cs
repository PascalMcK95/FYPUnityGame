using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected float speed;
    [SerializeField]
    //protected float health;
    Vector3 theScale;

    public Animator anim;
    private Rigidbody2D rigidbody;

    protected bool facingLeft = false;

    protected bool isAttacking = false;

    
    protected Vector2 direction;

    protected virtual void Start()
    {
        //Makes a reference to the rigidbody2D
        rigidbody = GetComponent<Rigidbody2D>();

        //Makes a reference to the character's animator
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected void Move()
    {
        //if(anim.GetBool("Attack") == false)
        //{
        //    rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
        //}
        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    protected void FlipAsset()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0,180,0);   
    }
}
