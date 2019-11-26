using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    private bool touched;
    private int pointerID;
    private bool canFire;

    private void Awake()
    {
        touched = false;
        canFire = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            //set our start point
            canFire = true;
        }
    }


    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            //reset everything
            canFire = false;
            touched = false;
        }
    }

    public bool CanFire()
    {
        return canFire;
    }
}
