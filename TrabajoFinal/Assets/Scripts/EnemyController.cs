using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    public Enemys enemyData; 
    public Transform player;
    private float currentHealth;
    private bool isPlayerInRange;

    void Start()
    {
        currentHealth = enemyData.health; 
    }

    void Update()
    {
        if (isPlayerInRange && player != null)
        {
            ChasePlayer(); 
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * enemyData.speed * Time.deltaTime; 
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isPlayerInRange = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; 
        }
    }
}
