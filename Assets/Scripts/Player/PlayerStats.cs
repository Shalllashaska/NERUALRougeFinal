using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("---Stats---")] 
    [Range(1,10)]
    public float strength = 3;
    [Range(1,10)]
    public float stealth = 3;
    [Range(1,10)]
    public float agility = 3;
    
    [Header("---Speed---")]
    public float goodMultSpeed = 1.1f;
    public float badMultSpeed = 0.9f;

    [Header("---Health---")] 
    public float goodMultHealth = 20f;
    public float badMultHealth = 12f;
    
    [Header("---Damage---")]
    public float goodMultDamage = 2f;
    public float badMultDamage = 1.5f;

    private Controls _controls;
    private WeaponManager _wpManager;
    private PlayerHealth _plHealth;

    private void Awake()
    {
        strength = PlayerSettings.Srenght;
        stealth = PlayerSettings.Stealth;
        agility = PlayerSettings.Agility;
    }
    
    private void Start()
    {
        _controls = transform.GetComponent<Controls>();
        _wpManager = GameObject.Find("CameraHolder/PlayerCamera/WeaponHolder").GetComponent<WeaponManager>();
        _plHealth = transform.GetComponent<PlayerHealth>();
    }

    public void UpdateStats(int str, int stl, int agl)
    {
        strength = str;
        stealth = stl;
        agility = agl;
        
        _controls.UpdateStats();
        _wpManager.UpdateStats();
        _plHealth.UpdateStats();
    }
}
