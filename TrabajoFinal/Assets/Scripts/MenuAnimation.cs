using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;

public class MenuAnimationController : MonoBehaviour
{
    public RectTransform title;
    public Button[] menuButtons;
    public Transform movingObject;

    public float fallDistance = 500f;
    public float fallDuration = 1f;
    public float moveDistanceZ = 10f;
    public float moveDurationZ = 1f;

    void Start()
    {
        AnimateTitleAndButtons();
        MoveObjectForward();
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

    void MoveObjectForward()
    {
        Vector3 localMove = movingObject.TransformDirection(Vector3.forward) * moveDistanceZ;
        movingObject.DOMove(movingObject.position + localMove, moveDurationZ).SetEase(Ease.OutQuad);
    }
}