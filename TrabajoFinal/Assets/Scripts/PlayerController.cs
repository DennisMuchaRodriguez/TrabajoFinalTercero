using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    private Rigidbody _compRigidbody;
    public float OriginalSpeed;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 120.0f;
    public GameObject[] leftWheels;
    public GameObject[] rightWheels;
    public float Life = 10;
    public float wheelRotationSpeed = 200.0f;
    private float moveInput;
    private float rotationInput;
    public AudioSource audioSource;
    public AudioClip damageSound;
    [SerializeField] private string gameOverScene = "Derrota";
    [SerializeField] private float delayBeforeSceneChange = 2.0f;
    private bool isInTrigger = false; 
    public float reducedPushBackFactor = 0.5f;

    public static event Action<PlayerController> OnPlayerInstantiated;
    public event Action<float> OnLifeChanged;
    void Awake()
    {

        _compRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        OriginalSpeed = moveSpeed;
        if (OnPlayerInstantiated != null)
        {
            OnPlayerInstantiated(this);
        }
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
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
        Vector3 forwardMovement = transform.forward * input * moveSpeed;// 2 multiplicaciones y 1 asignación
        _compRigidbody.velocity = forwardMovement;// 1 acceso y 1 asignación
    }
    //Detallado: 2+1+1+1 = 5
    //Asintotico: O(1)
    void RotationTank(float input)
    {
        float rotation = input * rotationSpeed * Mathf.Deg2Rad; // 1 multiplicación, 1 acceso a constante y 1 asignación
        Vector3 angularVelocity = new Vector3(0.0f, rotation, 0.0f);// 1 creación de objeto, 1 acceso y 1 asignación
        _compRigidbody.angularVelocity = angularVelocity; // 1 acceso y 1 asignación
    }
    //Detallado: 1+1+1+1+1+1=6
    //Asintotico: O(1)
    void RotationWeels(float moveInput, float rotationInput)
    {
        float wheelRotation = moveInput * wheelRotationSpeed * Time.deltaTime;// 1 multiplicación y 1 asignación


        for (int i = 0; i < leftWheels.Length; i++)// 1 por inicialización + N (1 comparación + 1 incremento + 1 acceso al arreglo)
        {
            if (leftWheels[i] != null)// 1 comparación y 1 acceso al arreglo
            {
                leftWheels[i].transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);// 2 multiplicaciones + 1 resta + 1 acceso al arreglo + 1 llamada al método Rotate
            }
        }


        for (int i = 0; i < rightWheels.Length; i++)// 1 por inicialización + M (1 comparación + 1 incremento + 1 acceso al arreglo)
        {
            if (rightWheels[i] != null) // 1 comparación y 1 acceso al arreglo
            {
                rightWheels[i].transform.Rotate(wheelRotation + rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);// 2 multiplicaciones + 1 suma + 1 acceso al arreglo + 1 llamada al método Rotate
            }
        }
    }
    //Detallado: 1 + (1 + N(7)) + (1 + M(7)) = 3 + 7N + 7M
    //Asintotico: O(n)
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
            Vector3 pushDirection = transform.position - collision.transform.position;
            PushBack(pushDirection.normalized);
            ChangeLife(-1);


        }
    }
    public void ChangeLife(float amount)
    {
        Life = Life + amount;
       
        if (OnLifeChanged != null)
        {
            OnLifeChanged(Life);
        }
        if (amount < 0 && audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }
    public IEnumerator ApplySpeedBoostCoroutine(float multiplier, float duration)
    {
        moveSpeed *= multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed = OriginalSpeed;
    }
    public void PushBack(Vector3 direction)
    {

        float pushBackDistance = 2.5f; // 1 asignación


        if (isInTrigger)// 1 comparación
        {
            pushBackDistance *= reducedPushBackFactor;// 1 multiplicación y 1 asignación
        }

        _compRigidbody.DOMove(transform.position + direction * pushBackDistance, 0.3f);// 2 multiplicaciones, 1 suma, 1 llamada a método
    }
    //Detallado: 1 + 1 MAX(1,0) + 1 +1 + 1 = 5 o 6
    //Asintotico: O(1)
    public void PushBackForMine(Vector3 minePosition, float forceMagnitude)
    {
        Vector3 direction = transform.position - minePosition;// 1 resta y 1 asignación
        direction.y = 0; // 1 asignación
        direction.Normalize();// 1 llamada a método


        transform.DOMove(transform.position + direction * 2f, 0.3f); // 2 multiplicaciones, 1 suma y 1 llamada a método


    }
    //Detallado : 1+1+1+1+1+1 : 6
    //Asintotico:O(1)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PushBackReducer"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PushBackReducer")) 
        {
            isInTrigger = false;
        }
    }
}