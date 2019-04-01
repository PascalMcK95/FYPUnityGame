using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScoreToLeaderboard : MonoBehaviour {

    string input;

    void Start()
    {


    }

    //void CheckInput()
    //{
    //    if (input != null)
    //    {
    //        Debug.Log("Writing to file " + input + " score: " + scoreText.text);
    //        textToWrite = input.text + "," + scoreText.text + ";";
    //        SaveScoreToLeaderboard(textToWrite);
    //    }
    //}

    //public void SaveScoreToLeaderboard(string text)
    //{

    //    string path = "Assets/Resources/Leaderboard.txt";

    //    //Write some text to the test.txt file
    //    StreamWriter writer = new StreamWriter(path, true);
    //    writer.WriteLine(text);
    //    writer.Close();

    //    //Re-import the file to update the reference in the editor
    //    AssetDatabase.ImportAsset(path);
    //}
}
