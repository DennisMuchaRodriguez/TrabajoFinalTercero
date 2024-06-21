using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrafosController : MonoBehaviour
{
    public GameObject[] nodes;
    public EnemyPatroller enemy; 

    void Start()
    {
        if (nodes == null || nodes.Length == 0)
        {
            Debug.LogError("Nodos no asignados o vacíos en GraphController.");
            return;
        }

        if (enemy == null)
        {
            Debug.LogError("Enemigo no asignado en GraphController.");
            return;
        }

        Lista<GameObject> allNodes = new Lista<GameObject>();
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] == null)
            {
                Debug.LogError("Nodo en la posición " + i + " no asignado.");
                return;
            }
            allNodes.Add(nodes[i]);
        }

       
        for (int i = 0; i < allNodes.Length; i++)
        {
            GameObject currentNode = allNodes.Get(i);
            GameObject nextNode = allNodes.Get((i + 1) % allNodes.Length); 
            NodeControllers currentController = currentNode.GetComponent<NodeControllers>();
            NodeControllers nextController = nextNode.GetComponent<NodeControllers>();

            if (currentController != null && nextController != null)
            {
                currentController.AddAdjacentNode(nextController, 1f);
            }
            else
            {
                Debug.LogError("NodeController no asignado en el nodo " + i + " o " + ((i + 1) % allNodes.Length) + ".");
                return;
            }
        }

        
        enemy.InitializePath(allNodes);
    }
}
