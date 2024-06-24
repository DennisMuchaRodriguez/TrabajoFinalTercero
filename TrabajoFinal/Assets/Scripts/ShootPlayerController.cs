using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerController : MonoBehaviour
{
    public float launchSpeed = 50.0f;
    public GameObject bullet;
    public AudioSource audioSource;
    public AudioClip shootSound;


    public AnimationCurve speedCurve = AnimationCurve.Linear(0, 1, 1, 1);

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = Quaternion.identity;

        Vector3 localDirection = transform.TransformDirection(Vector3.forward);

        
        float modifiedLaunchSpeed = launchSpeed * speedCurve.Evaluate(Time.timeSinceLevelLoad % speedCurve[speedCurve.length - 1].time);
        Vector3 velocity = localDirection * modifiedLaunchSpeed;

        GameObject spawnedBullet = Instantiate(bullet, spawnPosition, spawnRotation);
        Rigidbody rigidbody = spawnedBullet.GetComponent<Rigidbody>();
        rigidbody.velocity = velocity;

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
