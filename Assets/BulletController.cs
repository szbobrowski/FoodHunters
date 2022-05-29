using UnityEngine;

public class BulletController : MonoBehaviour
{


    public static int counterShootEnemy = 0;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			if (gameObject != null){
                Destroy(gameObject);
                counterShootEnemy++;
            }
		}
	}

}