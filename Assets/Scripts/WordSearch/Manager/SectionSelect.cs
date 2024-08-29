using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SectionSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameData gameData;

    public GameCategoryData gameCategoryData;

    public Text sectionText;

    public Image progressBarFilling;

    public Image lockImage;

    private bool _levelLocked;
    void Start()
    {
        // DataSaver.SaveSectionData("Fit For Life", new Vector2(0, 0));
        // DataSaver.SaveSectionData("Plant", new Vector2(0, 1));
        _levelLocked = false;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        UpdateButton();
        button.interactable = !_levelLocked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateButton()
    {
        var currentIdx = -1;
        var totalSection = 0;

        foreach (var data in gameCategoryData.categoryRecords)
        {
            if (data.selectedCategoryName == gameData.selectedCategoryName)
            {
                int categoryIdx = DataSaver.ReadCategoryCurrentIndexValues(gameData.selectedCategoryName);
                foreach(var dataSec in data.sectionDatas.sectionRecords)
                {
                    if(dataSec.sectionName == gameObject.name)
                    {
                        var temp = DataSaver.ReadSectionCurrentIndexValues(gameObject.name);
                        if(temp.x == categoryIdx)
                        {
                            currentIdx = (int)temp.y;
                        }
                        // currentIdx = (int)temp.y;
                        totalSection = dataSec.levelDatas.levelRecords.Count;

                        if((data.sectionDatas.sectionRecords[0].sectionName == gameObject.name) && currentIdx < 0)
                        {
                            DataSaver.SaveSectionData(dataSec.sectionName, new Vector2(categoryIdx, 0));
                            temp = DataSaver.ReadSectionCurrentIndexValues(gameObject.name);
                            currentIdx = (int)temp.y;
                            totalSection = dataSec.levelDatas.levelRecords.Count;
                        }
                    }
                    
                }
            }
        }
        if(currentIdx == - 1){
            _levelLocked = true;
        }
        // else
        // {
        //     var sectionIdx = DataSaver.ReadSectionCurrentIndexValues(gameObject.name);
        //     if (sectionIdx.x == currentIdx)
        //     {
        //         _levelLocked = false;
        //     }
        //     else
        //     {
        //         _levelLocked = true;
        //     }
        // }

        lockImage.gameObject.SetActive(_levelLocked);
        sectionText.text = _levelLocked ? "" : ((currentIdx + 1)/totalSection).ToString("0.0%");
        progressBarFilling.fillAmount = (currentIdx >= 0 && totalSection > 0) ? (float)(currentIdx + 1)/ totalSection : 0f;
    }

    private void OnButtonClick()
    {
        gameData.selectedSectionName = gameObject.name;
        // print("Selected Category: " + gameData.selectedCategoryName);
    }
}
