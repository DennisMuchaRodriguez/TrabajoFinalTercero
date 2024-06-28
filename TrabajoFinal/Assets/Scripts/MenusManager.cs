using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenusManager : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private Button Button;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (Button != null)
        {
            Button.onClick.AddListener(ToggleVolumeMenu);
        }
        else
        {
            Debug.LogError("Error");
        }


        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseVolumeMenu);
        }
        else
        {
            Debug.LogError("Error");
        }
    }


    public void ToggleVolumeMenu()
    {
        if (Menu != null)
        {
            bool isActive = Menu.activeSelf;
            Menu.SetActive(!isActive);
        }
        else
        {
            Debug.LogError("Error");
        }
    }


    public void CloseVolumeMenu()
    {
        if (Menu != null)
        {
            Menu.SetActive(false);
        }
        else
        {
            Debug.LogError("Error");
        }
    }
}