using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinaController : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    public Minas minaData;
    private float Damage;
    public GameObject explosionPrefab;
    public float explosionDuration = 3;
    public AudioSource audioSource;
    public AudioClip explosionSound;
    private void Start()
    {
        Damage = minaData.damage;
        PlayerController.OnPlayerInstantiated += UpdatePlayerReference;
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerInstantiated -= UpdatePlayerReference;
    }

    private void UpdatePlayerReference(PlayerController player)
    {
        Player = player;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Player != null)
            {
                Vector3 minePosition = transform.position;
                float forceMagnitude = 10f;
                Player.PushBackForMine(minePosition, forceMagnitude);

                Player.ChangeLife(-Damage);
            }
            if (audioSource != null && explosionSound != null)
            {
                audioSource.PlayOneShot(explosionSound);
            }
            GameObject explosion =  Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, explosionDuration);

            Destroy(this.gameObject);
        }
    }

}
