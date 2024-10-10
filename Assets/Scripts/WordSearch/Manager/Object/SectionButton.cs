using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionButton : MonoBehaviour
{
    public Image isLock;
    public Text title;
    public Text progressText;
    public Image progressBarFilling;

    ReviewNavigation navigation;

    public GameData gameData;

    SoundManagement audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManagement>();
    }

    public void Init(string sectionName, float progress, bool isLocked, string progressTxt)
    {
        gameObject.name = sectionName;
        title.text = sectionName;
        isLock.gameObject.SetActive(isLocked);
        progressBarFilling.fillAmount = progress;
        progressText.gameObject.SetActive(!isLocked);
        progressText.text = progressTxt;
    }

    void Start()
    {
        navigation = FindAnyObjectByType<ReviewNavigation>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }


    private void OnButtonClick()
    {
        //  audioManager.PlaySFX(audioManager.sfxClipsList[0]);
        gameData.newSectionName = gameObject.name;
        if (gameData.newCategoryName != "")
        {

            gameData.selectedSectionName = gameData.newSectionName;
            gameData.selectedCategoryName = gameData.newCategoryName;
        }
        else
        {
            gameData.selectedSectionName = gameData.newSectionName;
        }
        navigation.GoToLevel();
    }

}
