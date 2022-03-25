using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class BossMind : MonoBehaviour
{

    public NavMeshAgent agent;
    
    [Range(1,10)]
    public int typeOfBoss = 1; //1 - monkey boos; 2 - big boss

    public Transform attackPoint;
   
    private Transform _plTranform;
    
    public float _restSpeed;
    public float _chaseSpeed;
    public float _acceleration;
    public float _angularSpeed;

    public AudioClip splashAttackClip;
    public AudioClip mindAttackClip;
    
    [Header("---Monkey Boss---")]
    public float timeChasePhase = 6; 
    public float timeRestPhase = 2.5f;
    public float timeBetweenShots = 2f;

    public AudioClip restSound;
    public AudioClip attackSound;
    
   
    
   
    
    [Header("---JERY Boss---")]
    public GameObject projectail;
    [Range(3,15)]
    public int countOfSlash = 5;
    public float timeBetweenSplashShots = 1.5f;
    public GameObject forwardProjectail;
    public Transform attackSplashPoint;
    public bool rotate = true;
    public float angleMult = 30;
    public LayerMask playerLayer;
    public float rayDamage = 15f;
    [Range(1, 10)]
    public float laserSpeed = 5;
    public float speedOfDamage = 0.5f;
    public float laserMult = 0.5f;


    public AudioSource attackSFX;
    public AudioSource laserSource;

    private float _currentTime;
    private float _currentTimeShoot;
    private float _currentTimeShootSplash;
    private float _currentTimeLaserDamage;
    
    //MonkeyBoss
    private bool IsRestTime = true;
    private bool IsDead = false;
    private Text _nameOfBoss;
    
    private float multAngle = 0;
    
    private LineRenderer _lineRenderer;
    private Vector3 _posOfPlayer;
    
    void Start()
    {
        _plTranform = GameObject.Find("Player").transform;
        _nameOfBoss = GameObject.Find("Canvas/BossName").GetComponent<Text>();
        
        agent.speed = _restSpeed;
        agent.acceleration = _acceleration;
        agent.angularSpeed = _angularSpeed;
          
        _currentTime = timeRestPhase;
        _nameOfBoss.color = new Color(_nameOfBoss.color.r, _nameOfBoss.color.g, _nameOfBoss.color.b, 1);
        if (typeOfBoss == 1)
        {
            _nameOfBoss.text = "VINOGRAD";
        }
        if (typeOfBoss == 2)
        {
            laserSource.volume = 0;
            _nameOfBoss.text = "ОЛЕГ";
            _lineRenderer = transform.GetComponent<LineRenderer>();
            _posOfPlayer = Vector3.zero;
            DisableLaser();
        }
    }

    
    void Update()
    {
        if (IsDead) return;
        
        agent.SetDestination(_plTranform.position);

        if (typeOfBoss == 1)
        {
            MonkeyBossMInd();
        }

        if (typeOfBoss == 2)
        {
            JeryBossMind();
        }
    }

    private void JeryBossMind()
    {
        if (IsRestTime)
        {
            if (_currentTime <= 0)
            {
                IsRestTime = false;
                _currentTime = timeChasePhase;
                EnableLaser();
                _currentTimeLaserDamage = 0;
            }
            
            
            laserSource.volume -= 5 * Time.deltaTime;
            
            Stop();
            _currentTimeShoot -= Time.deltaTime;
            _currentTimeShootSplash -= Time.deltaTime;
            if (_currentTimeShoot <= 0)
            {
                ShootMonk();
            }
            
            if(_currentTimeShootSplash <=0)
            {
                SplashShoot();
            }
        }
        else
        {
            if (_currentTime <= 0)
            {
                _currentTime = timeRestPhase;
                _posOfPlayer = Vector3.zero;
                DisableLaser();
                IsRestTime = true;
            }

            laserSource.volume += 5 * Time.deltaTime;
            _currentTimeLaserDamage -= Time.deltaTime;
            LerpPlayerPos();
            ShootRay();
        }
        _currentTime -= Time.deltaTime;
    }

    private void MonkeyBossMInd()
    {
        
        if (IsRestTime)
        {
            if (_currentTime <= 0)
            {
                IsRestTime = false;
                _currentTime = timeChasePhase;
                ChangeSpeed(_chaseSpeed);
                attackSFX.PlayOneShot(attackSound);
            }

            _currentTimeShoot -= Time.deltaTime;
            if (_currentTimeShoot <= 0)
            {
                ShootMonk();
            }
        }
        else
        {
            if (_currentTime <= 0)
            {
                IsRestTime = true;
                _currentTime = timeRestPhase;
                ChangeSpeed(_restSpeed);
                attackSFX.PlayOneShot(restSound);
            }
        }
        _currentTime -= Time.deltaTime;
    }

    private void ChangeSpeed(float sp)
    {
        agent.speed = sp;
    }
    
    private void Stop()
    {
        agent.SetDestination(transform.position);
    }

    private void ShootMonk()
    {
        attackSFX.PlayOneShot(mindAttackClip);
        Instantiate(projectail, attackPoint.position, quaternion.LookRotation((_plTranform.position - attackPoint.position).normalized,Vector3.forward));
        _currentTimeShoot = timeBetweenShots;
    }

    private void LerpPlayerPos()
    {
        _posOfPlayer = Vector3.Lerp(_posOfPlayer, _plTranform.position, laserSpeed * laserMult* Time.deltaTime);
    }

    private void ShootRay()
    {
        RaycastHit hit;
        _lineRenderer.SetPosition(0, attackPoint.position);
        Vector3 dir = (_posOfPlayer - attackPoint.position).normalized;
        Ray ray = new Ray(attackPoint.position, dir);
        if(Physics.Raycast(ray, out hit, 100f))
        {
            _lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                if (_currentTimeLaserDamage <= 0)
                {
                    hit.collider.GetComponent<PlayerHits>().OnHit(rayDamage);
                    _currentTimeLaserDamage = speedOfDamage;
                }
            }
        }
    }

    private void EnableLaser()
    {
        _lineRenderer.enabled = true;
    }

    private void DisableLaser()
    {
        _lineRenderer.enabled = false;
    }

    private void SplashShoot()
    {
        Quaternion nextRot;
        float angle = 360 / countOfSlash;
        
        for (int i = 0; i < countOfSlash; i++)
        {
            nextRot = Quaternion.AngleAxis(angle * i + multAngle, Vector3.up);
            Instantiate(forwardProjectail, attackSplashPoint.position, nextRot);
        }

        attackSFX.PlayOneShot(splashAttackClip);
        if (rotate)
        {
            multAngle += angleMult;
        }
        
        _currentTimeShootSplash = timeBetweenSplashShots;
    }

    public void OnDead()
    {
        IsDead = true;
        _nameOfBoss.color = new Color(_nameOfBoss.color.r, _nameOfBoss.color.g, _nameOfBoss.color.b, 0);
        agent.SetDestination(transform.position);

        Destroy(gameObject, 5f);
    }
}
