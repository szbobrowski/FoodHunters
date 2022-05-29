using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;

public class GameManager : MonoBehaviour
{
    private bool isGameEnded = false;
    //public float restartDelay = 1f;
    public int levelGame = 0;

    public TextMeshProUGUI score;
    public TextMeshProUGUI enemy;
    public TextMeshProUGUI time;
    public TextMeshProUGUI gameOver;
    public static DateTime startGameUtcDate;
    public DateTime endGameUtcDate;

  public void EndGame()
  {
      if (!isGameEnded)
      {
          Debug.Log("Collect items: " + ThirdPersonMovement.numberOfCollectItems.ToString());
          score.text = ThirdPersonMovement.numberOfCollectItems.ToString();
          Debug.Log("Number of enemies shot: " + BulletController.counterShootEnemy.ToString());
          enemy.text = BulletController.counterShootEnemy.ToString();
          isGameEnded = true;
          endGameUtcDate = DateTime.UtcNow;
          Debug.Log("Game over: " + endGameUtcDate);
          var diffDate = (endGameUtcDate - startGameUtcDate);
          Debug.Log("Game last: " + diffDate.Minutes + ":" + diffDate.Seconds + ":" + diffDate.Milliseconds);
          time.text = "diffDate.Minutes" + ":" + diffDate.Seconds + ":" + diffDate.Milliseconds;
          gameOver.text = "Game Over";
          SceneManager.LoadScene("GameOverMenu");
          //Restart();
      }
      
  }

  private void Restart()
  {
      SceneManager.LoadScene("GameOverMenu");
      ThirdPersonMovement.Clear();
      Enemy.Clear();
  }

  public void ExitButton() 
  {
      Application.Quit();
      Debug.Log("Game closed");
  }

  public void StartGame()
  {
      SceneManager.LoadScene("SampleScene");
      startGameUtcDate = DateTime.UtcNow;
      Debug.Log("Start game:" + startGameUtcDate);
  }

    public void MenuButton()
  {
      SceneManager.LoadScene("MainMenu");
  }

  public void HandleInputLevel(int optionNumber)
  {
      if(optionNumber == 0)
      {
          levelGame = 0;
      }
      else if (optionNumber == 1)
      {
          levelGame = 1;
      }
      else if (optionNumber == 2)
      {
          levelGame = 2;
      }

      Debug.Log("Set level: " + levelGame);
  }

}
