using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public float timeBetweenBites = 2f;
    private PlayerHealth _plHealth;
    private bool _canBite = true;

    private float _curTime;

    private bool isDead = false;
    private void Start()
    {
        _plHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (isDead) return;
        if (!_canBite)
        {
            _curTime -= Time.deltaTime;
        }

        if (_curTime <= 0)
        {
            _canBite = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDead) return;
        if (other.CompareTag("Player"))
        {
            if (_canBite)
            {
                _plHealth.Damage(15f);
                _canBite = false;
                _curTime = timeBetweenBites;
            }
        }
    }

    public void OnDead()
    {
        isDead = true;
    }
}
