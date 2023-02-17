using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HIghScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> entries;
    private List<Transform> entriesTransformList;
    private void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        AddHighscoreEntry(TimeManager.Mytimer, PlayerPrefs.GetString("name"));
        string jsonRead = PlayerPrefs.GetString("highscoreTable");
        Highscore temp= JsonUtility.FromJson<Highscore>(jsonRead);
        entries = temp.entriesList;
        entriesTransformList = new List<Transform>();
        entries=entries.OrderBy(x => x.score).Take(10).ToList();
        for(int i=0;i<entries.Count;i++)
        {
            CreateHighScoreEntryTransform(entries[i], entryContainer, entriesTransformList,i);
        }
    }
    private void CreateHighScoreEntryTransform(HighscoreEntry entry,Transform container,List<Transform> transformsList,int position)
    {
        float templateHight = 60f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTemplate.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHight * (position+1));
        entryTransform.gameObject.SetActive(true);
        entryTransform.Find("posText").GetComponent<TMP_Text>().text = "" + (position);
        entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = "" + entry.score;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = entry.name;
    }

    private void AddHighscoreEntry(float score,string name)
    {
        HighscoreEntry entry = new HighscoreEntry() { score = score, name = name };
        string jsonRead = PlayerPrefs.GetString("highscoreTable");
        Highscore temp = JsonUtility.FromJson<Highscore>(jsonRead);
        temp.entriesList.Add(entry);
        string json = JsonUtility.ToJson(temp);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
}


class Highscore
{
    public List<HighscoreEntry> entriesList;
}


[System.Serializable]
class HighscoreEntry
{
    public float score;
    public string name;
}

