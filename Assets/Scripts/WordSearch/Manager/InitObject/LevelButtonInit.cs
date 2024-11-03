using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonInit : MonoBehaviour
{
    public GameDataSave data;

    public GameData currentGameData;
    public GameObject levelButtonPrefab;

    SavingFile savingFile;
    void Awake()
    {
        savingFile = FindAnyObjectByType<SavingFile>();
        data = savingFile.LoadData();
        // Debug.Log("LevelButtonInit: " + data.DataSet.Count);
        // InitializeLevelList();
    }

    void OnEnable()
    {
        // Debug.Log("LevelButtonInit: " + currentGameData.selectedCategoryName + " / " + currentGameData.selectedSectionName);
        InitializeLevelList();
    }

    void OnDisable()
    {
        // Reset all the button status
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void InitializeLevelList()
    {
        if (data.DataSet != null)
        {
            foreach (Category category in data.DataSet)
            {
                if (category.CategoryName == currentGameData.selectedCategoryName)
                {
                    // Debug.Log(category.CategoryName + " = " + currentGameData.selectedCategoryName);
                    foreach (Section section in category.Sections)
                    {
                        // Debug.Log(section.SectionName + " = " + currentGameData.selectedSectionName);
                        if (section.SectionName == currentGameData.selectedSectionName)
                        {
                            // Debug.Log(section.SectionName + " = " + currentGameData.selectedSectionName);
                            int elementCount = section.Levels.Count;
                            for (int i = 0; i < elementCount; i++)
                            {
                                Level level = section.Levels[i];
                                // Debug.Log("is Even Level " + level.Name + " : " + (i % 2 == 0) + " is Start Level " + (i == 0));
                                Button levelButton = Instantiate(levelButtonPrefab, transform).GetComponent<Button>();
                                levelButton.GetComponent<LevelButton>().Init(level.Name, level.isLock, i % 2 == 0, i == elementCount - 1, level.isCompleted);
                                levelButton.interactable = !level.isLock;
                            }
                            break;
                        }
                        // else
                        // {
                        //     Debug.Log(section.SectionName + " != " + currentGameData.selectedSectionName);
                        // }
                    }
                    break;
                }
                // else
                // {
                //     Debug.Log(category.CategoryName + " != " + currentGameData.selectedCategoryName);
                // }
            }
        }
    }
}
