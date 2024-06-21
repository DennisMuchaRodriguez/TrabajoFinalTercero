using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody _compRigidbody;
   public float moveSpeed = 5.0f;
    public float rotationSpeed = 120.0f;
    public GameObject[] leftWheels;
    public GameObject[] rightWheels;
    public float Life = 10;
    public float wheelRotationSpeed = 200.0f;
    private float moveInput;
    private float rotationInput;
    [SerializeField] private GameObject explosionPrefab; 
    [SerializeField] private string gameOverScene = "Derrota"; 
    [SerializeField] private float delayBeforeSceneChange = 2.0f;
    void Awake()
    {
        MinaController.PlayerInstantiated?.Invoke(this);
        _compRigidbody = GetComponent<Rigidbody>();  
    }
    void Update()
    {
        RotationWeels(moveInput, rotationInput);
        if (Life <= 0)
        {
            SceneManager.LoadScene(gameOverScene);
        }
    }
    void FixedUpdate()
    {
          MoveTank(moveInput);
          RotationTank(rotationInput);
    }
    void MoveTank(float input)
    {
        Vector3 forwardMovement = transform.forward * input * moveSpeed;
        _compRigidbody.velocity = forwardMovement;
    }
    void RotationTank(float input)
    {
        float rotation = input * rotationSpeed * Mathf.Deg2Rad;
        Vector3 angularVelocity = new Vector3(0.0f, rotation, 0.0f);
        _compRigidbody.angularVelocity = angularVelocity;
    }
    void RotationWeels(float moveInput, float rotationInput)
    {
        float wheelRotation = moveInput * wheelRotationSpeed * Time.deltaTime;

        
        for (int i = 0; i < leftWheels.Length; i++)
        {
            if (leftWheels[i] != null)
            {
                leftWheels[i].transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

     
        for (int i = 0; i < rightWheels.Length; i++)
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Life = Life - 1;

           
        }
      
    }
  
}
