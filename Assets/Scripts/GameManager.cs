using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelState state = LevelState.None;
    public static GameManager instance;

    public List<Advancement> advance = new();
    private string path;

    private GameObject lastObj;

    public GameObject Voyger;
    private bool voyger = false;
    public GameObject Manhole;
    private bool manhole = false;

    private bool updatetAdvance = false;
    [HideInInspector]
    public AdvanceUI UI;
    [HideInInspector]
    public int highscore = 0, allTimeHighscore = 0;
    [HideInInspector]
    public bool endless = false;

    private bool pause = false;
    private GameObject pauseMenu;
    public AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        path = Path.Combine(Application.persistentDataPath, "advancements.json");
        Debug.Log(path);
    }

    void Start()
    {
        List<Advancement> holder = LoadGame();
        if (holder != null)
        {
            if (holder.Count != advance.Count)
            {
                Debug.Log("Mismatch in advancement count, rebuilding advancements.");
                foreach (var item in holder)
                {
                    Debug.Log(item);
                }
            }
            else
            {
                advance.Clear();
                advance.AddRange(holder);
            }
        }
        else
        {
            Debug.Log("No Saves found");
            Debug.Log(path);
            foreach (var item in advance)
            {
                item.BuildNew();
            }
        }
        if (state == LevelState.MainMenu)
        {
            UI = GameObject.Find("AdvanceUI").GetComponent<AdvanceUI>();
            UI.MenuAdv(advance);
            updatetAdvance = true;
        }
    }

    private List<string> nonSceens = new List<string> { "LoadingSceen", "GameOver", "Credits" };
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (nonSceens.Contains(SceneManager.GetActiveScene().name))
                return;
            pause = !pause;
            PauseMenuButtons pmb = GameObject.Find("PauseMenu").GetComponent<PauseMenuButtons>();
            if (pause)
                pmb.PauseGame();
            else
                pmb.ResumeGame();
        }

        if (state == LevelState.MainMenu && !updatetAdvance)
        {
            UI.ShowChild();
            UI.MenuAdv(advance);
            updatetAdvance = true;
        }
        if (!updatetAdvance)
        {
            return;
        }
        if (state != LevelState.MainMenu)
        {
            UI.HideChild();
            updatetAdvance = false;
        }
    }

    public void SetLastPoint(Transform point, LevelManager lm)
    {
        lastObj = new GameObject("LastObj");
        lastObj.transform.position = point.position;
        lastObj.AddComponent<BoxCollider2D>().size = new Vector2(30, 1);
        lastObj.GetComponent<BoxCollider2D>().isTrigger = true;
        lastObj.AddComponent<NextLevelCollider>();
        lastObj.GetComponent<NextLevelCollider>().levelManager = lm;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPos = player != null ? player.transform.position : point.position;

        if (state == LevelState.Mars && !voyger)
        {
            GameObject prefab = Instantiate(Voyger, new Vector3(point.position.x + 5, point.position.y + 5, 0), Quaternion.identity);
            voyger = true;
            prefab.GetComponent<AdvancementTrigger>().targetPos = targetPos;
            prefab.GetComponent<AdvancementTrigger>().target = true;
        }
        else if (state == LevelState.Merkur && !manhole)
        {
            GameObject prefab = Instantiate(Manhole, new Vector3(point.position.x + 5, point.position.y + 5, 0), Quaternion.identity);
            manhole = true;
            prefab.GetComponent<AdvancementTrigger>().targetPos = targetPos;
            prefab.GetComponent<AdvancementTrigger>().target = true;
        }
    }

    public void SaveGame()
    {
        List<AdvancementData> dataList = new();
        foreach (var adv in advance)
        {
            dataList.Add(new AdvancementData
            {
                AdvanceID = adv.AdvanceID,
                Name = adv.Name,
                Description = adv.Description,
                Achieved = adv.Achieved
            });
        }

        AdvancementDataListWrapper wrapper = new()
        {
            advancements = dataList,
            allTimeHighscore = allTimeHighscore
        };

        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(path, json);
    }

    private List<Advancement> LoadGame()
    {
        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        var wrapper = JsonUtility.FromJson<AdvancementDataListWrapper>(json);
        if (wrapper == null || wrapper.advancements == null)
            return null;

        // Load persisted all-time highscore
        allTimeHighscore = wrapper.allTimeHighscore;

        // Map gespeicherte Daten per ID
        var savedById = new Dictionary<int, AdvancementData>();
        foreach (var a in wrapper.advancements)
        {
            // Bei doppelten IDs gewinnt der letzte Eintrag
            savedById[a.AdvanceID] = a;
        }

        // In die bestehende Masterliste mergen
        foreach (var adv in advance)
        {
            if (savedById.TryGetValue(adv.AdvanceID, out var data))
            {
                // Übernehme nur Save-relevante Felder
                adv.Achieved = data.Achieved;

                // Optional: Name/Description aus Save übernehmen
                // adv.Name = data.Name;
                // adv.Description = data.Description;
            }
            else
            {
                // Nicht im Save vorhanden -> belasse Defaults
            }
        }

        // Immer die vollständige Liste zurückgeben
        return new List<Advancement>(advance);
    }

    public void NewAchieved(int ID)
    {
        AdvanceUI UI = GameObject.Find("AdvanceUI").GetComponent<AdvanceUI>();
        int index = advance.FindIndex(a => a.AdvanceID == ID);
        UI.NewAdvancement(advance, index);
    }

    [System.Serializable]
    private class AdvancementData
    {
        public int AdvanceID;
        public string Name;
        public string Description;
        public bool Achieved;
    }

    [System.Serializable]
    private class AdvancementDataListWrapper
    {
        public List<AdvancementData> advancements;
        public int allTimeHighscore;
    }

    public enum LevelState
    {
        None,
        MainMenu,
        Jupiter,
        Mars,
        Venus,
        Merkur,
        Erde
    }
}