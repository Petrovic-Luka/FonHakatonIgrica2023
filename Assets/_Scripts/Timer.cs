using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    public TMP_Text textScore;
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        //textScore.text = "Time:";
        //timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float seconds=(int)(timer%60);
        float minutes = Mathf.FloorToInt(timer / 60);
        textScore.text = "Time: " + minutes + " " + seconds;
    }
}
