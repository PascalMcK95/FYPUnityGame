using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour {
    bool keepAddingEnemies = true;
    Vector2 position;
    public int min;
    public int max;
    public  Enemy enemies;
    public int maxEnemies;
    float randomX;
    float randomY;

    List<Enemy> totalEnemies = new List<Enemy>();

    void Start()
    {
        SpawnEnemies();
    }

    private void Update()
    {
        //Debug.Log("***********"+GameObject.FindGameObjectsWithTag("Enemy(Clone)").Length);
    }

    private void SpawnEnemies()
    {
        while (totalEnemies.Count <= maxEnemies)
        {
            randomY = Random.Range(min, max);
            randomX = Random.Range(min, max);
            //Debug.Log("X: " + randomX + " Y: " + randomY);
            position = new Vector2((Random.value * randomX), (Random.value * randomY));
            Instantiate(enemies, position, Quaternion.identity);
            totalEnemies.Add(enemies);
        }
        //GameObject SpawnLocation = Instantiate(enemies, position, Quaternion.identity);
        ////if(Collision2D.Equals())
        //totalEnemies.Add(SpawnLocation);
       // Debug.Log("***********" + GameObject.FindGameObjectsWithTag("Enemy(Clone)").Length);
       // Debug.Log("Enemy count : " + totalEnemies.Count);
    }
}
