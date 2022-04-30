using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public GameObject myObjects;

    void Update()
    {
        //transform.Rotate(50 * Time.deltaTime, 0 ,0 );
    }

    private void generateObject ()
    {
        Debug.Log("Create object");
        //int randomIndexObject = Random.Range(0,gameObject.length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-20,20),45,Random.Range(80,120));
        Instantiate(gameObject,randomSpawnPosition,Quaternion.identity);
    }

    private void OnTriggerEnter (Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            ThirdPersonMovement.numberOfCollectItems += 1;
            Debug.Log("Item was picked up");
            Debug.Log("Score: " + ThirdPersonMovement.numberOfCollectItems);
            Destroy(gameObject);
            generateObject();
        }
    }


}
