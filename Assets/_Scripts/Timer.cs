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

    private float time;
    void Start()
    {
        //time = TimeManager.Mytimer;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
            
            float seconds = Mathf.FloorToInt((TimeManager.Mytimer+Time.timeSinceLevelLoad) % 60);
            float minutes = Mathf.FloorToInt((TimeManager.Mytimer + Time.timeSinceLevelLoad) / 60);
            if(minutes>=3)
             {
            SceneManager.LoadScene(12);
             }
            textScore.text = "Time: " + minutes + ":" + seconds;

        
    }
}
