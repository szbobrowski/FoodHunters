using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonHandler : MonoBehaviour
{
    public void ExitButton() 
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        Application.Quit();
        Debug.Log("Game closed");
    }
}
