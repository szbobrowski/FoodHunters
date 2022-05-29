using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    GameObject player;
    private State state = State.Alive;
    private Rigidbody rb;
    public float chasingRange;
    public float speed;
	private int hp;
	private float multiplayerDivider;
    

    enum State {
	    Alive,
		Chasing,
		Dead
    }

   

	void Start () {
		SetupParameters();
	    player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
		UpdateParameters();
		Debug.Log(speed);
		switch (state)
		{
			case State.Alive:
                //rb.velocity = transform.forward * 0f;
				LookAtPlayer();
				if (Vector3.Distance(transform.position, player.transform.position) < chasingRange)
				{
					state = State.Chasing;
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
			hp--;
			if (hp == 0) 
			{
				state = State.Dead;
            	die();
			}
		}
	}

	private void SetupParameters()
    {

		multiplayerDivider = 40f;

        switch(GameManager.GetLevel()){
            case GameManager.Level.Easy:
            {
                chasingRange = 5f;
				speed = 2f;
				hp = 1;
            }
            break;
            case GameManager.Level.Medium: 
            {
                chasingRange = 10f;
				speed = 2.5f;
				hp = 2;
            }
            break;
            case GameManager.Level.Hard: 
            {
                chasingRange = 15f;
				speed = 3f;
				hp = 2;
            }
            break;
        }
    }
	private void UpdateParameters()
    {
		float multiplayer = (GetNumberOfCollectedItems() + multiplayerDivider) / multiplayerDivider;

        switch(GameManager.GetLevel()){
            case GameManager.Level.Easy:
            {
                chasingRange = multiplayer * 5f;
				speed = multiplayer * 2f;
            }
            break;
            case GameManager.Level.Medium: 
            {
                chasingRange = multiplayer * 10f;
				speed = multiplayer * 2.5f;
            }
            break;
            case GameManager.Level.Hard: 
            {
                chasingRange = multiplayer * 15f;
				speed = multiplayer * 3f;
            }
            break;
        }
    }

	private int GetNumberOfCollectedItems()
	{
		return ThirdPersonMovement.numberOfCollectItems;
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
