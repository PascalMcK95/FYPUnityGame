using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    float timer = 0.0f;
    int time = 0;
    public int score = 0;
    public Text scoreText;
    public Player player;
    float startTime;
    int timePassed;

   

    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        if(player.dead == false)
        {
            time = Convert.ToInt32(Time.time - startTime);
            scoreText.text = (time + score).ToString();

        }

    }

    public void AddDamageToScore()
    {
        score += 10;
    }
}
