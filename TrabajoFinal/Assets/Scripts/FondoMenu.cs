using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FondoMenu : MonoBehaviour
{
    public RawImage image;
    public float _x;
    public float _y;

    private void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, image.uvRect.size);
    }
    
}
