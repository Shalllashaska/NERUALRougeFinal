using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenu : MonoBehaviour
{
    [Header("---Camera settings---")]
    public Transform cameraMain;
    public Transform firstPos;
    public Transform secondPos;
    public Transform thirdPos;
    public Transform fourPos;
    [Range(1,20)]
    public float speedOfChangePos = 5;

    public float mulSpeed = 0.2f;
    
    [Header("---Menus---")]
    public GameObject mainMenu;
    public GameObject newGameMenu;
    public GameObject quiteMenu;
    public GameObject characterChangeMenu;
    public GameObject settingsMenu;
    

    [Header("---Start Game---")] 
    public Text numStrenght;
    public Text numAgility;
    public Text numStealth;
    public Dropdown weaponDropdown;
    public InputField seed;

    [Header("---Start Game Guns---")] 
    public Gun pistol;
    public Gun knife;
    public Gun pipe;
    public Gun plasmaCutter;
    public Gun rifle;
    public Gun shotgun;
    
    public Armor stnd;
    public Armor engn;
    public Armor cmbt;

    private List<GameObject> _menuWindows = new List<GameObject>();
    private Gun curGun;

    private bool first = false;
    private bool second = false;
    private bool third = false;
    private bool four = false;

    private string seedText = "";
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        _menuWindows.Add(newGameMenu);
        _menuWindows.Add(quiteMenu);
        _menuWindows.Add(characterChangeMenu);
        _menuWindows.Add(settingsMenu);
        
        OpenMainMenu();
    }

    public void OpenQuiteWindow()
    {
        AllFalse();
        third = true;
        CloseAllMenu();
        quiteMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenCharacterChange()
    {
        CloseAllMenu();
        characterChangeMenu.SetActive(true);
        SetPistol();
    }
    
    public void OpenNewGameWindow()
    {
        AllFalse();
        second = true;
        CloseAllMenu();
        newGameMenu.SetActive(true);
    }

    public void OpenMainMenu()
    {
        AllFalse();
        first = true;
        CloseAllMenu();
    }

    public void OpenSettingsWindow()
    {
        AllFalse();
        four = true;
        CloseAllMenu();
        settingsMenu.SetActive(true);
    }
    
    private void CloseAllMenu()
    {
        foreach (GameObject window in _menuWindows)
        {
            window.SetActive(false);
        }
    }

    public void StartGameSoldier()
    {
        PlayerSettings.Srenght = 2;
        PlayerSettings.Agility = 2;
        PlayerSettings.Stealth = 5;
        SetRifle();
        PlayerSettings.weapon1 = curGun;
        SetKnife();
        PlayerSettings.weapon2 = curGun;
        PlayerSettings.typeArmor = 3;
        PlayerSettings.armorPref = cmbt;
        LoadLevel();
    }
    
    public void StartGameLoader()
    {
        PlayerSettings.Srenght = 5;
        PlayerSettings.Agility = 2;
        PlayerSettings.Stealth = 2;
        SetShotgun();
        PlayerSettings.weapon1 = curGun;
        SetPipe();
        PlayerSettings.weapon2 = curGun;
        PlayerSettings.typeArmor = 1;
        PlayerSettings.armorPref = stnd;
        LoadLevel();
    }
    
    public void StartGameEngineer()
    {
        PlayerSettings.Srenght = 2;
        PlayerSettings.Agility = 5;
        PlayerSettings.Stealth = 2;
        SetPistol();
        PlayerSettings.weapon1 = curGun;
        SetPlasmaCutter();
        PlayerSettings.weapon2 = curGun;
        PlayerSettings.typeArmor = 2;
        PlayerSettings.armorPref = engn;
        LoadLevel();
    }
    

    public void StartGameYour()
    {
        PlayerSettings.Srenght = int.Parse(numStrenght.text);
        PlayerSettings.Agility = int.Parse(numAgility.text);
        PlayerSettings.Stealth = int.Parse(numStealth.text);
        PlayerSettings.weapon1 = curGun;
        PlayerSettings.typeArmor = 1;
        PlayerSettings.armorPref = stnd;
        LoadLevel();
    }


    public void ChangeWeapon()
    {
        switch (weaponDropdown.value)
        {
            case 0:
                SetPistol();
                break;
            case 1:
                SetKnife();
                break;
            case 2:
                SetPipe();
                break;
            case 3:
                SetPlasmaCutter();
                break;
        }
    }

    #region Set Guns

    public void SetPistol()
    {
        curGun = pistol;
    }

    public void SetKnife()
    {
        curGun = knife;
    }
    
    public void SetPipe()
    {
        curGun = pipe;
    }
    
    public void SetPlasmaCutter()
    {
        curGun = plasmaCutter;
    }

    public void SetShotgun()
    {
        curGun = shotgun;
    }

    public void SetRifle()
    {
        curGun = rifle;
    }
    
    #endregion
    
    public void LoadLevel()
    {
        int seedInt;
        if (seed.text == "")
        {
            seedInt = Random.Range(0, 10000);
        }
        else
        {
            seedInt = int.Parse(seed.text);
        }

        CloseAllMenu();
        GameSettings.Seed = seedInt;
        SceneLoad.SwitchScene("levelGenerator3D");
    }

    private void AllFalse()
    {
        first = false;
        second = false;
        third = false;
        four = false;
    }

    public void InputSeed()
    {
        seedText = seed.text;
    }
    private void Update()
    {
        if (first)
        {
            cameraMain.position = Vector3.Slerp(cameraMain.position, firstPos.position, speedOfChangePos * mulSpeed *  Time.deltaTime);
            cameraMain.rotation = Quaternion.Slerp(cameraMain.rotation, firstPos.rotation, speedOfChangePos *mulSpeed *  Time.deltaTime);
            if ((Vector3.Distance(cameraMain.position, firstPos.position) <= 1)  && (Quaternion.Angle(cameraMain.rotation, firstPos.rotation) <= 1))
            {
                AllFalse();
            }
        }
        else if (second)
        {
            cameraMain.position = Vector3.Slerp(cameraMain.position, secondPos.position, speedOfChangePos *mulSpeed *  Time.deltaTime);
            cameraMain.rotation = Quaternion.Slerp(cameraMain.rotation, secondPos.rotation, speedOfChangePos * mulSpeed * Time.deltaTime);
            if ((Vector3.Distance(cameraMain.position ,secondPos.position) <=1) &&(Quaternion.Angle(cameraMain.rotation,  secondPos.rotation)<=1))
            {
                AllFalse();
            }
        }
        else if (third)
        {
            cameraMain.position = Vector3.Slerp(cameraMain.position, thirdPos.position, speedOfChangePos * mulSpeed * Time.deltaTime);
            cameraMain.rotation = Quaternion.Slerp(cameraMain.rotation, thirdPos.rotation, speedOfChangePos * mulSpeed * Time.deltaTime);
            if ((Vector3.Distance(cameraMain.position ,thirdPos.position )<=1)&& (Quaternion.Angle(cameraMain.rotation, thirdPos.rotation)<=1))
            {
                AllFalse();
            }
        }
        else if (four)
        {
            cameraMain.position = Vector3.Slerp(cameraMain.position, fourPos.position, speedOfChangePos * mulSpeed * Time.deltaTime);
            cameraMain.rotation = Quaternion.Slerp(cameraMain.rotation,   fourPos.rotation, speedOfChangePos * mulSpeed * Time.deltaTime);
            if ((Vector3.Distance(cameraMain.position ,fourPos.position )<=1)&& (Quaternion.Angle(cameraMain.rotation, fourPos.rotation)<=1))
            {
                AllFalse();
            }
        }
    }
}
