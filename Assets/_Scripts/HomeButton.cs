using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(0);
        //PlayerPrefs.DeleteAll();
        //Debug.Log(PlayerPrefs.GetString("name") + " " + PlayerPrefs.GetString("Highscore"));
    }
}
