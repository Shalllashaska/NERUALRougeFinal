using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMainMenu : MonoBehaviour
{
    public AudioSource source;
    public AudioClip mainMenuSong;

    
    private void Start()
    {
        source.clip = mainMenuSong;
        source.Play();
        source.volume = 0.5f;
    }

    public void ChangeSong(AudioClip song)
    {
        
    }
}
