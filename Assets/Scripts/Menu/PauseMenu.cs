using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject loseScreenUI;
    public GameObject winScreenUI;

    private PlayerController player;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        Resume();
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
        EscPause();
    }

    private void EscPause()
    {
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

    /// <summary>
    /// Lose Menu
    /// </summary>
    public void LoseScreen()
    {
        loseScreenUI.SetActive(true);
    }

    /// <summary>
    /// Win screen. Used when the player defeats the final boss
    /// </summary>
    public void WinScreen()
    {
        StartCoroutine(ShowWinScreenAfterDelay());
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
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

    IEnumerator ShowWinScreenAfterDelay()
    {
        yield return new WaitForSeconds(1);
        winScreenUI.SetActive(true);
        player.disableMovements = true;

    }
}