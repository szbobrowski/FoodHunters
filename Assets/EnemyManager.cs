using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool useSpawnPoints = true;
    private int numberOfEnemies;
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;


    private int numberOfSpawnPoints;

    private float range = 10;

   

    // Start is called before the first frame update
    void Start()
    {
        SetupParameters();
        
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

    private void SetupParameters()
    {
        switch(GameManager.GetLevel()){
            case GameManager.Level.Easy:
            {
                numberOfEnemies = 2;
            }
            break;
            case GameManager.Level.Medium: 
            {
                numberOfEnemies = 4;
            }
            break;
            case GameManager.Level.Hard: 
            {
                numberOfEnemies = 5;
            }
            break;
        }
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
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-20,20), 43.9f, Random.Range(80,120));
            while(Vector3.Distance(randomSpawnPosition, playerTransform.position) < range)
            {
               randomSpawnPosition = new Vector3(Random.Range(-20,20), 43.9f, Random.Range(80,120));
            }
            
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        }

       
       
        


    }

}
