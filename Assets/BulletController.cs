using UnityEngine;

public class BulletController : MonoBehaviour
{



    void Start()
    {
        Destroy(gameObject, 5);
    }

    public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall")
		{
			if (gameObject != null){
                Destroy(gameObject);
            }
		}
	}

}