using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject loseScreenUI;
    private PlacyerController player;


    private void Awake()
    {
        player = FindObjectOfType<PlacyerController>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (player != null)
        {
            if (player.isDead)
            {
                LoseScreen();
            }
        }


		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void LoseScreen()
    {
        loseScreenUI.SetActive(true);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //public void LoadOptions()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("Options");
    //}
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");

    }


    public void QuitMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}