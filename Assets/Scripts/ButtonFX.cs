using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clickSound;
    public AudioClip hoverSound;
    public AudioClip dragSound;
    public AudioClip releaseSound;
    
    public void OnClick()
    {
        source.clip = clickSound;
        source.Play();
    }
    
    public void OnHover()
    {
        source.clip = hoverSound;
        source.Play();
    }
    
    public void OnDrag()
    {
        source.clip = dragSound;
        source.Play();
    }
    
    public void OnRelease()
    {
        source.clip = releaseSound;
        source.Play();
    }
}
