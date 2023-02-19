using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Greeting : MonoBehaviour
{
    public TMP_Text textGreeting;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        textGreeting.text="Zdravo "+ PlayerPrefs.GetString("name") +" ovo je Kevin pomozi mu da stigne do cilja i imaš šansu da osvojiš nagradu. Za ovo imaš 3 minuta a poene dobijaš za svaki kompletiran nivo, i to što brže više poena dobijaš. Vreme startuje kada klikneš na dugme.";
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
