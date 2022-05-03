using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5);
    }

    void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			if (gameObject != null){
                Destroy(gameObject);
            }
		}
	}

}