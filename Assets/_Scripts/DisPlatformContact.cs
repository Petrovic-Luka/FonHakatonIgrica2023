using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisPlatformContact : MonoBehaviour
{
    [SerializeField] float timeToJump;
    float timer;
    bool active=false;
    private void Update()
    {
       if(active)
        {
            timer += Time.deltaTime;
        }
       if(timer > timeToJump)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (!child.CompareTag("Player"))
                {
                    child.gameObject.SetActive(false);
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
        }
    }
}
