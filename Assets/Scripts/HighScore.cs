using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public GameObject highscore;

    void Start()
    {
        if (GameManager.instance.endless)
        {
            if (GameManager.instance.highscore < GameManager.instance.allTimeHighscore)
            {
                highscore.GetComponent<TMP_Text>().text = "Highscore: " + GameManager.instance.highscore.ToString() + "\nOld Highscore: " + GameManager.instance.allTimeHighscore.ToString();
            }
            else
            {
                highscore.GetComponent<TMP_Text>().text = "New All Time Highscore! Old Highscore: " + GameManager.instance.allTimeHighscore.ToString() + "\nNew Highscore: " + GameManager.instance.highscore.ToString();
                GameManager.instance.allTimeHighscore = GameManager.instance.highscore;
            }
            highscore.SetActive(true);
        }
        else
        {
            highscore.SetActive(false);
        }
    }
}
