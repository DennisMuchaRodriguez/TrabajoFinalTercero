using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSelectorAnimation : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform characterSpawnPoint;
    public Button prevButton;
    public Button nextButton;

    private int currentCharacterIndex = 0;
    private GameObject currentCharacter;

    void Start()
    {
        InitializeCharacter();
        SetupButtons();
    }

    void InitializeCharacter()
    {
        if (characterPrefabs.Length > 0)
        {
            currentCharacter = Instantiate(characterPrefabs[currentCharacterIndex], characterSpawnPoint.position, Quaternion.identity);
            currentCharacter.transform.SetParent(characterSpawnPoint);
        }
    }

    void SetupButtons()
    {
        prevButton.onClick.AddListener(ShowPreviousCharacter);
        nextButton.onClick.AddListener(ShowNextCharacter);
    }

    void ShowPreviousCharacter()
    {
        currentCharacterIndex--;
        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = characterPrefabs.Length - 1;
        }
        ChangeCharacter();
    }

    void ShowNextCharacter()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex >= characterPrefabs.Length)
        {
            currentCharacterIndex = 0;
        }
        ChangeCharacter();
    }

    void ChangeCharacter()
    {
       
        currentCharacter.transform.DORotate(new Vector3(0f, -180f, 0f), 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            Destroy(currentCharacter);
            currentCharacter = Instantiate(characterPrefabs[currentCharacterIndex], characterSpawnPoint.position, Quaternion.identity);
            currentCharacter.transform.SetParent(characterSpawnPoint);
            currentCharacter.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            currentCharacter.transform.DORotate(Vector3.zero, 0.5f).SetEase(Ease.InOutQuad);
        }
        );
    }
}
