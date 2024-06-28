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
            Debug.LogError("El bot�n de opciones no est� asignado en el inspector.");
        }

        if (closeButton != null)
        {
           
            closeButton.onClick.AddListener(CloseOptionsMenu);
        }
        else
        {
            Debug.LogError("El bot�n de cerrar no est� asignado en el inspector.");
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
            Debug.LogError("El canvas del men� de opciones no est� asignado en el inspector.");
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
            Debug.LogError("El canvas del men� de opciones no est� asignado en el inspector.");
        }
    }
}
