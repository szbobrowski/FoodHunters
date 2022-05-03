using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool isGameEnded = false;
    public float restartDelay = 1f;

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
}
