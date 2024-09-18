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
        Debug.Log("Cliked " + gameObject.name);
        gameData.selectedSectionName = gameObject.name;
        navigation.GoToLevel();
    }

}
