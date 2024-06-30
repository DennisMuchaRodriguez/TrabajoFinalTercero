using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class LevelController : MonoBehaviour
{
    public TextMeshProUGUI titleText; 
    public Button[] buttons; 
    void Start()
    {
       if(titleText != null && buttons != null)
        {
            Vector3 titleOriginalPosition = titleText.transform.localPosition;
            Vector3[] buttonsOriginalPositions = new Vector3[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttonsOriginalPositions[i] = buttons[i].transform.localPosition;
            }
            AnimateTitle(titleOriginalPosition);
            AnimateButtons(buttonsOriginalPositions);
        }
        else
        {
            Debug.Log("Estas en el menu");
        }

        
  
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void AnimateTitle(Vector3 originalPosition)
    {
   
        titleText.transform.localPosition = new Vector3(originalPosition.x, Screen.height, originalPosition.z);

     
        titleText.transform.DOLocalMove(originalPosition, 2.0f).SetEase(Ease.OutBounce);
    }

    void AnimateButtons(Vector3[] originalPositions)
    {
      
        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];

           
            button.transform.localPosition = new Vector3(originalPositions[i].x, -Screen.height, originalPositions[i].z);

            
            button.transform.DOLocalMove(originalPositions[i], 2.0f).SetEase(Ease.OutBounce);

           
            CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, 2.0f);
        }
    }

}
