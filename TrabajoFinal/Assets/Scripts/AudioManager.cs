using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject volumeMenu;
    [SerializeField] private Button volumeButton;

    private void Awake()
    {
        if (volumeButton != null)
        {
            volumeButton.onClick.AddListener(ToggleVolumeMenu);
        }
        else
        {
            Debug.LogError("El botón de volumen no está asignado.");
        }
    }

    public void ToggleVolumeMenu()
    {
        if (volumeMenu != null)
        {
            volumeMenu.SetActive(!volumeMenu.activeSelf);
        }
        else
        {
            Debug.LogError("El menú de volumen no está asignado.");
        }
    }
}
