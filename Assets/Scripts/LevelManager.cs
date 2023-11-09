using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public enum LevelType
{
    Parkour,
    FarFarAway
}

public class LevelManager : MonoSingleton<LevelManager>
{
    public int currentLevel;
    public LevelType levelType;

    string bestScore;
    string scoreOnFinish;

    int bestMinutes;
    int bestSecondes;
    int bestMillisecondes;

    int minutes;
    int seconds;
    int milliseconds;

    public bool isBeatScore;

    public void SetNewTimeOnFinish()
    {
        bestScore = PlayerPrefs.GetString("Level" + currentLevel);
        scoreOnFinish = TimerManager.Instance.m_TextMeshPro.text;

        switch (levelType)
        {
            case LevelType.Parkour:
                FinishParkourLevel();
                break;
            case LevelType.FarFarAway:
                FinishFarFarAwayLevel();
                break;
        }

            UiManager.Instance.OpenView("EndView");
            UiManager.Instance.PauseGame();
    }


    void FinishParkourLevel()
    {

        if (bestScore != "")
        {
            bestMinutes = int.Parse(bestScore.Split(':')[0]);
            bestSecondes = int.Parse(bestScore.Split(':')[1]);
            bestMillisecondes = int.Parse(bestScore.Split(':')[2]);
        }

        minutes = int.Parse(scoreOnFinish.Split(':')[0]);
        seconds = int.Parse(scoreOnFinish.Split(':')[1]);
        milliseconds = int.Parse(scoreOnFinish.Split(':')[2]);
        //00:00:000

        int bestTimeCompress = bestMinutes * 100000 + bestSecondes * 1000 + bestMillisecondes;
        int timeOnFinishCompress = minutes * 100000 + seconds * 1000 + milliseconds;

        if (bestTimeCompress > timeOnFinishCompress || bestScore == "" || bestTimeCompress == 0)
        {
            print("records battues");
            isBeatScore = true;
            scoreOnFinish = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            PlayerPrefs.SetString("Level" + currentLevel, scoreOnFinish);
        }
    }

    void FinishFarFarAwayLevel()
    {
        if (bestScore == "")
        {
            isBeatScore = true;
        } else
        {
            bestScore = bestScore.Remove(bestScore.Length - 1);
            bestScore = bestScore.Replace(',', '.');
        }

        scoreOnFinish = scoreOnFinish.Remove(scoreOnFinish.Length - 1);
        scoreOnFinish = scoreOnFinish.Replace(',', '.');

        if (isBeatScore || float.Parse(bestScore, NumberStyles.Any, new CultureInfo("en-us")) < float.Parse(scoreOnFinish, NumberStyles.Any, new CultureInfo("en-us")))
        {
            print("records battues");
            isBeatScore = true;
            scoreOnFinish = scoreOnFinish + "M";
            bestScore = bestScore + "M";
            PlayerPrefs.SetString("Level" + currentLevel, scoreOnFinish);
        }
    }
}
