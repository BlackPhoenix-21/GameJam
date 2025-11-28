using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform target;       // Das Objekt, das fällt
    public TMP_Text counterText;   // Dein TMP Text
    [HideInInspector]
    public int score = 0;
    private float startHeight;

    void Start()
    {
        // Start-Höhe merken
        startHeight = target.position.y;
    }

    void Update()
    {
        // Differenz berechnen: wie weit nach unten bewegt
        float distanceFallen = startHeight - target.position.y;

        // Optional: nur positive Werte anzeigen
        if (distanceFallen < 0) distanceFallen = 0;

        // Rund auf int für "Counter"-Feeling
        counterText.text = Mathf.FloorToInt(distanceFallen).ToString();
        score = Mathf.FloorToInt(distanceFallen);
    }
}