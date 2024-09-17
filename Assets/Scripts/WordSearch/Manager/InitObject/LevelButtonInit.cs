using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonInit : MonoBehaviour
{
    public GameDataSave data;

    public GameData currentGameData;
    public GameObject levelButtonPrefab;

    SavingFile savingFile;
    void Start()
    {
        savingFile = FindAnyObjectByType<SavingFile>();
        data = savingFile.LoadData();
        InitializeCategoryList();
    }

    public void InitializeCategoryList()
    {
        if (data.DataSet != null)
        {
            foreach (Category category in data.DataSet)
            {
                if (category.CategoryName == currentGameData.selectedCategoryName)
                {
                    foreach (Section section in category.Sections)
                    {
                        if (section.SectionName == currentGameData.selectedSectionName)
                        {
                            foreach (Level level in section.Levels)
                            {
                                GameObject levelButton = Instantiate(levelButtonPrefab, transform);
                                levelButton.GetComponent<LevelButton>().Init(level.Name, level.isLock);
                            }
                            break; 
                        }
                    }
                    break;
                }
            }
        }
    }
}
