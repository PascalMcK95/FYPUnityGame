using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {


    public int health;
    public int maxNumOfHearts;

    public Sprite heart;
    public Image[] hearts;
    // Use this for initialization
	void Start () {
		//hearts = gameObject.GetComponent("HUDCanvas").get
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < hearts.Length; i++)
        {
            if (i < maxNumOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
	}
}
