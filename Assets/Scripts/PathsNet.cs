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

    public MovingPoint mP;


    [Header("Nodes Settings")]
    public int nodesCountX;
    public int nodesCountY;
    public float nodeDistance;

    public GameObject nodePref, pathPref;

    void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        List<Node> nodes = new List<Node>();
        int curNindex = 0;
        Vector2 initPos = transform.position;

        for (int i = 0; i < nodesCountY; i++)
        {
            for (int j = 0; j < nodesCountX; j++)
            {
                var n = Instantiate(nodePref, initPos + new Vector2(j, i) * nodeDistance, Quaternion.identity).GetComponent<Node>();
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
        mP.nextNode = nodes[1];
        mP.currentWorldDir = nodes[1].transform.position - nodes[0].transform.position;
        mP.transform.position = nodes[0].transform.position;
    }

    void Update()
    {
        
    }
}
