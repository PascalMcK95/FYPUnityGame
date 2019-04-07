using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text scoreText;
    public InputField input;
    string textToWrite;
    string path;
    // Use this for initialization
    void Start ()
    {
         path = "Assets/Resources/Leaderboard.txt";

        scoreText.text = Player.finalScore.ToString();
    }

    void SaveScoreToLeaderboard(string text)
    { 
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();
    }
    
    public void GetLeaderboard()
    { 
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();

    }

    public void CloseGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
