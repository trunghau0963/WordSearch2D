using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButtonPuzzle : MonoBehaviour
{
    public GameData gameData;
    public GameCategoryData gameCategoryData;

    public Image Lock;

    // public Image Unlock;
    private bool _levelLocked;
    private string gameSceneName = "WordSearchGameScene";

    void Start()
    {
        // DataSaver.ResetFirstLevel("Level 1");
        _levelLocked = false;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        UpdateButton();
        button.interactable = !_levelLocked;
    }

    void UpdateButton()
    {
        var currentIdx = -1;
        var totalBoard = 0;

        foreach (var data in gameCategoryData.categoryRecords)
        {
            if (data.selectedCategoryName == gameData.selectedCategoryName)
            {
                print("Category Name in select button: " + data.selectedCategoryName);
                int idxCategory = DataSaver.ReadCategoryCurrentIndexValues(data.selectedCategoryName);

                foreach (var dataSecData in data.sectionDatas.sectionRecords)
                {
                    if (dataSecData.sectionName == gameData.selectedSectionName)
                    {
                        print("Section Name in select button: " + dataSecData.sectionName);

                        var tempIdx = DataSaver.ReadSectionCurrentIndexValues(dataSecData.sectionName);
                        int idxSection = (int)tempIdx.x;

                        foreach (var dataLevel in dataSecData.levelDatas.levelRecords)
                        {
                            if (dataLevel.levelName == gameObject.name)
                            {
                                var temp = DataSaver.ReadLevelCurrentIndexValues(gameObject.name);
                                print("index get in current level" + temp.x + " "+  temp.y + " "+ temp.z);
                                if(temp.x == idxCategory && temp.y == idxSection){
                                    print("true");
                                    currentIdx = (int)temp.z;
                                }
                                // currentIdx = (int)temp.z;
                                totalBoard = dataLevel.boardData.Count;

                                if ((dataSecData.levelDatas.levelRecords[0].levelName == gameObject.name) && currentIdx < 0)
                                {
                                    print("category idx" + idxCategory + "section idx" + idxSection); 
                                    DataSaver.SaveLevelData(dataSecData.levelDatas.levelRecords[0].levelName, new Vector3(idxCategory, idxSection, 0));

                                    temp = DataSaver.ReadLevelCurrentIndexValues(gameObject.name);
                                    currentIdx = (int)temp.z;
                                    totalBoard = dataLevel.boardData.Count;
                                }
                            }
                        }


                    }
                }
            }
        }
        if (currentIdx == -1)
        {
            _levelLocked = true;
        }

        Lock.gameObject.SetActive(_levelLocked);
        // Unlock.gameObject.SetActive(!_levelLocked);
    }

    private void OnButtonClick()
    {
        gameData.selectedLevelName = gameObject.name;
        SceneManager.LoadScene(gameSceneName);
    }
}
