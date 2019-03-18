using System;
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
        for (int i = 0; i < hearts.Length; i++)
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
	
	// Update is called once per frame
	void Update () {

	}

    private void CheckForDeath()
    {

        Debug.Log("");
    }

    public void RemoveHeart()
    {
        int count = 0;
        foreach (var item in hearts)
        {
            Debug.Log(item.enabled + "^^^^^^^^^^^");
            if (item.enabled)
            {
                count++;
            }
        }
        if(count > 0)
        {
            hearts[count - 1].enabled = false;
        }
    }
}
