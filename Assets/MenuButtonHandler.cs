using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public void ShowMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
