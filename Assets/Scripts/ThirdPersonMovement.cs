using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public int playerHP = 4;

    private float collisionCheckInterval = 1.5f;
    private float immortalTimeInterval = 1f;
    private float immortalTime = 0f;
    private bool isImmortal = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public static int numberOfCollectItems = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerHP_text;

    public static bool isColided = false;
    private float elapsedTime = 0f;

    //BOUND
    // private void Start()
    // {
    //     //transform.position = cam.transform.position;
    //     numberOfCollectItems=0;
    // }

    // Update is called once per frame
    void Update()
    {
        
        if (isImmortal)
        {
            immortalTime += Time.deltaTime;
            if (immortalTime >= immortalTimeInterval)
            {
                isImmortal = false;
                immortalTime = 0f;
            }
        }
        Debug.Log(isImmortal);

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= collisionCheckInterval) {
            elapsedTime = 0;
            OnCollisionWithEnemy();
        }
        float horizontal = Input.GetAxisRaw("Horizontal") * -1;
        float vertical = Input.GetAxisRaw("Vertical") * -1;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {   
            //BOUND
            /*if(transform.position.y >= 2)
            {
                transform.position = new Vector3(transform.position.x, 2, 0);
            }*/
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * -1;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        scoreText.text = "Score: " + numberOfCollectItems.ToString();
        playerHP_text.text = "Player HP: " + playerHP;
    }

    private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
            isColided = true;
            OnCollisionWithEnemy();
		}
	}

    private void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Enemy")
		{
			isColided = false;
		}
	}
    
    private void OnCollisionWithEnemy()
    {
        if (isColided && !isImmortal)
        {
            playerHP--;
            isImmortal = true;
            elapsedTime = 0;
            FindObjectOfType<AudioManager>().Play("zombieBite");
        }

        if (playerHP <= 0) 
        {
            die();
        }
    }

    private void die()
    {
        FindObjectOfType<GameManager>().EndGame();
        // Destroy(gameObject);
    }

    public static void Clear()
    {
        numberOfCollectItems = 0;
    }
}
