using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Greetings : MonoBehaviour
{
    public TMP_Text displayText;
    // Start is called before the first frame update
    void Awake()
    {
        displayText.text = "Zdravo " + PlayerPrefs.GetString("name", "nepoznato") + " ovo je kevin";
    }

    public void NextScene()
    {

        Debug.Log("is this working");
    }
}
