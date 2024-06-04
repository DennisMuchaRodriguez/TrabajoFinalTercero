using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SpenController : MonoBehaviour
{
    public float spinSpeed = 90.0f;
    public float Direction;
    void Update()
    {
      transform.Rotate(Vector3.up,spinSpeed * Direction  * Time.deltaTime);
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
         Direction = context.ReadValue<float>();
    }

}
