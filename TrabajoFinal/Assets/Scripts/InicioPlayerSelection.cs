using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioPlayerSelection : MonoBehaviour
{
  
    void Start()
    {
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Instantiate(GameManager.Instance.GetCharacterByIndex(indexJugador).Player, transform.position, Quaternion.identity);
    }

   
}
