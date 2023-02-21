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
        textGreeting.text="Zdravo "+ PlayerPrefs.GetString("name") +" ovo je Kevin pomozi mu da stigne do cilja i ima� �ansu da osvoji� nagradu. Za ovo ima� 3 minuta a poene dobija� za svaki kompletiran nivo, i to �to br�e vi�e poena dobija�. Vreme startuje kada klikne� na dugme.";
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
