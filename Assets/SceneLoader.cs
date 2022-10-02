using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string sceneName)
    {
        Debug.Log("Change scene");
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
