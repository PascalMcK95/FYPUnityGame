using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadLeaderboard : MonoBehaviour {

    List<PlayerScore> listOfScores;
    public Text first;
    public Text second;
    public Text third;
    public Text fourth;
    public Text fifth;

    void Start()
    {
        listOfScores = new List<PlayerScore>();
        GetLeaderboard();
        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        Debug.Log("count " + listOfScores.Count);
        List<PlayerScore> SortedList = listOfScores.OrderByDescending(x => x.score).ToList();

        first.text = "1st\t\t\t" + SortedList[0].name + "\t\t\t" + SortedList[0].score;
        second.text = "2nd\t\t\t" + SortedList[1].name + "\t\t\t" + SortedList[1].score;
        third.text = "3rd\t\t\t" + SortedList[2].name + "\t\t\t" + SortedList[2].score;
        fourth.text = "4th\t\t\t" + SortedList[3].name + "\t\t\t" + SortedList[3].score;
        fifth.text = "5th\t\t\t" + SortedList[4].name + "\t\t\t" + SortedList[4].score;
    }

    public void GetLeaderboard()
    {
        string path = "Assets/Resources/Leaderboard.txt";
        string[] values;

        using (StreamReader sr = new StreamReader(path))
        {
            string line;

            // Read and display lines from the file until the end of 
            // the file is reached.
            while ((line = sr.ReadLine()) != null)
            {
                PlayerScore player = new PlayerScore();
                values = line.Split(',');
                player.name = values[0];
                player.score = Convert.ToInt32(values[1]);
                listOfScores.Add(player);
            }
        }
    }

    class PlayerScore
    {
        public string name { get; set; }
        public int score { get; set; }
    }

}
