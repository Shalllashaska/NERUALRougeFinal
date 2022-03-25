using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightGenerator : MonoBehaviour
{
    [Range(0,1)]
    public float chance = 0.5f;
    public Light spotlight;
    public GameObject spotlightModel;
    public Material offmaterial;
   
    void Start()
    {
        if (Random.Range(0f, 1f) <= 0.5f)
        {
            if (spotlight != null)
            {
                spotlight.intensity = 0;
                spotlightModel.GetComponent<MeshRenderer>().material = offmaterial;
            }
        }
    }
}
