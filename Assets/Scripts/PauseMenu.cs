using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }

    public void PauseGame (){
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame (){
        ButtonPressed();
        isPaused = false;
    }

    public void MainMenu(){
        ButtonPressed();
        SceneManager.LoadScene(0);
    }

    public void ButtonPressed(){
        Time.timeScale = 1f;
        isPaused = false;
    }
    
}
