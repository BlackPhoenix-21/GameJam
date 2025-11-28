using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject btm;

    void Start()
    {
        menu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        menu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        menu.SetActive(true);
        pauseMenu.SetActive(true);
        if (GameManager.instance.state == GameManager.LevelState.MainMenu)
        {
            btm.SetActive(false);
        }
        else
        {
            btm.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
