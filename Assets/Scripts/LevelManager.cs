using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public int currentLevel;

    string bestTime;
    string timeOnFinish;

    int bestMinutes;
    int bestSecondes;
    int bestMillisecondes;

    int minutes;
    int seconds;
    int milliseconds;

    public bool isBeatScore;

    public void SetNewTimeOnFinish()
    {

        bestTime = PlayerPrefs.GetString("Level"+currentLevel);
        timeOnFinish = TimerManager.Instance.m_TextMeshPro.text;

        if (bestTime != "")
        {
            bestMinutes = int.Parse(bestTime.Split(':')[0]);
            bestSecondes = int.Parse(bestTime.Split(':')[1]);
            bestMillisecondes = int.Parse(bestTime.Split(':')[2]);
        }

        minutes = int.Parse(timeOnFinish.Split(':')[0]);
        seconds = int.Parse(timeOnFinish.Split(':')[1]);
        milliseconds = int.Parse(timeOnFinish.Split(':')[2]);
        //00:00:000

        int bestTimeCompress = bestMinutes * 100000 + bestSecondes * 1000 + bestMillisecondes;
        int timeOnFinishCompress = minutes * 100000 + seconds * 1000 + milliseconds;

        if (bestTimeCompress > timeOnFinishCompress || bestTime == "" || bestTimeCompress == 0)
        {
            print("records battues");
            isBeatScore = true;
            timeOnFinish = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            PlayerPrefs.SetString("Level"+currentLevel, timeOnFinish);
            Time.timeScale = 0;
        }

        UiManager.Instance.OpenView("EndView");
        UiManager.Instance.PauseGame();
    }
}
