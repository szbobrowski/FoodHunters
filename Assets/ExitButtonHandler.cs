using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonHandler : MonoBehaviour
{
    public void ExitButton() 
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
}
