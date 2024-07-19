using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goalpost : MonoBehaviour
{
    public AudioSource goalSound;

    // Start is called before the first frame update
    void Start()
    {
        goalSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            goalSound.Play();
            StartCoroutine(wait());
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(7);
    }
}
