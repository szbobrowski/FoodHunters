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
    public static GameManager instance;
    private bool isGameEnded = false;
    public static int levelGame = 0;
    public static string enemyText;
    public static string scoreText;
    public static string timeText;

    public enum Level {
        Easy,
        Medium,
        Hard
    }

    public static DateTime startGameUtcDate;
    public DateTime endGameUtcDate;

    void Start()
    {
      startGameUtcDate = DateTime.UtcNow;
    }

    void Awake() 
    {
      instance = this;
    }

  public void EndGame()
  {
      if (!isGameEnded)
      {
          scoreText = ThirdPersonMovement.numberOfCollectItems.ToString();
          enemyText = EnemyManager.numberOfKilledEnemies.ToString();
         
          isGameEnded = true;
          endGameUtcDate = DateTime.UtcNow;
          var diffDate = (endGameUtcDate - startGameUtcDate);
          timeText = diffDate.Seconds + ":" + diffDate.Milliseconds;
          
          Restart();
      }
      
  }

  private void Restart()
  {
      levelGame = 0;
      ThirdPersonMovement.Clear();
      Enemy.Clear();
      EnemyManager.Clear();
      SceneManager.LoadScene("GameOverMenu");
  }

  public static Level GetLevel(){
      if (levelGame == 0)
      {
        return Level.Easy;
      } else if(levelGame == 1) 
      {
        return Level.Medium;
      } else if(levelGame == 2)
      {
        return Level.Hard;
      }
      return Level.Easy;
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
