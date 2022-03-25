using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public TooltipSystem current;
   public string header;
   [TextArea(30,100)]
   public string content;
   

   public void OnPointerEnter(PointerEventData enevtData)
   {
      current.Show();
      current.SetText(header,content);
   }
   
   public void OnPointerExit(PointerEventData enevtData)
   {
      
   }
}
