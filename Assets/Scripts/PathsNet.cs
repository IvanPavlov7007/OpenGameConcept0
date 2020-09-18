using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsNet : MonoBehaviour
{
    static PathsNet inst;
    public static PathsNet Instance
    {
        get {
            return inst;    
        }
    }

    public MovingPoint playerPoint;

    public GameObject nodePref, pathPref;

    [Header("Nodes Settings")]
    public int nodesCountX;
    public int nodesCountY;
    public float nodeDistance;
    public float maxDestinationRaidius = 0.5f, maxNextDestinationTime = 3f;




    void Awake()
    {
        inst = this;
    }
    List<Node> nodes;
    private void Start()
    {
        nodes = new List<Node>();
        int curNindex = 0;
        Vector2 initPos = transform.position;

        for (int i = 0; i < nodesCountY; i++)
        {
            for (int j = 0; j < nodesCountX; j++)
            {
                var n = Instantiate(nodePref, initPos + new Vector2(j, i) * nodeDistance, Quaternion.identity).GetComponent<Node>();
                //n.maxDestinationRaidius = maxDestinationRaidius;
                //n.max
                nodes.Add(n);
                if (j > 0)
                {
                    n.ConnectWithNode(nodes[curNindex - 1]);
                }
                if (i > 0)
                {
                    n.ConnectWithNode(nodes[curNindex - nodesCountX]);
                }
                curNindex++;
            }
        }
        playerPoint.nextNode = nodes[1];
        playerPoint.currentWorldDir = nodes[1].transform.position - nodes[0].transform.position;
        playerPoint.transform.position = nodes[0].transform.position;
    }

    void Update()
    {
        
    }
}
