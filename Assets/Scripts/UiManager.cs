using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoSingleton<UiManager>
{
    public List<View> allView;

    public void Init()
    {
        OpenView("GameView");
    }
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        TimerManager.Instance.isBlockTimer = true;
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        TimerManager.Instance.isBlockTimer = false;
        Time.timeScale = 1.0f;
    }

    public void OpenView(string name)
    {
        foreach (View view in allView)
        {
            if (view.viewName == name)
            {
                view.gameObject.SetActive(true);
                view.Init();
            } else 
                view.gameObject.SetActive(false);
        }
    }

}


[System.Serializable]
public class View : MonoBehaviour
{
    public string viewName;
    public virtual void Init()
    {

    }
}
