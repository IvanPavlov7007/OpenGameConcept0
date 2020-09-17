using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Vector2 pointA, pointB;
    LineRenderer lr;
    private void Awake()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
    }

    public void SetPoints(Vector2 a, Vector2 b)
    {

        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
    }
}
