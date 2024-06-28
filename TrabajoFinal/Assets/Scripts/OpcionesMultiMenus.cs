using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpcionesMultiMenus : MonoBehaviour
{


    [SerializeField] private GameObject optionsMenu; 
    [SerializeField] private GameObject audioMenu; 
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject creditsMenu; 

    
    [SerializeField] private Button closeAudioButton;
    [SerializeField] private Button closeControlsButton;
    [SerializeField] private Button closeCreditsButton;

    private void Start()
    {
        if (closeAudioButton != null)
        {
            closeAudioButton.onClick.AddListener(() => audioMenu.SetActive(false));
        }
            

        if (closeControlsButton != null)
        {
            closeControlsButton.onClick.AddListener(() => controlsMenu.SetActive(false));
        }
            

        if (closeCreditsButton != null)
        {
            closeCreditsButton.onClick.AddListener(() => creditsMenu.SetActive(false));
        }
           
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void OpenAudioMenu()
    {
        audioMenu.SetActive(true);
    }

    public void OpenControlsMenu()
    {
        controlsMenu.SetActive(true);
    }

    public void OpenCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }

}
