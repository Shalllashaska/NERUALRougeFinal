using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public Animator anim;
    public bool _isHaveBeenOpened = false;

    public AudioClip openClip;
    public AudioClip moveClip;
    private RoomManager rm;
    private AudioSource sourceFX;
    

    public bool active = true;
    private void Start()
    {
        if (transform.parent.parent.parent.GetComponent<RoomManager>())
        {
            rm = transform.parent.parent.parent.GetComponent<RoomManager>();
        }

        sourceFX = GetComponent<AudioSource>();
    }

    public void Open()
    {
        if (!active) return;
        anim.SetBool("isOpened", true);
        Debug.Log("openDOr");
        sourceFX.PlayOneShot(openClip);
        sourceFX.PlayOneShot(moveClip);
        _isHaveBeenOpened = true;
        if (rm != null)
        {
            if (!rm.EnemyIsSpawn())
            {
                rm.SpawnEnemies();
            }
        }
    }

    public void Close()
    {
        if (_isHaveBeenOpened)
        {
            anim.SetBool("isOpened", false);
        }
    }
}
