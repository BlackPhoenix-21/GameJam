using UnityEngine;

public class AdvancementTrigger : MonoBehaviour
{
    public int advancementID;

    [HideInInspector]
    public bool target = false;
    [HideInInspector]
    public Vector2 targetPos;

    private float speed = 5f;

    void Update()
    {
        if (!target)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var advancement = GameManager.instance.advance.Find(x => x.AdvanceID == advancementID);
            if (advancement != null && advancement.Achieved != true)
            {
                advancement.Achieved = true;
                GameManager.instance.NewAchieved(advancementID);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var advancement = GameManager.instance.advance.Find(x => x.AdvanceID == advancementID);
            if (advancement != null && advancement.Achieved != true)
            {
                advancement.Achieved = true;
                GameManager.instance.NewAchieved(advancementID);
                if (target)
                    Destroy(gameObject);
            }
        }
    }
}
