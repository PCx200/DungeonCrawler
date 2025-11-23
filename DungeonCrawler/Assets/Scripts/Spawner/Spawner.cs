using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Enemy> enemiesToSpawn = new List<Enemy>();
    [SerializeField] BoxCollider spawnArea;
    [SerializeField] float spawnInterval;

    float lastSpawnTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       SpawnEnemies();
    }

    public void SpawnEnemy()
    {
        int randEnemyIndex = Random.Range(0, enemiesToSpawn.Count);

        float xPos = Random.Range(transform.position.x - spawnArea.size.x / 2, transform.position.x + spawnArea.size.x / 2);
        float zPos = Random.Range(transform.position.x - spawnArea.size.z / 2, transform.position.x + spawnArea.size.z / 2);

        Vector3 pos = new Vector3(xPos, transform.position.y, zPos);

        Instantiate(enemiesToSpawn[randEnemyIndex], pos, Quaternion.identity);
    }
    
    void SpawnEnemies()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
        
    }


}
