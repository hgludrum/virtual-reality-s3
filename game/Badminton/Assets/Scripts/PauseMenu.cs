using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public Transform cameraTransform; 
    public float distanceFromCamera = 2.0f;

    public bool activePauseUI = true;

    void Start()
    {
        pauseUI.SetActive(false);
        activePauseUI = false;
        Time.timeScale = 1;
    }



    
    public void DisplayPauseUI()
    {
        if(activePauseUI)
        {
            pauseUI.SetActive(false);
            activePauseUI = false;
            Time.timeScale = 1;
        }
        else if (!activePauseUI)
        {
            if (cameraTransform != null)
            {
                pauseUI.transform.position = cameraTransform.position + (cameraTransform.forward * distanceFromCamera);

                pauseUI.transform.rotation = Quaternion.LookRotation(pauseUI.transform.position - cameraTransform.position);
            }
            pauseUI.SetActive(true);
            activePauseUI = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        activePauseUI = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

