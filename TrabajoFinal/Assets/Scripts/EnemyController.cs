using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
      public Enemys enemyDate;
    public Transform player;
    private float currentHealh;
    private bool isPlayerRange;

    void Start()
    {
        currentHealh = enemyDate.health;

    }
    private void Update()
    {
        if(isPlayerRange && player != null)
        {
            ChasePlayer();
        }
    }
    void ChasePlayer()
    {
        Vector3 director = (player.position - transform.position).normalized;
        transform.position += director * enemyDate.speed * Time.deltaTime;

    }
    public void TakeDamage(float damage)
    {
        currentHealh = currentHealh - damage;
        if (currentHealh <= 0)
        {
            Die();
        }
    }

    void Die()
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
        if (other.gameObject.tag == ("Player"))
        {
            player = other.transform;
            isPlayerRange = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            isPlayerRange = false;
        }
    }
}
