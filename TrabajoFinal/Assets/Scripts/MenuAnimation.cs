using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
public class MenuAnimationController : MonoBehaviour
{
    public RectTransform title;
    public Button[] menuButtons;
    public Image[] sideImages; // Im�genes que se mover�n desde los lados

    public float fallDistance = 500f;
    public float fallDuration = 1f;
    public float sideMoveDuration = 1f; // Duraci�n de la animaci�n lateral
    public float sideOffset = 500f; // Distancia que recorrer�n las im�genes desde los lados
    public float delayBeforeMovingSideImages = 5f; // Retardo antes de mover las im�genes laterales

    private Vector2[] originalPositions; // Para almacenar las posiciones originales de las im�genes laterales

    void Start()
    {
        // Almacenar las posiciones originales de las im�genes laterales
        originalPositions = new Vector2[sideImages.Length];
        for (int i = 0; i < sideImages.Length; i++)
        {
            originalPositions[i] = sideImages[i].rectTransform.anchoredPosition;
        }

        AnimateTitleAndButtons();
        StartCoroutine(AnimateSideImagesAfterDelay());
    }

    void AnimateTitleAndButtons()
    {
        // Animar el t�tulo desde una posici�n desplazada verticalmente hacia abajo
        title.anchoredPosition += new Vector2(0, fallDistance);
        title.DOAnchorPosY(title.anchoredPosition.y - fallDistance, fallDuration).SetEase(Ease.OutBounce);

        // Animar los botones de men� desde una posici�n desplazada verticalmente hacia abajo
        for (int i = 0; i < menuButtons.Length; i++)
        {
            RectTransform buttonTransform = menuButtons[i].GetComponent<RectTransform>();
            buttonTransform.anchoredPosition += new Vector2(0, fallDistance);
            buttonTransform.DOAnchorPosY(buttonTransform.anchoredPosition.y - fallDistance, fallDuration).SetEase(Ease.OutBounce).SetDelay(0.1f * i);
        }
    }

    IEnumerator AnimateSideImagesAfterDelay()
    {
        // Posicionar las im�genes fuera del borde de la pantalla
        PositionImagesOutsideScreen();

        // Esperar el tiempo definido antes de iniciar la animaci�n
        yield return new WaitForSeconds(delayBeforeMovingSideImages);

        // Animar las im�genes hacia sus posiciones originales
        AnimateSideImagesToOriginalPosition();
    }

    void PositionImagesOutsideScreen()
    {
        float screenWidth = Screen.width;

        for (int i = 0; i < sideImages.Length; i++)
        {
            RectTransform imageTransform = sideImages[i].rectTransform;
            bool fromLeft = i % 2 == 0; // Alternar entre izquierda y derecha

            if (fromLeft)
            {
                // Mover la imagen fuera del borde izquierdo
                imageTransform.anchoredPosition = new Vector2(-screenWidth - sideOffset, imageTransform.anchoredPosition.y);
            }
            else
            {
                // Mover la imagen fuera del borde derecho
                imageTransform.anchoredPosition = new Vector2(screenWidth + sideOffset, imageTransform.anchoredPosition.y);
            }
        }
    }

    void AnimateSideImagesToOriginalPosition()
    {
        for (int i = 0; i < sideImages.Length; i++)
        {
            RectTransform imageTransform = sideImages[i].rectTransform;
            Vector2 originalPosition = originalPositions[i];

            // Mover la imagen a su posici�n original con un suavizado
            imageTransform.DOAnchorPos(originalPosition, sideMoveDuration).SetEase(Ease.OutCubic);
        }
    }
}