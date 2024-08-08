using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public int livesCount;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//      livesText.text = "Lives " + livesCount.ToString();

        if (livesCount < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void LoseLife()
    {
        livesCount--;
    }

    public void GainLife()
    {
        livesCount++;
    }

}
