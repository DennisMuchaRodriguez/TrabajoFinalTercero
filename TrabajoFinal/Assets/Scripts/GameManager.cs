using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
            
            }

            return instance; 
        }
        private set
        {
            instance = value;
        }

    }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void StartGame()
    {
        Debug.Log("El juego ha comenzado");
    }

    public void EndGame()
    {
        Debug.Log("El juego ha terminado.");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
