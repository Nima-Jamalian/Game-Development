using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGamePause = false;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePause == false)
            {
                PauseGame();
            } else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        isGamePause = true;
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        isGamePause = false;
        Time.timeScale = 1f;
    }
}
