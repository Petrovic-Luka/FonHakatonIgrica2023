using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearningPlatform : MonoBehaviour
{
    public float timeToTogglePlatform = 2;
    public float currentTime = 0;
    public bool enabled = true;

    // Start is called before the first frame update
    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeToTogglePlatform)
        {
            currentTime = 0;
            TogglePlatform();
        }
    }

    private void TogglePlatform()
    {
        enabled = !enabled;
        foreach (Transform child in gameObject.transform)
        {
            if(!child.CompareTag("Player"))
            {
                child.gameObject.SetActive(enabled);
            }
        }

    }
}
