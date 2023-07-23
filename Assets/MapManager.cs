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

            int maxYsize = 40;
            int minYsize = -40;
            string objectPath = "Assets/Prefabs/UI/MapNode.prefab";
            int[] arrNodeXPosition = { 100,0,-100,-200,-300,-400};

            for (int i = 1; i < listNodeZones.Count; i++)//오브젝트 생성 
            {
                Transform parent = listNodeZones[i].GetComponent<Transform>();
                int randomNodeSize = Random.Range(1, 6);

                for (int z = 0; z < randomNodeSize; z++)
                {
      
                    float randomY = Random.Range(minYsize, maxYsize);
                    UImanager.InstansUI(objectPath, new Vector2(parent.position.x + arrNodeXPosition[randomNodeSize] + Random.Range(-30,30) + z * 200
                        ,parent.position.y + randomY), parent);
                }

            }
            
        }

        public void Start()
        {
            Init();
        }

    }
}
