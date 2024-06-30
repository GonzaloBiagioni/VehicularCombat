using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float timeRemaining = 30f; 
    public TMP_Text timerText; 

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            ChangeToVictoryScene();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 30);
        int seconds = Mathf.FloorToInt(timeRemaining % 30);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ChangeToVictoryScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3); 
    }
}