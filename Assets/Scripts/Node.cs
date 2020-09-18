using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    protected List<Node> connectedNodes = new List<Node>();

    Vector2 initPos;

    void Start()
    {
        initPos = transform.position;
        curDestination = initPos;
    }

    public void ConnectWithNode(Node another)
    {
        connectedNodes.Add(another);
        another.connectedNodes.Add(this);
        var p = Instantiate(PathsNet.Instance.pathPref, Vector3.zero, Quaternion.identity, transform).GetComponent<Path>();
        p.SetPoints(transform, another.transform);
    }

    public Vector2 GetPath(Vector2 nextDir)
    {
        Vector2 dif, minDif = Vector2.zero, pos = transform.position;
        int i = 0, minI = 0;

        do
        {
            dif = nextDir - ((Vector2)connectedNodes[i].transform.position - pos).normalized;
            if (i == 0 || dif.magnitude < minDif.magnitude)
            {
                minDif = dif;
                minI = i;
            }

        } while (i < connectedNodes.Count);
        return (Vector2)connectedNodes[minI].transform.position - pos;
    }

    public Node GetNextNode(Vector2 nextDir)
    {
        Vector2 dif, minDif = Vector2.zero, pos = transform.position;
        int i = 0, minI = 0;

        do
        {
            dif = nextDir - ((Vector2)connectedNodes[i].transform.position - pos).normalized;
            if (i == 0 || dif.magnitude < minDif.magnitude)
            {
                minDif = dif;
                minI = i;
            }
            i++;
        } while (i < connectedNodes.Count);
        return connectedNodes[minI];
    }


    public float maxDestinationRaidius = 0.5f,maxNextDestinationTime = 3f;
    Vector2 curDestination, lastDestination;
    float destinationTime = 0f, t = 0f;
    void Update()
    {
        if(t>= destinationTime)
        {
            t = 0f;
            lastDestination = transform.position;
            curDestination = Random.insideUnitCircle * maxDestinationRaidius + initPos;
            destinationTime = Random.Range(0, maxNextDestinationTime);
        }
        t += Time.deltaTime;
        transform.position = Vector2.Lerp(lastDestination, curDestination, t / maxNextDestinationTime);
    }
}
