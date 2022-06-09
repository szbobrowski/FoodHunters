using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

	GameObject text;
	TextMesh t;
	private Animator animator;
    

    enum State {
	    Alive,
		Chasing,
		Dead
    }

   

	void Start () {
		animator = GetComponent<Animator>();
		SetupParameters();
	    player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

		text = new GameObject();
		t = text.AddComponent<TextMesh>();
	}
	
	void Update ()
	{
		UpdateParameters();
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
				animator.SetBool("isWalking", true);
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
		UpdateText();
	}

	public void UpdateText() {
		t.text = "HP: " + hp;
		t.transform.localPosition = transform.position + new Vector3(1f, 6f, 0f);
		t.transform.localEulerAngles = transform.eulerAngles;
		t.transform.Rotate(0, 180, 0);
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
			else 
			{
				state = State.Chasing;
				rb.isKinematic = true;
				rb.isKinematic = false;
			}
	

			FindObjectOfType<AudioManager>().Play("zombieDead");
		}

		if (collision.gameObject.tag == "Player")
		{
			rb.isKinematic = true;
			rb.isKinematic = false;
		}
	}

	private void SetupParameters()
    {
		multiplayerDivider = 40f;

        switch(GameManager.GetLevel()){
            case GameManager.Level.Easy:
            {
                chasingRange = 7f;
				speed = 2f;
				hp = 2;
            }
            break;
            case GameManager.Level.Medium: 
            {
                chasingRange = 10f;
				speed = 2.5f;
				hp = 3;
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
		//bool isWalking = animator.GetBool(isWalkingHash);
		float multiplayer = (GetNumberOfCollectedItems() + multiplayerDivider) / multiplayerDivider;

        switch(GameManager.GetLevel()){
            case GameManager.Level.Easy:
            {
                chasingRange = multiplayer * 7f;
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

		t.text = "";
    }

	private void OnDestroy() {
		ThirdPersonMovement.isColided = false;
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
