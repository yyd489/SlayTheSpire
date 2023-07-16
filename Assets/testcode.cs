using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum MapField
{
    unknown,
    monster,
    eliteMonster,
    shop,
    sleep,
    treasure
}

class Graph
{
    public class Node
    {
       public int data;
       public List<Node> adjacent;
       public bool marked;
       public MapField field;
       public Node(int data)
       {
            this.data = data;
            adjacent = new List<Node>();
       }

    }

    public Node[] nodes;
    public Graph(int size)
    {
        nodes = new Node[size];
        for(int i = 0; i<size; i++)
        {
            nodes[i] = new Node(i);
        }
    }

   public void addEdge(int i1, int i2, int randomField)
   {
        int index;
        
       
        
    }

    //void dfs()
    //{
    //    dfs(0);
        
    //}
    //void dfs(int index)
    //{
    //    Node root = nodes[index];
    //    Stack<Node> stack = new Stack<Node>();
    //    stack.Push(root);
    //    root.marked = true;

    //    while(stack.Count > 1)
    //    {
    //        Node r = stack.Pop();
           
    //        if()
    //    }

    //}
    

}
public class testcode : MonoBehaviour
{
    Graph g = new Graph(16);
    

    private void Start()
    {
       
        g.addEdge(6, 7, Random.Range(0, 6));
        g.addEdge(7, 8, Random.Range(0, 6));
        g.addEdge(8, 9, Random.Range(0, 6));
        g.addEdge(2, 3, Random.Range(0, 6));
        g.addEdge(3, 4, Random.Range(0, 6));
        g.addEdge(4, 5, Random.Range(0, 6));
        g.addEdge(5, 6, Random.Range(0, 6));
        g.addEdge(16, 17, Random.Range(0, 6));
        g.addEdge(17, 18, Random.Range(0, 6));
        g.addEdge(18, 19, Random.Range(0, 6));
        g.addEdge(18, 29, Random.Range(0, 6));
        g.addEdge(19, 20, Random.Range(0, 6));
        g.addEdge(29, 30, Random.Range(0, 6));
        g.addEdge(10, 40, Random.Range(0, 6));
        g.addEdge(20, 40, Random.Range(0, 6));
        g.addEdge(30, 40, Random.Range(0, 6));

        //Debug.Log(g.nodes[0].adjacent.First.Value.data);
        //Debug.Log(g.nodes[0].adjacent.Last.Value.data);
        Debug.Log(g.nodes[1].adjacent[0]);
        Debug.Log(g.nodes[1].adjacent[0].field);
        Debug.Log(g.nodes[1].adjacent[1].field);
        Debug.Log(g.nodes[2].adjacent[2].field);
        //Debug.Log(g.nodes[1].adjacent.Last.Value.data);
        //Debug.Log(g.nodes[0].data);
        //Debug.Log(g.nodes[1].data);
        //Debug.Log(g.nodes[2].data);


    }
}
