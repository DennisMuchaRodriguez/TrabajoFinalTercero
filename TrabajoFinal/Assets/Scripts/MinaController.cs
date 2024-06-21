using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinaController : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    public Minas minaData;
    private float Damage;

  
    public static Action<PlayerController> PlayerInstantiated;

    private void Start()
    {
        Damage = minaData.damage;

       
        PlayerInstantiated += UpdatePlayerReference;
    }

    private void OnDestroy()
    {
   
        PlayerInstantiated -= UpdatePlayerReference;
    }

    
    private void UpdatePlayerReference(PlayerController player)
    {
        Player = player;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Player != null)
            {
                Player.Life -= Damage;
            }
            Destroy(this.gameObject);
        }
    }

}
