using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text scoreText;

    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = " " + score.ToString();
    }
}
