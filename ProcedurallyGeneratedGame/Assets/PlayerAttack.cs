using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {


    private float timeBetweenAttack;
    public float startTimeBetweenAttack;
    public int damage;
    Collider2D[]  enemiesToDamage;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsAnEnemy;
    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
       // enemiesToDamage = new [] Collider2D();
    }
	
	// Update is called once per frame
	void Update () {
		if(timeBetweenAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Attacking");
                anim.SetTrigger("Attack");
                enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsAnEnemy);
                if(enemiesToDamage.Length > 0)
                {
                    //Debug.Log("Enemis in range " + enemiesToDamage.Length);

                    //foreach (var item in enemiesToDamage)
                    //{
                    //    Debug.Log(item.tag + "*************************************************");
                    //}
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].tag != "Untagged")
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        }
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

    // shows area in range of taking damage
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    //}
}
