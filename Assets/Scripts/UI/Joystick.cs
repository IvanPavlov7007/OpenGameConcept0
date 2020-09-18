using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(JoystickSettings))]
public class Joystick : EventTrigger
{
    Vector2 initPos, mousePosDif;
    private bool dragStarted;

    public float maxDist;

    private Vector2 _value;
    public Vector2 Value
    {
        get { return _value; }
        private set { }
    }

    void Start()
    {
        initPos = transform.position;
    }

    public void Update()
    {
        if (dragStarted)
        {
            Vector2 newRelativePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - mousePosDif - initPos;
            if (newRelativePos.magnitude > maxDist)
                newRelativePos = newRelativePos.normalized * maxDist;
            transform.position = newRelativePos + initPos;
        }
        else
            transform.position = initPos;
        _value = ((Vector2)transform.position - initPos) / maxDist;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
        mousePosDif = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - (Vector2)transform.position;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
    }
}
