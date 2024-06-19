using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControllers : MonoBehaviour
{
    public Lista<(NodeControllers, float)> adjacentNodes = new Lista<(NodeControllers, float)>();

    void Awake()
    {
        adjacentNodes = new Lista<(NodeControllers, float)>();
    }

    public void AddAdjacentNode(NodeControllers node, float weight)
    {
        if (node == null)
        {
            Debug.LogError("Nodo adyacente es nulo.");
            return;
        }
        adjacentNodes.Add((node, weight));
    }

    public NodeControllers GetRandomAdjacentNode()
    {
        if (adjacentNodes.Length == 0)
        {
            Debug.LogError("No hay nodos adyacentes disponibles.");
            return null;
        }

        int randomIndex = Random.Range(0, adjacentNodes.Length);
        return adjacentNodes.Get(randomIndex).Item1;
    }
}
