using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntrolControl : MonoBehaviour
{

    public string gameSceneName;
    SoundManagement audioManager;
    // Start is called before the first frame update
    // void Awake()
    // {
    //     audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManagement>();
    // }

    // Update is called once per frame
    void Start()
    {
        // SoundManagement.Instance.PlayMusic(1);
        MusicManager.Instance.PlayMusic("Intro");
    }

    public void Play(){
        LevelManager.Instance.LoadScene(gameSceneName, "CrossWipe");
        MusicManager.Instance.PlayMusic("MainMenu");
        // SoundManagement.Instance.PlayMusic(0);
        // SceneManager.LoadScene(gameSceneName);
    }
    public void Quit(){
        Application.Quit();
    }
}
