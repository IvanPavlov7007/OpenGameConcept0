using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    public float velocity;
    public Vector2 nextDirection = Vector2.zero;

    public Node currentNode, nextNode;
    [Range(0f,1f)]
    public float relativePosBetweenNodes;

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
        nextDirection = j.Value;

        float lerpV = 10f * Time.deltaTime;
        arrowRotationPoint.rotation = Quaternion.Lerp(arrowRotationPoint.rotation,  Quaternion.FromToRotation(Vector2.up, nextDirection.normalized),lerpV);
        arrowRotationPoint.localScale = new Vector3(1f, Mathf.Lerp(arrowRotationPoint.localScale.y, nextDirection.magnitude, lerpV), 1f);


        Vector2 nextNPos = nextNode.transform.position;
        Vector2 curNPos = currentNode.transform.position;

        Vector2 curToNextVec = (nextNPos - curNPos);

        float dot = Vector2.Dot(nextDirection, curToNextVec);

        if (dot < 0)
        {
            Node c = currentNode;
            currentNode = nextNode;
            nextNode = c;
            relativePosBetweenNodes = 1f - relativePosBetweenNodes;
            curToNextVec *= -1f;
            Vector2 vC = curNPos;
            curNPos = nextNPos;
            nextNPos = curNPos;
        }


        //Vector2 actualVelocity = curToNextVec.normalized * ( dot/ nextDirection.magnitude);
        //Vector2 nextStep = actualVelocity * Time.deltaTime;
        //float nextStepVal = Mathf.Sign(dot) * nextStep.magnitude / curToNextVec.magnitude;

        Vector2 newStandingPoint = curNPos + curToNextVec * relativePosBetweenNodes;

        Vector2 nextStepInDir = nextDirection * Time.deltaTime * velocity;
        Vector2 nextIdealPos = newStandingPoint + nextStepInDir;

        float distToNextNode, lastDist;

        distToNextNode = (nextIdealPos - nextNPos).magnitude;
        lastDist = (nextIdealPos - newStandingPoint).magnitude;

        while(distToNextNode<= lastDist)
        {
            currentNode = nextNode;
            nextNode = nextNode.GetNextNode(nextDirection);
            lastDist = distToNextNode;
            distToNextNode = (nextIdealPos - (Vector2)nextNode.transform.position).magnitude;
        }
        nextNPos = nextNode.transform.position;
        curNPos = currentNode.transform.position;

        curToNextVec = nextNPos - curNPos;
        newStandingPoint = CommonTools.GetPerpendicularPointFromPointToLine(nextIdealPos, nextNPos, curNPos);
        Vector2 curToNewStand = newStandingPoint - curNPos;
        if (Vector2.Dot(curToNextVec, curToNextVec) <= 0)
            relativePosBetweenNodes = 0f;
        else if (curToNewStand.magnitude < curToNextVec.magnitude)
            relativePosBetweenNodes = curToNewStand.magnitude / curToNextVec.magnitude;
        else
            relativePosBetweenNodes = 1f;

        transform.position = curNPos + curToNextVec * relativePosBetweenNodes;

        //Vector2 distToNextPos = (Vector2)transform.position - nextNPos;
        //float dif = distToNextPos.magnitude - velocity * Time.deltaTime;
        //if(dif < 0)
        //{
        //    currentNode = nextNode;
        //    transform.position = nextNPos;
        //    nextNode = currentNode.GetNextNode(nextDirection);
        //    nextDirection = Vector2.up;
        //}
        //else
        //    transform.position = nextNPos + distToNextPos.normalized * dif;
    }
}
