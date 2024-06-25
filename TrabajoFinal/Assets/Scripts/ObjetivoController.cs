using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjetivoController : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI dialogueText;
    public string fullMessage;
    public string allEnemiesDefeatedMessage;
    public float typingSpeed = 0.05f;
    public GameObject Jaule;
    public SceneController sceneController;
    private bool isPlayerNear = false;
    private bool isMessageShown = false;

    void Start()
    {
        promptText.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);

        
        PlayerController.OnPlayerInstantiated += UpdatePlayerReference;
        //Debug.Log("ObjetivoController Suscrito al evento OnPlayerInstantiated");
    }

    void OnDestroy()
    {
     
        PlayerController.OnPlayerInstantiated -= UpdatePlayerReference;
    }

    private void Update()
    {
        if (isPlayerNear && !isMessageShown)
        {
            promptText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ShowMessage());
            }
        }
        else if (!isPlayerNear)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void UpdatePlayerReference(PlayerController player)
    {
        Player = player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player != null && other.gameObject == Player.gameObject)
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Player != null && other.gameObject == Player.gameObject)
        {
            isPlayerNear = false;
            isMessageShown = false;
            StopAllCoroutines();
            dialogueText.gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowMessage()
    {
        isMessageShown = true;
        promptText.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";

        string messageToShow;
        if (sceneController.AreAllEnemisDefeat())
        {
            messageToShow = allEnemiesDefeatedMessage;
        }
        else
        {
            messageToShow = fullMessage;
        }

        int length = messageToShow.Length;

        for (int i = 0; i < length; i++)
        {
            dialogueText.text += messageToShow[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        if (sceneController.AreAllEnemisDefeat())
        {
            
            if (Jaule != null)
            {
                Destroy(Jaule);
            }
         
            yield return new WaitForSeconds(2.0f);
            sceneController.ChangeScene();
        }
    }
}
