using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public ArmorMenager armorOfGuard;

    private bool _scriptInit = false;

    private float _currentHealth;

    private bool _isDead = false;
    
    private Image _hitmarkerImage;
    private float _hitmarkerWaitMax = 0.3f;
    private float _hitmarkerWait;

    public bool IsBoss = false;

    private RawImage HealthBar;

    private void Awake()
    {
        _hitmarkerImage = GameObject.Find("Canvas/Hitmarker/Image").GetComponent<Image>();
        _hitmarkerImage.color = new Color(1, 1, 1, 0);
    }

    private void Start()
    {
        if (IsBoss)
        {
            HealthBar = GameObject.Find("Canvas/BossName/Health").GetComponent<RawImage>();
            HealthBar.color = new Color(HealthBar.color.r, HealthBar.color.g, HealthBar.color.b, 1);
        }
    }

    void Update()
    {
        if (!_scriptInit)
        {
            if (IsBoss)
            {
                _scriptInit = true;
                _currentHealth = health;
            }
            else
            {
                if (armorOfGuard.IsInit())
                {
                    if (armorOfGuard.GetHealthMult() > 0)
                    {
                        _currentHealth = health + armorOfGuard.GetHealthMult();
                    }

                    _scriptInit = true;
                }
                else
                {
                    return;
                }
            }
        }
        
        if (_hitmarkerImage.color == Color.white && _hitmarkerWait <=0)
        {
            _hitmarkerWait = _hitmarkerWaitMax;
        }
        
        if (_hitmarkerWait >= 0)
        {
            _hitmarkerImage.color = new Color(1, 1, 1, _hitmarkerWait / _hitmarkerWaitMax);
            _hitmarkerWait -= Time.deltaTime;
        }
        else
        {
            _hitmarkerImage.color = new Color(1, 1, 1, 0);
        }
        
        
    }

    public void Damage(float damage)
    {
        if (_isDead) return;
        Debug.Log("Damage: " + damage);
        _hitmarkerImage.color = new Color(255,255,255,1);
        _currentHealth -= damage;
        UpdateHealth();
        Debug.Log("Health: " + _currentHealth);
        _hitmarkerWait = _hitmarkerWaitMax;
    }

    private void UpdateHealth()
    {
        if (IsBoss)
        {
            if (IsBoss)
            {
                HealthBar.transform.localScale = new Vector3(Math.Max(_currentHealth / health, 0), 1,
                    1);
            }
            if (_currentHealth <= 0)
            {
                _isDead = true;
                SendDeadMessage();
                HealthBar.color = new Color(HealthBar.color.r, HealthBar.color.g, HealthBar.color.b, 0);
                HealthBar.transform.localScale = new Vector3(1, 1,
                    1);
            }
        }
        else
        {
            if (_currentHealth <= 0)
            {
                _isDead = true;
                SendDeadMessage();
            }
            if (_currentHealth <= 40)
            {
                gameObject.GetComponent<Guard>()._isToLowHealth = true;
            }
        }
        
    }

    private void SendDeadMessage()
    {
        if (!IsBoss)
        {
            gameObject.GetComponent<Guard>()._isDead = true;
            gameObject.GetComponent<AimTestScript>()._isDead = true;
            gameObject.GetComponent<RotationOrient>()._isDead = true;
            transform.parent.GetComponent<RoomManager>().EnemyIsDead();
        }
        else
        {
            gameObject.GetComponent<BossMind>().OnDead();
            if (gameObject.transform.Find("AttackZone"))
            {
                gameObject.transform.Find("AttackZone").GetComponent<AttackZone>().OnDead();
            }
            transform.parent.GetComponent<RoomManager>().EnemyIsDead();
        }
    }
}
