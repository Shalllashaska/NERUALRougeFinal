using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndElevator : MonoBehaviour
{
   private CanvasScript _cnvScr;
   private void Start()
   {
      _cnvScr = GameObject.Find("Canvas").GetComponent<CanvasScript>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         _cnvScr.EndGame();
        
      }
   }
}
