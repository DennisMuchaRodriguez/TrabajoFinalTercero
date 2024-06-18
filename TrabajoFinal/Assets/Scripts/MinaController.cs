using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinaController : MonoBehaviour
{
    [SerializeField] PlayerController Player;
    public Minas minaData;
    private float Damage;

    private void Start()
    {
        Damage = minaData.damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.Life = Player.Life - Damage;
            Destroy(this.gameObject);
        }
       
    }


}
