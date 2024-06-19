using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public Enemys enemyData; 
    public Lista<GameObject> pathNodes;
    private GameObject objective;
    private int currentIndex = 0;
    private Rigidbody rb;
    private GameObject player;
    private bool isChasing = false;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Jugador no encontrado en la escena.");
            return;
        }

        if (pathNodes != null && pathNodes.Length > 0)
        {
            objective = pathNodes.Get(currentIndex);
        }

    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        CheckPlayerInRange();
    }

    public void InitializePath(Lista<GameObject> nodes)
    {
        if (nodes == null || nodes.Length == 0)
        {
            Debug.LogError("Nodes no está inicializado o está vacío.");
            return;
        }

        pathNodes = nodes;
        objective = pathNodes.Get(0);
    }

    void Patrol()
    {
        if (objective != null)
        {
            Vector3 direction = (objective.transform.position - transform.position).normalized;
            rb.velocity = direction * enemyData.speed;

            if (Vector3.Distance(transform.position, objective.transform.position) < 0.1f)
            {
                currentIndex = (currentIndex + 1) % pathNodes.Length;
                objective = pathNodes.Get(currentIndex);
            }
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * enemyData.speed;

            if (Vector3.Distance(transform.position, player.transform.position) > enemyData.detectionRange)
            {
                isChasing = false;
                objective = pathNodes.Get(currentIndex);
            }
        }
    }

    void CheckPlayerInRange()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= enemyData.detectionRange)
        {
            isChasing = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRange);
    }
}

