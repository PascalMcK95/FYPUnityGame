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

    // Use this for initialization
    void Start ()
    {
        scoreText.text = Player.finalScore.ToString();
        //textToWrite = input.text + "," + scoreText.text + ";";
    }

    void Update()
    {
        
    }

    //public void CheckInput()
    //{
    //    if(input != null)
    //    {
    //        Debug.Log("Writing to file " + input + " score: " + scoreText.text);
    //        textToWrite = input.text + "," + scoreText.text + ";";
    //        SaveScoreToLeaderboard(textToWrite);
    //    }
    //}
	
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

    //[MenuItem("Tools/Read file")]
    public void GetLeaderboard()
    {
        string path = "Assets/Resources/Leaderboard.txt";

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
