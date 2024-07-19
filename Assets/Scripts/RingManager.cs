using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingManager : MonoBehaviour
{
    public int ringCount;
    public Text ringText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ringText.text = "Rings " + ringCount.ToString();
        ScoreManager.instance.addScore();
    }
}
