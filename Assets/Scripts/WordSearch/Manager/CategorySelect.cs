using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CategorySelect : MonoBehaviour
{
    public GameData gameData;
    public GameCategoryData gameCategoryData;
    public Text categoryText;
    public Image progressBarFilling;

    private bool _levelLocked;
    void Start()
    {
        // foreach (var data in gameCategoryData.categoryRecords)
        // {
        //     DataSaver.SaveCategoryData(data.selectedCategoryName, -1);
        // }
        _levelLocked = false;
        // DataSaver.SaveCategoryData(gameCategoryData.categoryRecords[0].selectedCategoryName, 0);
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        // button.interactable = true;
        UpdateButton();
        button.interactable = !_levelLocked;
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    void UpdateButton()
    {
        var currentIdx = -1;
        var totalSection = 0;

        foreach (var data in gameCategoryData.categoryRecords)
        {
            if (data.selectedCategoryName == gameObject.name)
            {
                currentIdx = DataSaver.ReadCategoryCurrentIndexValues(gameObject.name);
                totalSection = data.sectionDatas.sectionRecords.Count;

                if ((gameCategoryData.categoryRecords[0].selectedCategoryName == gameObject.name) && currentIdx < 0)
                {
                    DataSaver.SaveCategoryData(gameCategoryData.categoryRecords[0].selectedCategoryName, 0);
                    currentIdx = DataSaver.ReadCategoryCurrentIndexValues(gameObject.name);
                    totalSection = data.sectionDatas.sectionRecords.Count;
                }
            }
        }
        if(currentIdx == - 1){
            _levelLocked = true;
        }

        categoryText.text = _levelLocked ? "Locked" : ((currentIdx + 1).ToString() + "/" + totalSection.ToString());
        progressBarFilling.fillAmount = (currentIdx >= 0 && totalSection > 0) ? (float)(currentIdx + 1) / totalSection : 0f;

    }

    private void OnButtonClick()
    {
        gameData.selectedCategoryName = gameObject.name;
        // DataSaver.SaveCategoryData(gameObject.name, 0);
        // print("Selected Category: " + gameData.selectedCategoryName);
    }
}
