using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public Enemys enemyData;
    public Lista<GameObject> pathNodes;
    private GameObject objective;
    private int currentIndex = 0;
    private Rigidbody _compRigidbody;
    [SerializeField]private PlayerController player;
    private bool isChasing = false;
    public bool isCooldownActive = false;
    void Start()
    {
        _compRigidbody = GetComponent<Rigidbody>();
        PlayerController.OnPlayerInstantiated += OnPlayerInstantiated;

        if (player == null)
        {
            //Debug.LogError("Jugador no encontrado en la escena.");
            return;
        }

        if (pathNodes != null && pathNodes.Length > 0)
        {
            objective = pathNodes.Get(currentIndex);
        }
    }

    void Update()
    {
        if (isChasing && !isCooldownActive)
        {
            ChasePlayer();
        }
        else if(!isCooldownActive)
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
            _compRigidbody.velocity = direction * enemyData.speed;

            
            RotateTowards(direction);

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
            _compRigidbody.velocity = direction * enemyData.speed;

          
            RotateTowards(direction);

            if (Vector3.Distance(transform.position, player.transform.position) > enemyData.detectionRange)
            {
                isChasing = false;
                objective = pathNodes.Get(currentIndex);
            }
        }
    }

    void Die()
    {

        transform.DOScale(new Vector3(2f, 2f, 2f), 0.2f).OnComplete(Explode);
    }

    void Explode()
    {

        transform.DOScale(Vector3.zero, 0.1f).OnComplete(DestroySelf);
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        enemyData.health -= damage;
        if (enemyData.health <= 0)
        {
            Die();
        }
    }
    void CheckPlayerInRange()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= enemyData.detectionRange)
        {
            isChasing = true;
        }
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemyData.speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
        }
        if (collision.gameObject.tag == "Player")
        {
            if(player != null)
            {
                player.ChangeLife(-enemyData.damage);
                Vector3 pushDirection = collision.transform.position - transform.position;
                player.PushBack(pushDirection.normalized);

                StartCoroutine(AfterCollision());
            }

        }
    }
    IEnumerator AfterCollision()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(1f);
        isCooldownActive = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            isChasing = true;
        }

    }

    private void OnDrawGizmos()
    {
       
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRange);
    }
    private void OnPlayerInstantiated(PlayerController instantiatedPlayer)
    {
        player = instantiatedPlayer;
        
    }
    private void OnDestroy()
    {

        PlayerController.OnPlayerInstantiated -= OnPlayerInstantiated;
    }
}

