using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public Image isLock;
    public Text Name;

    public GameData gameData;
    private string gameSceneName = "WordSearchGameScene";

    SoundManagement audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManagement>();
    }
    public void Init(string levelName, bool isLocked)
    {
        Name.text = levelName;
        isLock.gameObject.SetActive(isLocked);

        gameObject.name = levelName;
    }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // audioManager.PlaySFX(audioManager.sfxClipsList[2]);
        gameData.selectedLevelName = gameObject.name;
        SceneManager.LoadScene(gameSceneName);
    }


}
