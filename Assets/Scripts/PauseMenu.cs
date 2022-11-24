using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    public TMP_Text timerText;
    private float totalSeconds; //

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();

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
        pauseUI.SetActive(false);
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

    public void UpdateTimerUI()
    {
        //set timer UI
        if (!isPaused)
        {
            totalSeconds += Time.deltaTime;

        }
        timerText.text = TimeSpan.FromSeconds(totalSeconds).ToString("mm\\:ss\\.f");

    }

}
