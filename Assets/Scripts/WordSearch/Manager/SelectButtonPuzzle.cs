using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButtonPuzzle : MonoBehaviour
{
    public GameData gameData;
    private string gameSceneName = "WordSearchGameScene";

    private void OnButtonClick()
    {
        gameData.selectedLevelName = gameObject.name;
        SceneManager.LoadScene(gameSceneName);
    }
}
