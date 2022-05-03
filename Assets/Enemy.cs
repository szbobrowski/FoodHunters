using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    GameObject player;
    private State state = State.Alive;
    private Rigidbody rb;
    public int chasingRange = 5;
    public float speed = 3f;
    

    enum State {
	    Alive,
		Chasing,
		Dead
    }

   

	void Start () {
		//OnEnemyKilled = null;
	    player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
		switch (state)
		{
			case State.Alive:
                //rb.velocity = transform.forward * 0f;
				LookAtPlayer();
				if (Vector3.Distance(transform.position, player.transform.position) < chasingRange)
				{
					state = State.Chasing;
					Debug.Log("Enemy is chasing");
				}
				break;
			case State.Chasing:
				LookAtPlayer();
                rb.isKinematic = false;
				rb.velocity = transform.forward * speed;
				
                // if (Vector3.Distance(transform.position, player.transform.position) >= chasingRange)
				// {
				// 	state = State.Alive;
				// }
				break;
			case State.Dead:
				break;
		}
		
		LookAtPlayer();
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			state = State.Dead;
            die();
		}
	}

	public void die(){
        if (OnEnemyKilled != null){
            OnEnemyKilled();
        }
		if (gameObject != null)
		{
			Destroy(gameObject);
		}
    }

	private void LookAtPlayer()
	{
		Vector3 target  = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		transform.LookAt(target);
	}

	public static void Clear(){
		OnEnemyKilled = null;
	}


     void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chasingRange);
    }
}
