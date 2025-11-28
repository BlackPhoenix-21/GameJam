using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceUI : MonoBehaviour
{
    public GameObject UIA;
    public GameObject advanceObjPrefab;
    public GameObject MenuItem;

    public GameObject content;
    public GameObject body;
    public GameObject Button;
    public GameObject ButtonName;

    private Vector3 pos = new Vector3(-325, 275, 0);
    private List<GameObject> menuItems = new();

    private bool currentState = false;

    private void Start()
    {
        if (GameManager.instance.state == GameManager.LevelState.MainMenu)
        {
            GameObject[] hold = GameObject.FindGameObjectsWithTag("AdvanceUI");
            if (hold.Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }

    public void HideChild()
    {
        UIA.SetActive(false);
    }

    public void ShowChild()
    {
        UIA.SetActive(true);
    }

    private void CreateMenuItem(Advancement item)
    {
        GameObject obj = Instantiate(MenuItem, body.transform.position, Quaternion.identity, body.transform);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localPosition = pos;
        pos.y -= 115;
        menuItems.Add(obj);
        if (item.Achieved)
        {
            RectTransform[] childs = obj.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform child in childs)
            {
                switch (child.name)
                {
                    case "Name":
                        child.GetComponent<TMP_Text>().text = item.Name;
                        break;
                    case "Image":
                        child.GetComponent<Image>().sprite = item.Img;
                        break;
                    case "Description":
                        child.GetComponent<TMP_Text>().text = item.Description;
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            RectTransform[] childs = obj.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform child in childs)
            {
                if (child.name == "Image")
                {
                    child.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                    continue;
                }
                try { child.GetComponent<TMP_Text>().text = "???"; }
                catch { }
            }
        }
    }

    public void MenuAdv(List<Advancement> adv)
    {
        if (menuItems.Count > 0)
        {
            foreach (var item in menuItems)
            {
                Destroy(item);
            }
            menuItems.Clear();
            pos = new Vector3(-325, 275, 0);
        }

        TMP_Text tmpText;
        if (ButtonName.TryGetComponent<TMP_Text>(out tmpText))
        {
            tmpText.text = "Advancements: " + UpdateAdvance(adv);
        }
        foreach (var item in GameManager.instance.advance)
        {
            CreateMenuItem(item);
        }
        content.SetActive(false);
    }

    public void NewAdvancement(List<Advancement> adv, int index)
    {
        if (currentState)
        {
            StartCoroutine(Waiter(adv, index));
            return;
        }
        currentState = true;
        GameObject advObj = Instantiate(advanceObjPrefab);
        RectTransform[] childs = advObj.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in childs)
        {
            switch (child.name)
            {
                case "Name":
                    child.GetComponent<TMP_Text>().text = adv[index].Name;
                    break;
                case "Image":
                    child.GetComponent<Image>().sprite = adv[index].Img;
                    break;
                case "Description":
                    child.GetComponent<TMP_Text>().text = adv[index].Description;
                    break;
                case "Count":
                    child.GetComponent<TMP_Text>().text = UpdateAdvance(adv);
                    break;
                default:
                    break;
            }
        }
        DontDestroyOnLoad(advObj);
        StartCoroutine(AdvancementCountdown(advObj));
    }

    private IEnumerator Waiter(List<Advancement> adv, int index)
    {
        while (currentState)
        {
            yield return new WaitForSeconds(1f);
        }
        NewAdvancement(adv, index);
    }

    private IEnumerator AdvancementCountdown(GameObject obj)
    {
        yield return new WaitForSeconds(10f);
        Destroy(obj);
        currentState = false;
    }

    public string UpdateAdvance(List<Advancement> adv)
    {
        int count = 0;
        foreach (Advancement advancement in adv)
        {
            if (advancement.Achieved)
            {
                count++;
            }
        }
        return count.ToString() + "/" + adv.Count.ToString();
    }
}
