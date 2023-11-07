using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public List<Sprite> spritesMapInOrder;
    public Image mapImage;
    public TMPro.TextMeshProUGUI levelSelectedText;
    public TMPro.TextMeshProUGUI levelMaxText;
    public TMPro.TextMeshProUGUI BestScore;
    public int maxLevelNumber;

    int levelSelectedIndex;

    public void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SelectLevel(1);
        levelMaxText.text = maxLevelNumber.ToString();
        
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene("Level" + levelSelectedIndex);
    }

    public void SelectLevel(int levelIndex)
    {
        levelSelectedIndex = levelIndex;

        levelSelectedText.text = levelIndex.ToString();
        mapImage.sprite = spritesMapInOrder[levelSelectedIndex-1];
        print(PlayerPrefs.GetString($"Level{levelIndex}"));
        if (PlayerPrefs.GetString($"Level{levelIndex}") == "")
            BestScore.text = "00:00:000";
        else
            BestScore.text = PlayerPrefs.GetString($"Level{levelIndex}");

    }

    public void NextLevel()
    {
        levelSelectedIndex++;
        if (levelSelectedIndex > maxLevelNumber)
            levelSelectedIndex = 1;
        SelectLevel(levelSelectedIndex);
    }

    public void PreviousLevel()
    {
        levelSelectedIndex--;
        if (levelSelectedIndex <= 0)
            levelSelectedIndex = maxLevelNumber;
        SelectLevel(levelSelectedIndex);
    }
}
