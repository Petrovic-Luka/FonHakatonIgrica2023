using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        //string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/Leaderboard.json");
        Highscore temp= JsonUtility.FromJson<Highscore>(jsonRead);
        entries = temp.entriesList;
        entriesTransformList = new List<Transform>();
        entries=entries.OrderBy(x => x.score).Take(10).ToList();
        for(int i=0;i<entries.Count;i++)
        {
            if (entries[i]!=null)
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
        //if(!File.Exists(Application.persistentDataPath + "/Leaderboard.json"))
        //{
        //    System.IO.File.AppendAllText(Application.persistentDataPath + "/Leaderboard.json", "");
        //}
        HighscoreEntry entry = new HighscoreEntry() { score = score, name = name };
        string jsonRead = PlayerPrefs.GetString("highscoreTable");
       // string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/Leaderboard.json");
        Highscore temp = JsonUtility.FromJson<Highscore>(jsonRead);
        if(temp==null)
        {
            temp=new Highscore();
            temp.entriesList = new List<HighscoreEntry>();
        }
        temp.entriesList.Add(entry);
        string json = JsonUtility.ToJson(temp);
       // System.IO.File.WriteAllText(Application.persistentDataPath + "/Leaderboard.json", json);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        //Debug.Log(Application.persistentDataPath);
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

