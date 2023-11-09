using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoSingleton<TimerManager>
{
    public TMPro.TextMeshProUGUI m_TextMeshPro;
    float gameScore;
    public bool isBlockTimer;
    public int milliseconds, seconds, minutes;
    private void Update()
    {
        // Obtenir le temps du jeu en secondes.
        if (isBlockTimer)
            return;

        switch(LevelManager.Instance.levelType)
        {
            case LevelType.Parkour:
                gameScore += Time.deltaTime;

                // Formater le temps en minutes et secondes.
                minutes = Mathf.FloorToInt(gameScore / 60);
                seconds = Mathf.FloorToInt(gameScore % 60);

                milliseconds = Mathf.FloorToInt((gameScore * 1000) % 1000);

                // Mettre à jour le texte de l'UI avec le temps formaté.
                m_TextMeshPro.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
                break;
            case LevelType.FarFarAway:
                gameScore = PlayerController.Instance.transform.position.x;

                m_TextMeshPro.text = System.Math.Round(gameScore, 1).ToString() + 'M';
                break;
        }

    }

    public void ResetTime()
    {
        gameScore = 0;
    }

}
