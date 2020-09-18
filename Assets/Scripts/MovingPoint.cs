using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    public float velocity;
    public Vector2 nextDirection;

    public Node currentNode, nextNode;
    public Vector2 currentWorldDir;

    public Joystick j;

    Transform arrowRotationPoint;

    void Start()
    {
        j = FindObjectOfType<Joystick>();
        arrowRotationPoint = transform.GetChild(0);
    }

    void LateUpdate()
    {
        nextDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (j.Value.magnitude > 0f)
            nextDirection = j.Value;

        if (nextDirection.magnitude <= 0.1f)
            nextDirection = Vector2.up;
        else
            nextDirection.Normalize();

        Vector2 nextWorldDirection = Quaternion.FromToRotation(Vector3.up, nextDirection) * currentWorldDir;

        arrowRotationPoint.rotation = Quaternion.Lerp(arrowRotationPoint.rotation,  Quaternion.FromToRotation(Vector2.up, nextWorldDirection),10f * Time.deltaTime);

        //if (Input.GetKey(KeyCode.A))
        //    nextDirection = Vector2.left;
        //if (Input.GetKey(KeyCode.D))
        //    nextDirection = Vector2.right;
        //if (Input.GetKey(KeyCode.W))
        //    nextDirection = Vector2.up;

        Vector3 nextNPos = nextNode.transform.position;
        float dif = (transform.position - nextNPos).magnitude - velocity * Time.deltaTime;
        if(dif < 0)
        {
            currentNode = nextNode;
            transform.position = nextNPos;
            nextNode = currentNode.GetNextNode(nextWorldDirection);
            currentWorldDir = nextNode.transform.position - nextNPos;
            nextDirection = Vector2.up;
        }
        else
            transform.position = nextNPos + (transform.position - nextNPos).normalized * dif;
    }
}
