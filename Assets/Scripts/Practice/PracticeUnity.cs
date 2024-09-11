using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeUnity : MonoBehaviour
{
    int score = 1;
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            score++;
            Debug.Log("Add Score");
            Save();
        }

        if(Input.GetMouseButtonDown(1)){
            score--;
            Debug.Log("Subtract Score");
            Save();
        }

        if(Input.GetMouseButtonDown(2)){
            Debug.Log("Load Score");
            Load();
        }

        if(Input.GetKeyDown(KeyCode.Y)){
            Debug.Log("Pressing Y");
        }
        if(Input.GetKeyDown(KeyCode.N)){
            Debug.Log("Pressing N");
        }
        if(Input.GetKeyDown(KeyCode.M)){
            Debug.Log("Pressing M");
        }
    }

    public void Save(){
        PlayerPrefs.SetInt("Score", score);
    }

    public void Load(){
        score = PlayerPrefs.GetInt("Score");
        Debug.Log("Score: " + score);
    }
}
