using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class MapGraph
    {
        public class Node
        {
            
            public List<Node> listAdjacent;
            public bool marked;
            public MapField field;

            public Node()
            {
                listAdjacent = new List<Node>();
            }

        }

        public Node[] arrNodes;

        public MapGraph(int size)
        {
            arrNodes = new Node[size];

            for (int i = 0; i < arrNodes.Length; i++)
            {
                arrNodes[i] = new Node();

            }

        }

        public void addEdge(Node i1, Node i2)
        {
            if (!i1.listAdjacent.Contains(i2))
            {
                i1.listAdjacent.Add(i2);
            }

            if (!i2.listAdjacent.Contains(i1))
            {
                i2.listAdjacent.Add(i1);
            }

        }

    }
    public enum MapField
    {
        unknown,
        monster,
        eliteMonster,
        shop,
        sleep,
        treasure
    }


    public class MapManager : MonoBehaviour
    {
        public Transform mapObject;

        public MapGraph mapGraph;

        public GameObject node;
        //MapGraph g = new MapGraph(16);
        public List<GameObject> listNodeZones;
        public List<GameObject> listNodes;

        public void Init()
        {
            for(int i = 0; i < mapObject.childCount; i++)
            {
                listNodeZones.Add(mapObject.GetChild(i).gameObject);

            }

            int randomNodeSize = Random.Range(0,6);
            //for(int i = 0; i < randomNodeSize; i++)
            //UImanager.InstansUI(node,new Vector2(,))
        }

        public void Start()
        {
            Init();
        }

    }
}
