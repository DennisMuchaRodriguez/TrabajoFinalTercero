using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public DoubleCircularList<PlayerSelect> personajes;
    public static GameManager Instance;
  
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


}
