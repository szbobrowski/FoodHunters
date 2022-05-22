using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool isGameEnded = false;
    public float restartDelay = 1f;
    public int levelGame = 0;

    public TextMeshProUGUI gameOver;

  public void EndGame()
  {
      if (!isGameEnded)
      {
          isGameEnded = true;
          Debug.Log("GAME OVER");
          gameOver.text = "Game Over";
          Invoke("Restart", restartDelay);
      }
      
  }

  private void Restart()
  {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
