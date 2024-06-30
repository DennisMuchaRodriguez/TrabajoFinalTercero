using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsButton : MonoBehaviour
{

    [SerializeField] private Image targetImage; 

   
    public void ToggleImage()
    {
       
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
        }
        else
        {
            Debug.LogError("No hay nada");
        }
    }
}
