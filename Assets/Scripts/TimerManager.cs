using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoSingleton<TimerManager>
{
    public TMPro.TextMeshProUGUI m_TextMeshPro;
    float gameTime;
    public bool isBlockTimer;
    public int milliseconds, seconds, minutes;
    private void Update()
    {
        // Obtenir le temps du jeu en secondes.
        if (isBlockTimer)
            return;


        gameTime += Time.deltaTime;

        // Formater le temps en minutes et secondes.
        minutes = Mathf.FloorToInt(gameTime / 60);
        seconds = Mathf.FloorToInt(gameTime % 60);

        milliseconds = Mathf.FloorToInt((gameTime * 1000) % 1000);

        // Mettre à jour le texte de l'UI avec le temps formaté.
        m_TextMeshPro.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void ResetTime()
    {
        gameTime = 0;
    }

}
