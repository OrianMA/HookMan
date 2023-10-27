using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoSingleton<timer>
{
    public TMPro.TextMeshProUGUI m_TextMeshPro;
    float gameTime;
    public bool isBlockTimer;

    private void Start()
    {
        isBlockTimer = false;
    }


    private void Update()
    {
        // Obtenir le temps du jeu en secondes.
        if (!isBlockTimer)
            gameTime += Time.deltaTime;

        // Formater le temps en minutes et secondes.
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        int milliseconds = Mathf.FloorToInt((gameTime * 1000) % 1000);

        // Mettre à jour le texte de l'UI avec le temps formaté.
        m_TextMeshPro.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void ResetTime()
    {
        gameTime = 0;
    }

}
