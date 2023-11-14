using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndView : View
{
    public TMPro.TextMeshProUGUI scoreInFinish;
    public TMPro.TextMeshProUGUI bestScore;

    bool isFirstTime;

    public override void Init()
    {
        scoreInFinish.text = TimerManager.Instance.m_TextMeshPro.text;

        if (PlayerPrefs.GetString("Level" + LevelManager.Instance.currentLevel) != "")
            bestScore.text = PlayerPrefs.GetString("Level" + LevelManager.Instance.currentLevel);
        else
        {
            isFirstTime = true;
            if (LevelManager.Instance.levelType == LevelType.Parkour)
                bestScore.text = "00:00:000";
            if (LevelManager.Instance.levelType == LevelType.FarFarAway)
                bestScore.text = "0M";
        }
    }

}

[System.Serializable]
public class TrollEndText
{
    public string name;
    public List<string> quotes;
}