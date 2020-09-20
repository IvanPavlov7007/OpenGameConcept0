using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpendicularTest : MonoBehaviour
{
    public Transform A, B, C, D;


    void Update()
    {
        D.position = CommonTools.GetPerpendicularPointFromPointToLine(C.position, A.position, B.position);
    }
}
