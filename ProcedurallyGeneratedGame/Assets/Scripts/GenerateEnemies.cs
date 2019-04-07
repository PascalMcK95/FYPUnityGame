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
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        if(enemies.Length == 0)
        {
            maxEnemies += 3;
            SpawnEnemies();
            IncreaseDifficulty();
            Debug.Log("Spawned enemies " + maxEnemies);
        }
    }

    public void SpawnEnemies()
    {
        while (enemies.Length < maxEnemies)
        {
            randomY = UnityEngine.Random.Range(min, max);
            randomX = UnityEngine.Random.Range(min, max);
            position = new Vector2(randomX, randomY);
            Instantiate(enemy, position, Quaternion.identity);
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    public void IncreaseDifficulty()
    {
        GameObject[] range = GameObject.FindGameObjectsWithTag("Range");
        float radius;
        foreach (var item in range)
        {
            radius = item.GetComponent<CircleCollider2D>().radius;
            radius += 10;
            item.GetComponent<CircleCollider2D>().radius = radius;
        }
        Debug.Log("Difficulty Increased********************");
    }
}
