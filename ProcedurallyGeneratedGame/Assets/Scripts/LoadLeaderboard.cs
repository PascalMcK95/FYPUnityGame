using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadLeaderboard : MonoBehaviour {

    List<PlayerScore> listOfScores;
    public Text firstName;
    public Text secondName;
    public Text thirdName;
    public Text fourthName;
    public Text fifthName;
    public Text firstScore;
    public Text secondScore;
    public Text thirdScore;
    public Text fourthScore;
    public Text fifthScore;

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

        //first.text = "1st\t\t\t" + SortedList[0].name + "\t\t\t" + SortedList[0].score;
        //second.text = "2nd\t\t\t" + SortedList[1].name + "\t\t\t" + SortedList[1].score;
        //third.text = "3rd\t\t\t" + SortedList[2].name + "\t\t\t" + SortedList[2].score;
        //fourth.text = "4th\t\t\t" + SortedList[3].name + "\t\t\t" + SortedList[3].score;
        //fifth.text = "5th\t\t\t" + SortedList[4].name + "\t\t\t" + SortedList[4].score;
        firstName.text = SortedList[0].name;
        firstScore.text = SortedList[0].score.ToString();
        secondName.text = SortedList[1].name;
        secondScore.text = SortedList[1].score.ToString();
        thirdName.text = SortedList[2].name;
        thirdScore.text = SortedList[2].score.ToString();
        fourthName.text = SortedList[3].name;
        fourthScore.text = SortedList[3].score.ToString();
        fifthName.text = SortedList[4].name;
        fifthScore.text = SortedList[4].score.ToString();

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
