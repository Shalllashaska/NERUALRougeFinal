using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayCamera : MonoBehaviour
{
    #region Variables
    //Public
    public float intesity;
    public float smooth;
    public float div;
    public float mult;
    public Transform aroundPoint;

    //Private 
    private Quaternion originRotation;
    private Vector3 originPos;

    #endregion

    #region Monobehavior Callbacks

    private void Start()
    {
        originRotation = transform.localRotation;
        originPos = transform.localPosition;
    }

    private void Update()
    {
        UpdateSway();
    }

    #endregion

    #region Private Methods
    
    private void UpdateSway()
    {
        
        //Controls
        float xMouse = Input.mousePosition.x - Screen.width / 2;
        float yMouse = Input.mousePosition.y- Screen.height / 2;

        Vector3 targetPos;

        if (Math.Abs(xMouse) > 300 || Math.Abs(yMouse) > 300)
        {
            targetPos = originPos + new Vector3((-xMouse-300)/div,(-yMouse-300)/div, 0); //
        }
        else
        {
            targetPos = originPos;
        }
       
        transform.localPosition = Vector3.Slerp(transform.localPosition, targetPos, smooth * mult * Time.deltaTime);
        Quaternion lookRot = Quaternion.LookRotation(aroundPoint.position, transform.localPosition);
        
        Quaternion.Slerp(transform.localRotation, lookRot, smooth * smooth * smooth * Time.deltaTime);


    }

    #endregion
}
