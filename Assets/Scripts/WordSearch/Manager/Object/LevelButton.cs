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
        gameData.selectedLevelName = gameObject.name;
        SceneManager.LoadScene(gameSceneName);
    }
}
