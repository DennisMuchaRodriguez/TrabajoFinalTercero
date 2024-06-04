using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _compRigidbody;
   public float moveSpeed = 5.0f;
    public float rotationSpeed = 120.0f;
    public GameObject[] leftWheels;
    public GameObject[] rightWheels;

    public float wheelRotationSpeed = 200.0f;
    private float moveInput;
    private float rotationInput;

    void Awake()
    {
      _compRigidbody = GetComponent<Rigidbody>();  
    }
    void Update()
    {
        RotationWeels(moveInput, rotationInput);
    }
    void FixedUpdate()
    {
          MoveTank(moveInput);
          RotationTank(rotationInput);
    }
    void MoveTank(float input)
    {
        Vector3 moveDireccion = transform.forward * input * moveSpeed * Time.deltaTime;
        _compRigidbody.MovePosition(_compRigidbody.position + moveDireccion);
    }
    void RotationTank(float input)
    {
        float rotation = input * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        _compRigidbody.MoveRotation(_compRigidbody.rotation * turnRotation);
    }
    void RotationWeels(float moveInput, float rotationInput)
    {
        float wheelRotation = moveInput * wheelRotationSpeed * Time.deltaTime;
        
        for(int i = 0; i < leftWheels.Length; i++)
        {
            if (leftWheels[i] != null)
            {
                leftWheels[i].transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
        
       for(int i = 0;i < rightWheels.Length; i++) 
       { 
            if (rightWheels[i] != null)
            {
                rightWheels[i].transform.Rotate(wheelRotation + rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
       }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }
    public void Rotation(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();
    }
}
