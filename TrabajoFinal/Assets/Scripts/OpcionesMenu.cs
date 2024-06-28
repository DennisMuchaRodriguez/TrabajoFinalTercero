using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpcionesMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenuCanvas; 
    [SerializeField] private Button optionsButton; 
    [SerializeField] private Button closeButton; 

    private void Start()
    {
        if (optionsButton != null)
        {
            
            optionsButton.onClick.AddListener(OpenOptionsMenu);
        }
        else
        {
            Debug.LogError("El botón de opciones no está asignado en el inspector.");
        }

        if (closeButton != null)
        {
           
            closeButton.onClick.AddListener(CloseOptionsMenu);
        }
        else
        {
            Debug.LogError("El botón de cerrar no está asignado en el inspector.");
        }
    }

    
    public void OpenOptionsMenu()
    {
        if (optionsMenuCanvas != null)
        {
            optionsMenuCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("El canvas del menú de opciones no está asignado en el inspector.");
        }
    }

  
    public void CloseOptionsMenu()
    {
        if (optionsMenuCanvas != null)
        {
            optionsMenuCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("El canvas del menú de opciones no está asignado en el inspector.");
        }
    }
}
