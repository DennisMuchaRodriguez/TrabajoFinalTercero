using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;
    public PlayerController player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        if (player != null && cinemachineFreeLook != null)
        {
            Transform followTransform = player.transform;
            Transform lookAtTransform = player.transform;

            cinemachineFreeLook.m_LookAt = lookAtTransform;
            cinemachineFreeLook.m_Follow = followTransform;
        }
        else
        {
            Debug.LogError("No se encontró el objeto jugador con la etiqueta 'Player' o Cinemachine FreeLook no está asignado.");
        }
    }


}
