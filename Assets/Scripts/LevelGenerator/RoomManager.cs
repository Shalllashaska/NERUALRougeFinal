using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;
using RandomUnit = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    enum StateOfFight {
        None,
        SpawnEnemies,
        Fight,
        EndOfFight
    }
    [Header("---Boss Room---")]
    public Transform spawnBoss;
    public GameObject[] bosses;
    public bool IsBossRoom = false;
    public GameObject elevator;
    public GameObject bigReward;
    public Transform spawnElevator;
    public Transform spawnBigReward;
    
    
    [Header("---Regular Room---")]
    public Transform[] spawnEnemyPoints;
    public Transform rewardSpawnPoint;
    
    public Transform parentDoors;
    public GameObject enemyPrefab;
    public GameObject healPrefab;
    public GameObject ammoPrefab;

    public GameObject[] guns;
    
    private StateOfFight currentState;
    private Random random;

    private bool roomAlreadyClean = false;
    private bool enemyAlreadySpawn = false;
    private int deadEnemys = 0;
    private int currentCountOfEnemy;
    private CanvasScript _canvasScript;
    
    // Start is called before the first frame update
    void Start()
    {
        random = new Random(GameSettings.Seed);
        _canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        currentState = StateOfFight.None;
    }
    public void SpawnEnemies()
    {
        enemyAlreadySpawn = true;
        if (currentState == StateOfFight.None)
        {
            currentState = StateOfFight.SpawnEnemies;
        }

        currentCountOfEnemy = 0;
        if (IsBossRoom)
        {
            int dice = RandomUnit.Range(0, bosses.Length);
            Debug.Log(bosses.Length);
            Instantiate(bosses[dice], spawnBoss.position, Quaternion.identity, transform);
            currentCountOfEnemy++;
        }
        else
        {
            int numberOfRoom = gameObject.name[0];
        
            for (int i = 0; i < spawnEnemyPoints.Length; i++)
            {
                double dice = random.NextDouble();
                if (dice <= 0.8)
                {
                    currentCountOfEnemy++;
                    GameObject en = Instantiate(enemyPrefab, spawnEnemyPoints[i].position, Quaternion.identity, transform);
                    Guard scrGuard = en.GetComponent<Guard>();
                    
                    if (spawnEnemyPoints[i].childCount > 0)
                    {
                        scrGuard.pathHolder = spawnEnemyPoints[i].GetChild(0);
                        scrGuard.enemyWithPath = true;
                    }
                    else
                    {
                        scrGuard.enemyWithPath = false;
                    }
                
                    scrGuard.CalculateMind();
               
                }
            }
        }
        
        if (currentCountOfEnemy > 0)
        {
            currentState = StateOfFight.Fight;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") ) return;
        
        _canvasScript.UnSetNumRoom();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (roomAlreadyClean) return;
        if (!other.CompareTag("Player") ) return;

        _canvasScript.SetNumRoom(gameObject.name);
        
        if (currentState == StateOfFight.Fight)
        {
            for (int i = 0; i < parentDoors.childCount; i++)
            {
                Transform doorTr = parentDoors.GetChild(i);
                DoorScript door = doorTr.GetChild(0).GetComponent<DoorScript>();
                door.active = false;
                door.Close();
            }
        }
        else if (currentState == StateOfFight.None)
        {
            SpawnEnemies();
            if (currentState == StateOfFight.Fight)
            {
                for (int i = 0; i < parentDoors.childCount; i++)
                {
                    Transform doorTr = parentDoors.GetChild(i);
                    DoorScript door = doorTr.GetChild(0).GetComponent<DoorScript>();
                    door.active = false;
                    door.Close();
                }
            }
        }
    }

    public bool EnemyIsSpawn()
    {
        return enemyAlreadySpawn;
    }

    private void OpenDoors()
    {
        if (currentState == StateOfFight.EndOfFight)
        {
            for (int i = 0; i < parentDoors.childCount; i++)
            {
                Transform doorTr = parentDoors.GetChild(i);
                DoorScript door = doorTr.GetChild(0).GetComponent<DoorScript>();
                door.active = true;
                if (door._isHaveBeenOpened)
                {
                    door.Open();
                }
            }
        }
    }

    private void SpawnReward()
    {
        if (currentState == StateOfFight.EndOfFight)
        {
            if (RandomUnit.Range(0f, 1f) <= 0.35)
            {
                Instantiate(healPrefab, rewardSpawnPoint.position, Quaternion.identity, transform);
            }
            
            if (RandomUnit.Range(0f, 1f)  <= 0.55)
            {
                Instantiate(ammoPrefab, rewardSpawnPoint.position, Quaternion.identity, transform);
            }
            
            if (IsBossRoom)
            {
                Instantiate(elevator, spawnElevator.position, Quaternion.identity, transform);
                Instantiate(bigReward, spawnBigReward.position, Quaternion.identity, transform);
            }
            else
            {
                if (RandomUnit.Range(0f, 1f)  <= 0.2)
                {
                    int dice = RandomUnit.Range(0, guns.Length);
                    Instantiate(guns[dice], rewardSpawnPoint.position, Quaternion.identity, transform);
                }
            }
        }
    }
    
    public void EnemyIsDead()
    {
        deadEnemys++;
        if (currentCountOfEnemy == deadEnemys)
        {
            currentState = StateOfFight.EndOfFight;
            OpenDoors();
            SpawnReward();
        }
    }
}
