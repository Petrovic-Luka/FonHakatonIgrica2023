using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{

    public string nameOfPlayer;
    public string saveName;
    public TMPro.TMP_InputField inputText;

    // Update is called once per frame
    void Update()
    {
        nameOfPlayer=PlayerPrefs.GetString("name","none");
    }

    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
        //string name = PlayerPrefs.GetString("name", "none");
        //Debug.Log(name);
        SceneManager.LoadScene(2);
    }
}
