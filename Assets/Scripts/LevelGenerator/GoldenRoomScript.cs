using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenRoomScript : MonoBehaviour
{
    public GameObject[] guns;
    
    
    
    void Start()
    {
        int dice = Random.Range(0, guns.Length - 1);
        Debug.Log(dice);
        Instantiate(guns[dice], transform.Find("Piedistal Gun/Spawn Gun").position, Quaternion.identity, transform);
    }

   
}
