using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI; 
    public GameObject darkCanvas; 

    private CameraMove cameraMoveScript; 

    void Start()
    {
        
        cameraMoveScript = FindObjectOfType<CameraMove>();
        if (cameraMoveScript == null)
        {
            Debug.LogError("Camera move script not found!");
        }
        else
        {
            Debug.Log("Camera move script found!");
        }

        Time.timeScale = 1; 
        pauseMenuUI.SetActive(false);
        darkCanvas.SetActive(false); 
    }

    void Update()
    {
       
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame(); 
        }
    }

    void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true; 
            Time.timeScale = 0; 
            Debug.Log("Game Paused"); 

            pauseMenuUI.SetActive(true);

            darkCanvas.SetActive(true);

            if (cameraMoveScript != null)
            {
                cameraMoveScript.SetRotationEnabled(false);
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            Debug.Log("Game Resumed"); 

            pauseMenuUI.SetActive(false);

            darkCanvas.SetActive(false);

            if (cameraMoveScript != null)
            {
                cameraMoveScript.SetRotationEnabled(true);
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Cursor.visible: " + Cursor.visible);
            Debug.Log("Cursor.lockState: " + Cursor.lockState);
        }
    }

    public void ReturnToTitle() 
    {
        Debug.Log("Returning to Title Scene");
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }
}
