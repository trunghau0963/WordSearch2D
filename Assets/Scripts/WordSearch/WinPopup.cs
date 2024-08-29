using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPopup : MonoBehaviour
{
    public GameObject winPopup;
    void Start()
    {
        // winPopup = GameObject.Find("WinPopup");
        winPopup.SetActive(false);
    }

    private void OnEnable() {
        GameEvents.OnBoardComplete += ShowWinPopup;
    }

    private void OnDisable() {
        GameEvents.OnBoardComplete -= ShowWinPopup;
    }

    void ShowWinPopup(){
        winPopup.SetActive(true);
    }

    void CloseWinPopup(){
        winPopup.SetActive(false);
    }

    public void loadNextLevel(){
        GameEvents.LoadNextLevelMethod();
    }
}
