using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Linq;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text textScore;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        if (scene==2)
        {
            textScore.text = "Congratulations";
        }
        else
        {
            TimeManager.Mytimer += Time.deltaTime;
            float seconds = (int)(TimeManager.Mytimer % 60);
            float minutes = Mathf.FloorToInt(TimeManager.Mytimer / 60);
            textScore.text = "Time: " + minutes + " " + seconds;
        }
    }
}
