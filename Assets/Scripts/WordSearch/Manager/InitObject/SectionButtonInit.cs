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
        InitializeSectionList();
    }

    public void InitializeSectionList()
    {
        if (data.DataSet != null)
        {
            foreach (Category category in data.DataSet)
            {
                if (category.CategoryName == currentData.selectedCategoryName)
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