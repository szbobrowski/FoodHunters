using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    private void OnAnimatorMove()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool wPressed = Input.GetKey("w");
        bool aPressed = Input.GetKey("a");
        bool sPressed = Input.GetKey("s");
        bool dPressed = Input.GetKey("d");
        bool upPressed = Input.GetKey(KeyCode.UpArrow);
        bool downPressed = Input.GetKey(KeyCode.DownArrow);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.RightArrow);

        //if (!isWalking && (wPressed || aPressed || sPressed || dPressed))
        if ((!isWalking && wPressed) || (!isWalking && sPressed) || (!isWalking && aPressed) || (!isWalking && dPressed)
            || (!isWalking && upPressed) || (!isWalking && downPressed) || (!isWalking && rightPressed) || (!isWalking && leftPressed))
        {
            animator.SetBool(isWalkingHash, true);
        }
        //if (isWalking && !wPressed && !aPressed && !sPressed || !dPressed)
        if(isWalking && !wPressed && !sPressed && !aPressed && !dPressed && !upPressed && !downPressed && !rightPressed && !leftPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }
}
