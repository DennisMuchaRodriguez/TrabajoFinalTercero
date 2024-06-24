using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    public Enemys enemyData; 
    public Transform playerTransform;
    private float currentHealth;
    private bool isPlayerInRange;
    private float rotationSpeed = 5.0f;
    private bool isCooldownActive = false;
    void Start()
    {

        currentHealth = enemyData.health;
        PlayerController.OnPlayerInstantiated += OnPlayerInstantiated;
    }

    void Update()
    {
        if (isPlayerInRange && playerTransform != null)
        {

            ChasePlayer(); 
        }
    }

    void ChasePlayer()
    {
        if (!isCooldownActive)
        {
            RotatePlayer();
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * enemyData.speed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(); 
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
    void RotatePlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    void DestroySelf()
    {
        Destroy(gameObject); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
        }
        if (collision.gameObject.tag == "Player")
        {
            if(playerTransform != null && !isCooldownActive)
            {
                Player.ChangeLife(-enemyData.damage);
                Vector3 pushDirection = collision.transform.position - transform.position; 
                Player.PushBack(pushDirection.normalized);
                StartCoroutine(AfterCollision());
            }
        }
    }

    IEnumerator AfterCollision()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(0.5f);
        isCooldownActive = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerTransform = other.transform;
            isPlayerInRange = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false; 
        }
    }
    private void OnPlayerInstantiated(PlayerController instantiatedPlayer)
    {
        Player = instantiatedPlayer;
        playerTransform = Player.transform;
    }
    private void OnDestroy()
    {
       
        PlayerController.OnPlayerInstantiated -= OnPlayerInstantiated;
    }
}
