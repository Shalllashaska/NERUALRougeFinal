using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public GameObject lvlUpMenu;
    public GameObject pauseMenu;
    public GameObject endGamemenu;
    public GameObject settings;

    private PlayerStats _plStats;
    private WeaponManager _wm;
    private Look _look;

    public Text roomNum;
    public Text seedNum;
    

    private int _str;
    private int _stl;
    private int _agl;
    private int _points;


    private bool _pause;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    
    
    private void Start()
    {
        _plStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _wm = GameObject.Find("CameraHolder/PlayerCamera/WeaponHolder").GetComponent<WeaponManager>();
        _look = GameObject.Find("Player").GetComponent<Look>();
        _pause = false;
        Time.timeScale = 1;
        seedNum.text = GameSettings.Seed.ToString();
        roomNum.color = new Color(roomNum.color.r, roomNum.color.g, roomNum.color.b, 0);
    }


    public void SetNumRoom(string name)
    {
        roomNum.text = name;
        roomNum.color = new Color(roomNum.color.r, roomNum.color.g, roomNum.color.b, 1);
    }

    public void SettingsBut()
    {
        settings.SetActive(true);
    }

    public void closeSettings()
    {
        settings.SetActive(false);
    }
    
    public void UnSetNumRoom()
    {
        roomNum.color = new Color(roomNum.color.r, roomNum.color.g, roomNum.color.b, 0);
    }
    
    public void Pause()
    { 
        PauseNotMenu();
       pauseMenu.SetActive(_pause);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        PlayerSettings.weapon1 = null;
        PlayerSettings.weapon2 = null;
        SceneLoad.SwitchScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseNotMenu()
    {
        _pause = !_pause;
        Cursor.visible = _pause;
        _look.pause = _pause;
        _wm.pause = _pause;
        if (_pause)
        {
          
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    public void Dead()
    {
        _pause = true;
        Cursor.visible = true;
        _look.pause = true;
        _wm.pause = true;
        
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    
    public void PressDone()
    {
        Time.timeScale = 1;
        lvlUpMenu.SetActive(false);
        _str = int.Parse(lvlUpMenu.transform.Find("Strenght/num").GetComponent<Text>().text);
        _stl = int.Parse(lvlUpMenu.transform.Find("Coordination/num").GetComponent<Text>().text);
        _agl = int.Parse(lvlUpMenu.transform.Find("Agility/num").GetComponent<Text>().text);
        _points = int.Parse(lvlUpMenu.transform.Find("Point/num").GetComponent<Text>().text);
        _points++;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lvlUpMenu.transform.Find("Point/num").GetComponent<Text>().text = _points.ToString();
        PauseNotMenu();
        _plStats.UpdateStats(_str, _stl, _agl);
    }

    public void OnSetActive()
    {
        PauseNotMenu();
        lvlUpMenu.transform.Find("Strenght/num").GetComponent<Text>().text = _plStats.strength.ToString();
        lvlUpMenu.transform.Find("Coordination/num").GetComponent<Text>().text = _plStats.stealth.ToString();
        lvlUpMenu.transform.Find("Agility/num").GetComponent<Text>().text = _plStats.agility.ToString();
    }

    public void EndGame()
    {
        Cursor.visible = true;
        _look.pause = true;
        _wm.pause = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        endGamemenu.SetActive(true);
    }
}
