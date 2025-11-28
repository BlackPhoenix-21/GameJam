using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject screeen1;
    public GameObject screeen2;
    private bool run = false;

    private void Start()
    {
        StartCoroutine(Countdown(10, screeen1));
    }

    IEnumerator Countdown(int time, GameObject obj)
    {
        yield return new WaitForSeconds(time);

        if (!run)
        {
            Destroy(obj);
            run = true;
            StartCoroutine(Countdown(10, null));
        }
        else
        {
            if (!GameManager.instance.endless)
                SceneManager.LoadScene("MainGame");
            else
                SceneManager.LoadScene("Endless");
        }
    }
}
