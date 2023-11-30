using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public string _levelParkour;
    public string _levelStatic;
    public string _levelDistance;
    public GameObject deleteAccountPanel;

    public void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void Parkour()
    {
        SceneManager.LoadScene(_levelParkour);
    }

    public void Static()
    {
        SceneManager.LoadScene(_levelStatic);
    }

    public void Distance()
    {
        SceneManager.LoadScene(_levelDistance);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ActiveDeleteAccountPanel(bool isActive)
    {
        deleteAccountPanel.SetActive(isActive);
    }

    public void DeleteAllScore()
    {
        PlayerPrefs.DeleteAll();
        ActiveDeleteAccountPanel(false);
    }
}
