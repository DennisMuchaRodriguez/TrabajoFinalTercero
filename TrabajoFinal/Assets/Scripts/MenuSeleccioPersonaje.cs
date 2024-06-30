using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSeleccioPersonaje : MonoBehaviour
{

    private int index;
    [SerializeField] private Transform characterDisplayContainer;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Transform CanvasStats;
    private GameManager gameManager;
    private GameObject currentCharacterModel;
    private GameObject currentCanvasStats;

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
            UpdateCharacterStats(selectedCharacter.CanvasStats);
         
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
        UpdateCharacterStats(currentCharacter.CanvasStats);
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
            currentCharacterModel.transform.DORotate(new Vector3(0, 360, 0), 19.0f, RotateMode.FastBeyond360).SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }
    }

    private void UpdateCharacterStats(GameObject newCanvasStats)
    {
        if (currentCanvasStats != null)
        {
            currentCanvasStats.SetActive(false);
        }

       
        if (newCanvasStats != null)
        {
          
            currentCanvasStats = Instantiate(newCanvasStats, CanvasStats);
            currentCanvasStats.transform.localPosition = Vector3.zero;
            currentCanvasStats.transform.localRotation = Quaternion.identity;
            currentCanvasStats.SetActive(true);
        }
    }
}
