using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    public TMP_Text timerText;
    private float secondsCount;
    private int minuteCount;
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
            secondsCount += Time.deltaTime;
        }

        timerText.text = (int)secondsCount + "";
        //timerText.text = minuteCount + ":" + (int)secondsCount + "";
        /*if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }*/
    }

}
