using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
public class MenuAnimationController : MonoBehaviour
{
    public RectTransform title;
    public Button[] menuButtons;
    public Image[] sideImages;
    public float fallDistance = 500f;
    public float fallDuration = 1f;
    public float sideMoveDuration = 1f; 
    public float sideOffset = 500f; 
    public float delayBeforeMovingSideImages = 5f; 

    private Vector2[] originalPositions; 

    void Start()
    {
      
        originalPositions = new Vector2[sideImages.Length];
        for (int i = 0; i < sideImages.Length; i++)
        {
            originalPositions[i] = sideImages[i].rectTransform.anchoredPosition;
        }

        AnimateTitleAndButtons();
        StartCoroutine(AnimateImages());
    }

    void AnimateTitleAndButtons()
    {
      
        title.anchoredPosition += new Vector2(0, fallDistance);
        title.DOAnchorPosY(title.anchoredPosition.y - fallDistance, fallDuration).SetEase(Ease.OutBounce);

       
        for (int i = 0; i < menuButtons.Length; i++)
        {
            RectTransform buttonTransform = menuButtons[i].GetComponent<RectTransform>();
            buttonTransform.anchoredPosition += new Vector2(0, fallDistance);
            buttonTransform.DOAnchorPosY(buttonTransform.anchoredPosition.y - fallDistance, fallDuration).SetEase(Ease.OutBounce).SetDelay(0.1f * i);
        }
    }

    IEnumerator AnimateImages()
    {
      
        PositionImages();
        yield return new WaitForSeconds(delayBeforeMovingSideImages);
        AnimateSideImages();
    }

    void PositionImages()
    {
        float screenWidth = Screen.width;

        for (int i = 0; i < sideImages.Length; i++)
        {
            RectTransform imageTransform = sideImages[i].rectTransform;
            bool fromLeft = i % 2 == 0;

            if (fromLeft)
            {
                
                imageTransform.anchoredPosition = new Vector2(-screenWidth - sideOffset, imageTransform.anchoredPosition.y);
            }
            else
            {
                imageTransform.anchoredPosition = new Vector2(screenWidth + sideOffset, imageTransform.anchoredPosition.y);
            }
        }
    }
    void AnimateSideImages()
    {
        for (int i = 0; i < sideImages.Length; i++)
        {
            RectTransform imageTransform = sideImages[i].rectTransform;
            Vector2 originalPosition = originalPositions[i];

       
            imageTransform.DOAnchorPos(originalPosition, sideMoveDuration).SetEase(Ease.OutCubic);
        }
    }
}