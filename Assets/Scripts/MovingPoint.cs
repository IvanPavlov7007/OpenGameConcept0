using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    public float velocity;
    public Vector2 nextDirection;

    public Node currentNode, nextNode;
    public Vector2 currentWorldDir;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            nextDirection = Vector2.left;
        if (Input.GetKey(KeyCode.D))
            nextDirection = Vector2.right;
        if (Input.GetKey(KeyCode.W))
            nextDirection = Vector2.up;
        
        Vector3 nextNPos = nextNode.transform.position;
        float dif = (transform.position - nextNPos).magnitude - velocity * Time.deltaTime;
        if(dif < 0)
        {
            currentNode = nextNode;
            transform.position = nextNPos;
            nextNode = currentNode.GetNextNode( Quaternion.FromToRotation(Vector3.up, nextDirection) * currentWorldDir);
            currentWorldDir = nextNode.transform.position - nextNPos;
            nextDirection = Vector2.up;
        }
        else
            transform.position = nextNPos + (transform.position - nextNPos).normalized * dif;
    }
}
