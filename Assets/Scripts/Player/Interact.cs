using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Interact : MonoBehaviour
{
    public bool activePresentation = true;
    public float distanceToInteract = 3.3f;

    public AudioSource source;

    public AudioClip ammoInteract;
    public AudioClip healInteract;
    public AudioClip lvlUpInteract;

    public GameObject flashLight;
    
    
    private GameObject interact;
    private RaycastHit _hit;
    private Ray _ray;

    private PlayerHealth _playerHealth;
    private WeaponManager _playerWeaponManager;
    private GameObject _lvlUpMenu;

    private void Start()
    {
        interact = GameObject.Find("Canvas/Interact");
        _lvlUpMenu = GameObject.Find("Canvas/levelUpMenu");
        _lvlUpMenu.SetActive(false);
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _playerWeaponManager = GameObject.Find("CameraHolder/PlayerCamera/WeaponHolder").GetComponent<WeaponManager>();
        interact.SetActive(false);
    }

    void Update()
    {
        Ray();
        DoorInteract();
        HealInteract();
        AmmoInteract();
        LVLUpInteract();
        FlashLight();
    }

    private void Ray()
    {
        _ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(_ray, out _hit, distanceToInteract);
    }


    private void DoorInteract()
    {

        if (_hit.transform != null && _hit.transform.GetComponent<DoorScript>())
        {
            interact.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                _hit.transform.GetComponent<DoorScript>().Open();
            }
        }
        else
        {
            interact.SetActive(false);
        }
    }
    
    private void HealInteract()
    {
        if (_hit.transform != null && _hit.collider.CompareTag("Heal"))
        {
            interact.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                source.PlayOneShot(healInteract);
                _playerHealth.Heal();
                Destroy(_hit.transform.gameObject);
            }
        }
    }

    public void FlashLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashLight.SetActive(!flashLight.activeSelf);
        }
    }
    
    private void AmmoInteract()
    {
        if (_hit.transform != null && _hit.collider.CompareTag("Ammo"))
        {
            interact.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                source.PlayOneShot(ammoInteract);
                int ammo = Random.Range(15, 45);
                _playerWeaponManager.RefillAmmo(ammo);
                Destroy(_hit.transform.gameObject);
            }
        }
    }

    private void LVLUpInteract()
    {
        if (_hit.transform != null && _hit.collider.CompareTag("lvlUp"))
        {
            interact.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                source.PlayOneShot(lvlUpInteract);
                _lvlUpMenu.SetActive(true);
                _lvlUpMenu.GetComponentInParent<CanvasScript>().OnSetActive();
                
                interact.SetActive(false);
                Destroy(_hit.collider.gameObject);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
