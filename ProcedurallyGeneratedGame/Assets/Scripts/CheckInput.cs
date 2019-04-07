using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CheckInput : MonoBehaviour {

    public InputField input;
    public Text scoreText;
    string textToWrite;
    public LoadSceneOnClick sceneLoader;

    void Start()
    {
        scoreText.text = Player.finalScore.ToString();

        string path = "Assets/Resources/";
        string filename = "Leaderboard.txt";

     
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
            {
                if (!File.Exists(filename))
                    File.Create(path + filename);
            }
    }

    public void ValidateInput()
    {

        if (input.text != "")
        {
            Debug.Log("Writing to file " + input + " score: " + scoreText.text);
            textToWrite = input.text + "," + scoreText.text;
            SaveScoreToLeaderboard(textToWrite);
        }
    }

    void SaveScoreToLeaderboard(string text)
    {
        string path = "Assets/Resources/Leaderboard.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();
        sceneLoader.LoadSceneByIndex(0);
    }
}
