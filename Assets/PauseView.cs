using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseView : View
{
    public TMPro.TextMeshProUGUI scoreInFinish;
    public TMPro.TextMeshProUGUI bestScore;
    public override void Init()
    {
        scoreInFinish.text = TimerManager.Instance.m_TextMeshPro.text;

        if (PlayerPrefs.GetString("Level" + LevelManager.Instance.currentLevel) != "")
            bestScore.text = PlayerPrefs.GetString("Level" + LevelManager.Instance.currentLevel);
        else
            bestScore.text = "00:00:000";
    }
}
