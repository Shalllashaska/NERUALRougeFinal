using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : MonoBehaviour
{

    public GameObject tooltip;

    public void Show()
    {
        tooltip.SetActive(true);
    }


    public void SetText(string _header, string _content)
    {
        tooltip.transform.Find("Header").GetComponent<Text>().text = _header;
        tooltip.transform.Find("Content").GetComponent<Text>().text = _content;
    }
    
    
    public void Hide()
    {
        tooltip.SetActive(false);
    }
}
