using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    public DoubleCircularList<PlayerSelect> personajes = new DoubleCircularList<PlayerSelect>(); 
    public PlayerSelect Player1;
    public PlayerSelect Player2;
    public PlayerSelect Player3;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

      
        personajes.InsertNodeAtStart(Player1);
        personajes.InsertNodeAtStart(Player2);
        personajes.InsertNodeAtStart(Player3);
    }
  
    

    public PlayerSelect GetCharacterByIndex(int index)
    {
        return personajes.GetNodeAtPosition(index);
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
