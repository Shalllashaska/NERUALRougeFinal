using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] clips;

    private AudioSource sfx;
    private int i;
    void Start()
    {
        sfx = gameObject.GetComponent<AudioSource>();
        i = Random.Range(0, clips.Length);
        sfx.clip = clips[i];
        sfx.Play();
    }

    
    void Update()
    {
        if (!sfx.isPlaying)
        {
            i++;
            if (i == clips.Length)
            {
                i = 0;
            }
            sfx.clip = clips[i];
            sfx.Play();
        }
    }
}
