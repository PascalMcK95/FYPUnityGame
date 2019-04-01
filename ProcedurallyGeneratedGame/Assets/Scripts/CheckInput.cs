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

    void Start()
    {
        scoreText.text = Player.finalScore.ToString();
    }

    public void ValidateInput()
    {

        if (input != null)
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

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
    }
}
