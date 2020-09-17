using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    protected List<Node> connectedNodes = new List<Node>();


    public void ConnectWithNode(Node another)
    {
        connectedNodes.Add(another);
        another.connectedNodes.Add(this);
        var p = Instantiate(PathsNet.Instance.pathPref, Vector3.zero, Quaternion.identity, transform).GetComponent<Path>();
        p.SetPoints(transform.position, another.transform.position);
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

    void Update()
    {
        
    }
}
