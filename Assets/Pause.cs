using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool _paused = false;

    public GameObject panel;
    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyUp(KeyCode.Escape)) return;
        
        _paused = !_paused;
        Debug.Log(_paused);
        if (_paused)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            panel.SetActive(false);
        }
    }
}
