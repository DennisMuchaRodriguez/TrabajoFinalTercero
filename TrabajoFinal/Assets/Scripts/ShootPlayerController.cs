using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerController : MonoBehaviour
{
    public float launchSpeed = 80.0f;
    public GameObject bullet;
    
 
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
    }

}
