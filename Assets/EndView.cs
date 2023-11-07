using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndView : View
{
    public TMPro.TextMeshProUGUI scoreInFinish;
    public TMPro.TextMeshProUGUI bestScore;
    public TMPro.TextMeshProUGUI trollQuote;
    public List<TrollEndText> trollEndTexts;

    public override void Init()
    {
        scoreInFinish.text = TimerManager.Instance.m_TextMeshPro.text;

        if (PlayerPrefs.GetString("Level" + LevelManager.Instance.currentLevel) != "")
            bestScore.text = PlayerPrefs.GetString("Level" + LevelManager.Instance.currentLevel);
        else
            bestScore.text = "00:00:000";

        if (LevelManager.Instance.isBeatScore || bestScore.text == "00:00:000")
        {
            trollQuote.text = trollEndTexts[0].quotes[Random.Range(0, trollEndTexts[0].quotes.Count)];
        } else
        {
            trollQuote.text = trollEndTexts[1].quotes[Random.Range(0, trollEndTexts[1].quotes.Count)];
        }
    }

}

[System.Serializable]
public class TrollEndText
{
    public string name;
    public List<string> quotes;
}