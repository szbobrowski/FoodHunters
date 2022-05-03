using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool useSpawnPoints = true;
    public int numberOfEnemies;
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;


    private int numberOfSpawnPoints;

    private float range = 10;

   

    // Start is called before the first frame update
    void Start()
    {
        
        numberOfSpawnPoints = spawnPoints.Length;
        for(int i=0; i < numberOfEnemies; i++)
        {
            SpawnNewEnemy();
        }
        
    }

    void OnEnable()
    {
        Enemy.OnEnemyKilled += SpawnNewEnemy;
    }


    void SpawnNewEnemy() {

        if (useSpawnPoints)
        {
            
            int randomNumber = Mathf.RoundToInt(Random.Range(0f, numberOfSpawnPoints-1));
            Debug.Log(randomNumber);
            Debug.Log(spawnPoints[randomNumber]);
            Instantiate(enemyPrefab, spawnPoints[randomNumber].transform.position, Quaternion.identity);
        }
        else 
        {
            Transform playerTransform = GameObject.Find("Third Person Player").transform;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-20,20), 48f, Random.Range(80,120));
            while(Vector3.Distance(randomSpawnPosition, playerTransform.position) < range)
            {
               randomSpawnPosition = new Vector3(Random.Range(-20,20), 48f, Random.Range(80,120));
            }
            
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        }

       
       
        


    }

}
