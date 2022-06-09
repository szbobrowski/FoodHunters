using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public void ShowMenu()
    {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        SceneManager.LoadScene("MainMenu");
    }
}
