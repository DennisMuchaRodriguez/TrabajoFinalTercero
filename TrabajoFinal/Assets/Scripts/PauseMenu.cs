using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject controlsMenu;


    [SerializeField] private Button resumeButton; 
    [SerializeField] private Button returnToMainMenuButton; 
    [SerializeField] private Button closeAudioButton; 
    [SerializeField] private Button closeControlsButton; 


    private bool isPaused = false;
    private bool canPause = false; 

    private void Start()
    {
    
        pauseMenu.SetActive(false);

     
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }

        if (returnToMainMenuButton != null)
        {
            returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }

        if (closeAudioButton != null)
        {
            closeAudioButton.onClick.AddListener(CloseAudioMenu);
        }

        if (closeControlsButton != null)
        {
            closeControlsButton.onClick.AddListener(CloseControlsMenu);
        }


        
        StartCoroutine(EnablePauseAfterDelay(7f));
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private IEnumerator EnablePauseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canPause = true; 
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; 
        pauseMenu.SetActive(true); 
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        pauseMenu.SetActive(false); 
        isPaused = false;

        CloseAllMenus();
    }

    private void CloseAllMenus()
    {
        audioMenu.SetActive(false);
        controlsMenu.SetActive(false);
    
    }

    public void OpenAudioMenu()
    {
        CloseAllMenus();
        audioMenu.SetActive(true);
    }

    public void CloseAudioMenu()
    {
        audioMenu.SetActive(false);
    }

    public void OpenControlsMenu()
    {
        CloseAllMenus();
        controlsMenu.SetActive(true);
    }

    public void CloseControlsMenu()
    {
        controlsMenu.SetActive(false);
    }


    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu"); 
    }
  

}
