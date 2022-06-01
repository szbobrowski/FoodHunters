using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI enemy;
    public TextMeshProUGUI time;

    void Start()
    {
        score.text = GameManager.scoreText;
        enemy.text = GameManager.enemyText;
        time.text = GameManager.timeText;
    }

}
