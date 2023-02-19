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
        textGreeting.text="Zdravo "+ PlayerPrefs.GetString("name");
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
