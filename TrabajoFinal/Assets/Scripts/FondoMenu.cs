using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMenu : MonoBehaviour
{
    public GameObject backgroundImage; // Asigna la imagen (sprite) que deseas usar desde el inspector

    public float speed = 8f; // Velocidad de movimiento de las im�genes
    public float destroyTime = 5f; // Tiempo despu�s del cual se destruir�n las im�genes
    public float RespawnTime = 0f;
    void Start()
    {
        InvokeRepeating("SpawnImage", 0f, RespawnTime); // Llama a SpawnImage cada 0.5 segundos
    }
     
    void SpawnImage()
    {
        GameObject newImage = Instantiate(backgroundImage, transform.position, Quaternion.identity); // Crea una nueva imagen en la posici�n del GameObject actual
        Rigidbody2D rb = newImage.GetComponent<Rigidbody2D>(); // Obt�n el componente Rigidbody2D para aplicar movimiento

        // Aplica movimiento hacia la izquierda
        rb.velocity = new Vector2(speed, 0);

        Destroy(newImage, destroyTime); // Destruye la imagen despu�s de destroyTime segundos
    }
}
