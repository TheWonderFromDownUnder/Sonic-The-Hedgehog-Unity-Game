using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int playerScore;
    public Text scoreText;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = "Score " + playerScore.ToString();
    }

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        score += 10;
        scoreText.text = "Score " + playerScore.ToString();
    }
}
