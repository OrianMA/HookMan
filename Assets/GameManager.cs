using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        PlayerController.Instance.Init();
        UiManager.Instance.Init();
        SoundManager.Instance.Init();
        //LevelManager.Instance.currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring("Level".Length));
        print(LevelManager.Instance.currentLevel);
    }
}
