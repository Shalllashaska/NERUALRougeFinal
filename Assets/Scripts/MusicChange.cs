using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicChange : MonoBehaviour
{
    public AudioMixerGroup Mixer;

    public GameObject global;
    public GameObject music;
    public GameObject sound;
    public GameObject ui;

    private void Start()
    {
        global.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("masterVolume", 0.8f);
        sound.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("soundsVolume", 0.8f);
        music.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVolume", 0.8f);
        ui.transform.GetComponent<Slider>().value = PlayerPrefs.GetFloat("uiVolume", 0.8f);
    }


    public void ChangeMusicVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("musicVolume", Mathf.Lerp(-80, 20, volume));
        PlayerPrefs.SetFloat("musicVolume",volume);
    }
    
    public void ChangeSoundVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("soundsVolume", Mathf.Lerp(-80, 20, volume));
        PlayerPrefs.SetFloat("soundsVolume",volume);
    }
    
    public void ChangeUIVolume(float volume)
    { 
        Mixer.audioMixer.SetFloat("uiVolume", Mathf.Lerp(-80, 20, volume));
        PlayerPrefs.SetFloat("uiVolume",volume);
    }
    
    public void ChangeGlobalVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("masterVolume", Mathf.Lerp(-80, 20, volume));
        PlayerPrefs.SetFloat("masterVolume",volume);
    }
}
