using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje")]
public class PlayerSelect : ScriptableObject
{
    public GameObject Player;
    public GameObject PersonajeJugable;
    public string Name;
}
