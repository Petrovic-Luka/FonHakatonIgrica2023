using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLvl : MonoBehaviour
{
    //private float temp = 0;
    private float score=100;
    public void FixedUpdate()
    {
        if(score>0)
        {
            score -= 0.008f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            TimeManager.Score += score * (SceneManager.GetActiveScene().buildIndex - 2);
            TimeManager.Mytimer += Time.timeSinceLevelLoad;
            SceneManager.LoadScene(scene + 1);
        }
    }

}
