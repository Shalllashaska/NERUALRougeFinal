using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStatsInMenu : MonoBehaviour
{
   public Text points;

   private Text num;

   private void Start()
   {
      num = transform.GetChild(0).GetComponent<Text>();
   }

   public void Less()
   {
      if (int.Parse(num.text) > 1)
      {
         int curNum = int.Parse(num.text);
         curNum--;
         num.text = curNum.ToString();
         
         int curPounts = int.Parse(points.text);
         curPounts++;
         points.text = curPounts.ToString();
      }
   }

   public void More()
   {
      if (int.Parse(points.text) >= 1)
      {
         int curNum = int.Parse(num.text);
         curNum++;
         num.text = curNum.ToString();

         int curPounts = int.Parse(points.text);
         curPounts--;
         points.text = curPounts.ToString();
      }
   }
}
