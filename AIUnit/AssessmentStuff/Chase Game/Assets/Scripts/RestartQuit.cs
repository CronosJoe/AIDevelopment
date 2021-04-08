using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartQuit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape")) 
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            SceneManager.LoadScene("Game");
        }
    }
}
