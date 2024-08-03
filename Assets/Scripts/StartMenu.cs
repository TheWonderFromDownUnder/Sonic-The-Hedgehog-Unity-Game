using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    void Update() {

        bool isSubmitKeyPressed = Input.GetButtonDown(PAP.submitKeyName);

        if (isSubmitKeyPressed == true)
        {
            AudioManager.PlaySound(SoundType.checkpointSound);
            SceneManager.LoadScene(1);
        }
    }
}
