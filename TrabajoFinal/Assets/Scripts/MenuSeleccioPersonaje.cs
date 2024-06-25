using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuSeleccioPersonaje : MonoBehaviour
{

    private int index;
    [SerializeField] private Transform characterDisplayContainer;
    [SerializeField] private Transform stickerDisplayContainer;
    [SerializeField] private TextMeshProUGUI nameText; 
    private GameManager gameManager;
    private GameObject currentCharacterModel;
    private GameObject currentCharacterSticker;
    void Start()
    {
        gameManager = GameManager.Instance;
        index = PlayerPrefs.GetInt("JugadorIndex", 0); 

        if (index >= gameManager.personajes.count)
        {
            index = 0; 
        }

        UpdateCharacterDisplay(); 
    }

    private void CambiarPantalla()
    {
        if (index >= 0 && index < gameManager.personajes.count)
        {
            PlayerPrefs.SetInt("JugadorIndex", index);
            PlayerSelect selectedCharacter = gameManager.GetCharacterByIndex(index);

            
            nameText.text = selectedCharacter.Name;
            UpdateCharacterModel(selectedCharacter.PersonajeJugable);
            UpdateCharacterSticker(selectedCharacter.StickerPersonaje);
            //Debug.LogError("Se cambio a indice: " + index);
        }
        else
        {
            Debug.LogError("Índice de personaje fuera de rango.");
        }
    }

    public void SiguientePersonaje()
    {
        index = (index + 1) % gameManager.personajes.count; 
        CambiarPantalla();
    }

    public void AnteriorPersonaje()
    {
        index = (index - 1 + gameManager.personajes.count) % gameManager.personajes.count; 
        CambiarPantalla();
    }

    public void IniciarJuego()
    {
        PlayerPrefs.SetInt("JugadorIndex", index); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    private void UpdateCharacterDisplay()
    {
        
        PlayerSelect currentCharacter = gameManager.GetCharacterByIndex(index);
        nameText.text = currentCharacter.Name;
        UpdateCharacterModel(currentCharacter.PersonajeJugable);
        UpdateCharacterSticker(currentCharacter.StickerPersonaje);
    }



    private void UpdateCharacterModel(GameObject newModel)
    {
        
        if (currentCharacterModel != null)
        {
            Destroy(currentCharacterModel);
        }

        
        if (newModel != null)
        {
            currentCharacterModel = Instantiate(newModel, characterDisplayContainer);
            currentCharacterModel.transform.localPosition = Vector3.zero; 
            currentCharacterModel.transform.localRotation = Quaternion.identity;
            currentCharacterModel.transform.DORotate(new Vector3(0, 360, 0), 5.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
    }

    private void UpdateCharacterSticker(GameObject newSticker)
    {
        if (currentCharacterSticker != null)
        {
            Destroy(currentCharacterSticker);
        }

        if (newSticker != null)
        {
            currentCharacterSticker = Instantiate(newSticker, stickerDisplayContainer);
            currentCharacterSticker.transform.localPosition = Vector3.zero;
            currentCharacterSticker.transform.localRotation = Quaternion.identity;
        }
    }
}
