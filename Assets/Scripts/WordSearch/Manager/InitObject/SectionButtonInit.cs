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

    int totalBoardCount = 0;

    int finishedBoardCount = 0;

    SavingFile savingFile;
    void Awake()
    {
        savingFile = FindAnyObjectByType<SavingFile>();
        data = savingFile.LoadData();
        // InitializeSectionList();
    }

    void OnEnable()
    {
        InitializeSectionList();
    }

    void OnDisable()
    {
        // Reset all the button status
        foreach (Transform child in transform)
        {

            Destroy(child.gameObject);
        }
    }

    public void InitializeSectionList()
    {
        if (data.DataSet != null)
        {
            foreach (Category category in data.DataSet)
            {
                bool des = currentData.selectedCategoryName == currentData.newCategoryName || currentData.newCategoryName == "";
                if ((des && category.CategoryName == currentData.selectedCategoryName) || (category.CategoryName == currentData.newCategoryName))
                {
                    foreach (Section section in category.Sections)
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

                            foreach (BoardList board in level.Boards)
                            {
                                totalBoardCount++;
                                if (board.isLock == false)
                                {
                                    finishedBoardCount++;
                                }
                            }

                        }
                        string textProgress = (finishedBoardCount / totalBoardCount).ToString() + " %";
                        // Debug.Log("Section Name: " + section.SectionName + " " + (float)finishedLevelCount / totalLevelCount);
                        Button sectionButton = Instantiate(sectionButtonPrefab, transform).GetComponent<Button>();
                        sectionButton.GetComponent<SectionButton>().Init(section.SectionName, (float)finishedLevelCount / totalLevelCount, section.isLock, textProgress);
                        sectionButton.interactable = !section.isLock;
                        // Debug.Log(section.SectionName + " " + (float)finishedLevelCount / totalLevelCount);

                    }
                    break;
                }
            }
        }
    }
}