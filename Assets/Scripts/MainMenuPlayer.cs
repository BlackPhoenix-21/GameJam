using System.Collections;
using UnityEngine;

public class MainMenuPlayer : MonoBehaviour
{
    private Animator animator;
    private GameObject hold;
    private Canvas canvas;

    void Start()
    {
        animator = GetComponent<Animator>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    void Update()
    {
        if (canvas == null && GameManager.instance.state == GameManager.LevelState.MainMenu)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.name == "Advancements")
            {
                animator.SetBool("Hover", true);
                animator.SetBool("Open", true);
                GameObject hold = hit2D.collider.gameObject;
                hold.GetComponent<BoxCollider2D>().size = new Vector2(12.25f, 2);
            }
            else
            {
                if (animator.GetBool("Hover") == true)
                {
                    animator.SetBool("Hover", false);
                    animator.SetBool("Open", false);
                    GameObject hold = hit2D.collider.gameObject;
                    hold.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                }
            }
        }
        else
        {
            if (animator.GetBool("Hover") == true)
            {
                animator.SetBool("Hover", false);
                animator.SetBool("Open", false);
                GameObject hold = GameObject.Find("Advancements");
                hold.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit2D = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit2D.collider != null)
            {
                if (hit2D.collider.gameObject.name == "Advancements")
                {
                    hold = new GameObject("Border");
                    hold.AddComponent<BoxCollider2D>();
                    hold.GetComponent<BoxCollider2D>().size = new Vector2(20, 15);
                    hold.transform.position = new Vector3(0, 0, 0);

                    canvas.gameObject.SetActive(false);
                    AdvanceUI advanceUI = GameObject.Find("AdvanceUI").GetComponent<AdvanceUI>();
                    advanceUI.content.SetActive(true);

                    animator.SetBool("Hover", false);
                    animator.SetBool("Open", false);
                    GameObject holding = GameObject.Find("Advancements");
                    holding.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
                    holding.GetComponent<BoxCollider2D>().enabled = false;
                }
                else if (hit2D.collider.gameObject.name == "Border")
                {
                    Destroy(hold);
                    canvas.gameObject.SetActive(true);
                    AdvanceUI advanceUI = GameObject.Find("AdvanceUI").GetComponent<AdvanceUI>();
                    advanceUI.content.SetActive(false);
                    GameObject holding = GameObject.Find("Advancements");
                    holding.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }
}