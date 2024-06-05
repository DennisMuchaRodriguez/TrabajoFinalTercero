using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerController : MonoBehaviour
{
    public float launchSpeed = 80.0f;
    public GameObject bullet;
    public AudioSource audioSource;
    public AudioClip shootSound;

    private void Start()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            SpawndBullet();
        }
    }
    void SpawndBullet()
    {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = Quaternion.identity;

        Vector3 localDirection = transform.TransformDirection(Vector3.forward);
        Vector3 velocity = localDirection * launchSpeed;

        GameObject Bullet = Instantiate(bullet, spawnPosition, spawnRotation);
        Rigidbody rigidbody = Bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = velocity; 
        if(audioSource != null && shootSound != null) 
        {

            audioSource.PlayOneShot(shootSound);        
        }
    }

}
