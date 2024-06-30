using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje")]
public class PlayerSelect : ScriptableObject
{
    public GameObject Player;
    public GameObject PersonajeJugable;
    public GameObject CanvasStats;
 

    public string Name;
}
