﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform pointA, pointB;
    public Color col = Color.white;
    LineRenderer lr;
    private void Awake()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.startColor = col;
        lr.endColor = col;
    }

    public void SetPoints(Transform a, Transform b)
    {
        pointA = a;
        pointB = b;
    }

    private void LateUpdate()
    {
        lr.SetPosition(0, pointA.position);
        lr.SetPosition(1, pointB.position);
    }
}
