using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    public float smoothing;

    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;

    private void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            //set our start point
            origin = data.position;
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if(data.pointerId == pointerID)
        {
            //compare the difference between our start point and current position
            Vector2 currentPosition = data.position;

            Vector2 directionRaw = currentPosition - origin;

            //normalize it to make sure they don't move crazy fast.
            direction = directionRaw.normalized;
        }

    }

    public void OnPointerUp(PointerEventData data)
    {
        if(data.pointerId == pointerID)
        {
            //reset everything
            direction = Vector2.zero;
            touched = false;
        }
    }

    public Vector2 GetDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
        return smoothDirection;
    }
}
