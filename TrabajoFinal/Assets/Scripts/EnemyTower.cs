using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float fireRate = 1f;
    public float bulletForce = 20f;
    public GameObject bulletPrefab;
    public Transform firePointe;
    public float currentHealh;
    private Transform player;
    private float nextFireTime = 0f;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && player != null)
        {
            RotateTowardsPlayer();
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }
    void RotateTowardsPlayer()
    {
         Vector3 direction = player.position - transform.position;
        direction.y = 0; 
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    void Fire()
    {
       GameObject bullet = Instantiate(bulletPrefab,firePointe.position,firePointe.rotation);
       Rigidbody _compRigidbody = bullet.GetComponent<Rigidbody>();
        if(_compRigidbody != null)
        {
            _compRigidbody.AddForce(firePointe.up * bulletForce, ForceMode.Impulse);
        }
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.transform;
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInRange =false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
        }
    }
}
