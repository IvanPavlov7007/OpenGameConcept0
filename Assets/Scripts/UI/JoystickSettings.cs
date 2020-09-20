using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Joystick))]
public class JoystickSettings : MonoBehaviour
{
    public float maxDragRadius;
    public Vector2 Result;
    Joystick j;
    void Awake()
    {
        j = GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Start()
    {
        j.maxDist = maxDragRadius;
        Result = j.Value;
    }
}
