using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    Vector2 position;
    public int min;
    public int max;
    public  Enemy enemy;
    public int maxEnemies;
    float randomX;
    float randomY;

    List<Enemy> totalEnemies = new List<Enemy>();
    GameObject [] enemies;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        SpawnEnemies();
       // Debug.Log("Enemies " + enemies.Length);
        //CheckEnemyPosition();
    }

    //private void CheckEnemyPosition()
    //{
    //    foreach(GameObject enemyObject in enemies)
    //    {
    //        enemyObject.
    //    }
    //}

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        CheckEnemies();
        //Debug.Log("***********"+GameObject.FindGameObjectsWithTag("Enemy(Clone)").Length);
    }

    private void CheckEnemies()
    {
        if(enemies.Length == 0)
        {
            maxEnemies += 3;
            SpawnEnemies();
            Debug.Log("Spawned enemies " + maxEnemies);
        }
    }

    public void SpawnEnemies()
    {
        while (enemies.Length < maxEnemies)
        {
            randomY = UnityEngine.Random.Range(min, max);
            randomX = UnityEngine.Random.Range(min, max);
            //Debug.Log("X: " + randomX + " Y: " + randomY);
            //position = new Vector2((Random.value * randomX), (Random.value * randomY));
            position = new Vector2(randomX, randomY);
            Instantiate(enemy, position, Quaternion.identity);
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            //totalEnemies.Add(enemy);
        }
        //GameObject SpawnLocation = Instantiate(enemies, position, Quaternion.identity);
        ////if(Collision2D.Equals())
        //totalEnemies.Add(SpawnLocation);
       // Debug.Log("***********" + GameObject.FindGameObjectsWithTag("Enemy(Clone)").Length);
       // Debug.Log("Enemy count : " + totalEnemies.Count);
    }
}
