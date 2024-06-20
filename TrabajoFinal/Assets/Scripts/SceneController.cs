using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public CinemachineFreeLook initialCamera; 
    public CinemachineFreeLook followCamera;  
    private PlayerController player;

    private void Start()
    {
     
        if (initialCamera != null) initialCamera.Priority = 10;
        if (followCamera != null) followCamera.Priority = 5;

       
        StartCoroutine(FindPlayerAndSwitchCamera());
    }

    private IEnumerator FindPlayerAndSwitchCamera()
    {
        
        while (player == null)
        {
            player = FindAnyObjectByType<PlayerController>();
            yield return new WaitForSeconds(0.5f);
        }

    
        if (player != null && followCamera != null)
        {
            followCamera.m_LookAt = player.transform;
            followCamera.m_Follow = player.transform;

      
            followCamera.Priority = 11; 
            initialCamera.Priority = 5;
        }
        else
        {
            Debug.LogError("No se encontró el objeto jugador o las cámaras no están asignadas.");
        }
    }



}
