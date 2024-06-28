using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
public class MenuAnimationController : MonoBehaviour
{
    public RectTransform title;
    public Button[] menuButtons;
    public Image[] sideImages; // Imágenes que se moverán desde los lados

    public float fallDistance = 500f;
    public float fallDuration = 1f;
    public float sideMoveDuration = 1f; // Duración de la animación lateral
    public float sideOffset = 500f; // Distancia que recorrerán las imágenes desde los lados
    public float delayBeforeMovingSideImages = 5f; // Retardo antes de mover las imágenes laterales

    private Vector2[] originalPositions; // Para almacenar las posiciones originales de las imágenes laterales

    void Start()
    {
        // Almacenar las posiciones originales de las imágenes laterales
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
        // Animar el título desde una posición desplazada verticalmente hacia abajo
        title.anchoredPosition += new Vector2(0, fallDistance);
        title.DOAnchorPosY(title.anchoredPosition.y - fallDistance, fallDuration).SetEase(Ease.OutBounce);

        // Animar los botones de menú desde una posición desplazada verticalmente hacia abajo
        for (int i = 0; i < menuButtons.Length; i++)
        {
            RectTransform buttonTransform = menuButtons[i].GetComponent<RectTransform>();
            buttonTransform.anchoredPosition += new Vector2(0, fallDistance);
            buttonTransform.DOAnchorPosY(buttonTransform.anchoredPosition.y - fallDistance, fallDuration).SetEase(Ease.OutBounce).SetDelay(0.1f * i);
        }
    }

    IEnumerator AnimateSideImagesAfterDelay()
    {
        // Posicionar las imágenes fuera del borde de la pantalla
        PositionImagesOutsideScreen();

        // Esperar el tiempo definido antes de iniciar la animación
        yield return new WaitForSeconds(delayBeforeMovingSideImages);

        // Animar las imágenes hacia sus posiciones originales
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

            // Mover la imagen a su posición original con un suavizado
            imageTransform.DOAnchorPos(originalPosition, sideMoveDuration).SetEase(Ease.OutCubic);
        }
    }
}