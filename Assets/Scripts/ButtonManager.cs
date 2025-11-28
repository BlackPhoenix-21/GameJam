using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingSceen");
        GameManager.instance.state = GameManager.LevelState.Jupiter;
    }

    public void ExitGame()
    {
        GameManager.instance.SaveGame();
        Application.Quit();
    }

    public void Retry()
    {
        if (GameManager.instance.endless)
        {
            GameManager.instance.highscore = 0;
            SceneManager.LoadScene("Endless");
        }
        else
            SceneManager.LoadScene("MainGame");
        GameManager.instance.SaveGame();
        GameManager.instance.state = GameManager.LevelState.Jupiter;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.instance.state = GameManager.LevelState.MainMenu;
    }

    public void Endless()
    {
        GameManager.instance.endless = true;
        SceneManager.LoadScene("LoadingSceen");
        GameManager.instance.state = GameManager.LevelState.Jupiter;
    }
}