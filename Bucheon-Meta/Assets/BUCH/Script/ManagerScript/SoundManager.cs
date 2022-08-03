using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;
    private bool isPlayed = true;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ToggleBGM()
    {
        isPlayed = !isPlayed;
        if (isPlayed)
        {
            PlayBGM();
        }
        else
        {
            StopBGM();
        }
    }
    public void PlayBGM()
    {
        audioSource.Play();
    }
    public void StopBGM()
    {
        audioSource.Pause();
    }

}
