using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAnimation : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<BlackHole>().isActive = false;
            if (GameManager.instance.endless)
                GameManager.instance.highscore = GameObject.Find("UIManager").GetComponent<UIManager>().score;
            Debug.Log("Player has died!");

            collision.gameObject.GetComponent<Animator>().SetTrigger("Die");
            StartCoroutine(GameOverScreen(0.25f));
        }
    }

    IEnumerator GameOverScreen(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("GameOver");
    }
}
