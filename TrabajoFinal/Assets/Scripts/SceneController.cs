using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public CinemachineFreeLook initialCamera; 
    public CinemachineFreeLook followCamera;  
    private PlayerController player;
    public string nextSceneName = "Victoria";
    public float checkInterval = 1f;
    private bool allEnemiesDefeated = false;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemiesRemainingText;
    public TextMeshProUGUI messageTextStart;
    private bool isWritingMessage = false;
    public float typingSpeed = 0.05f;
    
    private void Start()
    {
        
        playerHealthText.gameObject.SetActive(false);
        enemiesRemainingText.gameObject.SetActive(false);
        if (initialCamera != null) 
        {
            initialCamera.Priority = 10;
        }
        if (followCamera != null)
        {
            followCamera.Priority = 5;
        }
        InvokeRepeating("CheckForEnemies", checkInterval, checkInterval);
        StartCoroutine(FindPlayerCamera());
        StartCoroutine(ShowTextDelay(3f));
        StartCoroutine(ShowMessageStart(3f, "Bienvenido al juego"));
    }
    private IEnumerator ShowMessageStart(float delay, string message)
    {
        yield return new WaitForSeconds(delay);

        messageTextStart.gameObject.SetActive(true);
        isWritingMessage = true;
        yield return StartCoroutine(WriteMessage(message));

   
        yield return new WaitForSeconds(3f);

 
        messageTextStart.gameObject.SetActive(false);
        isWritingMessage = false;
    }
    private IEnumerator WriteMessage(string message)
    {
        messageTextStart.text = "";
        
        for (int i = 0; i < message.Length; i++)
        {
            messageTextStart.text += message[i];
            yield return new WaitForSeconds(typingSpeed);
        }
            
     
    }
    private IEnumerator ShowTextDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

      
        playerHealthText.gameObject.SetActive(true);
        enemiesRemainingText.gameObject.SetActive(true);
    }
    private void CheckForEnemies()
    {
      
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int RemainigEnemis = enemies.Length;

        enemiesRemainingText.text = "Enemigos restantes: " + RemainigEnemis;

        if (enemies.Length == 0)
        {
            allEnemiesDefeated = true;
            CancelInvoke("ChecForEnemies");
        }

        Debug.Log("Hay " + enemies.Length);
        enemiesRemainingText.text = "Enemigos restantes: " + RemainigEnemis;
    }
    private IEnumerator FindPlayerCamera()
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
     
            player.OnLifeChanged += UpdatePlayerHealth;
            UpdatePlayerHealth(player.Life);
        }
        else
        {
            Debug.LogError("No se encontr� el objeto jugador o las c�maras no est�n asignadas");
        }
    }
    public bool AreAllEnemisDefeat()
    {
        return allEnemiesDefeated;
    }
    public void ChangeScene()
    {
  
        SceneManager.LoadScene(nextSceneName);
    }
    public void UpdatePlayerHealth(float Healht)
    {
        
        playerHealthText.text = "Vida: " + Healht.ToString();
    }
    private void OnDestroy()
    {
       
        if (player != null)
        {
            player.OnLifeChanged -= UpdatePlayerHealth;
        }
    }
    private void Update()
    {
        if (player != null)
        {
            if (isWritingMessage)
            {
                player.moveSpeed = 0;
            }
            else
            {
                player.moveSpeed = player.OriginalSpeed;
            }
        }
    }
}
