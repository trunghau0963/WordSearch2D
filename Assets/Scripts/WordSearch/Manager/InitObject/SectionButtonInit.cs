using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionInit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameDataSave data;

    public GameData currentData;

    public GameObject sectionButtonPrefab;

    int totalLevelCount = 0;

    int finishedLevelCount = 0;

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
                if (category.CategoryName == currentData.selectedCategoryName)
                {
                    foreach (Section section in category.Sections)
                    {
                        if (section.SectionName == currentData.selectedSectionName)
                        {
                            totalLevelCount = 0;
                            finishedLevelCount = 0;
                            foreach (Level level in section.Levels)
                            {
                                totalLevelCount++;
                                if (level.isLock == false)
                                {
                                    finishedLevelCount++;
                                }
                            }
                            GameObject sectionButton = Instantiate(sectionButtonPrefab, transform);
                            sectionButton.GetComponent<SectionButton>().Init(section.SectionName, finishedLevelCount / totalLevelCount, section.isLock);
                            // sectionButton.interactable = !section.isLock;

                        }
                        break;
                    }
                }
            }
        }
    }
}
