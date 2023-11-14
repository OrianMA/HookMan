using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public List<AudioClip> allMusics;
    public AudioSource audioSource;

    public void Init()
    {
        audioSource.clip = allMusics[Random.Range(0, allMusics.Count)];
        audioSource.Play();
    }
}
