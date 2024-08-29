using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// dung de chon boardata phu hop voi category, section, level
/// </summary>
public class GameDataSelector : MonoBehaviour
{
    public GameData currentGameData;
    public GameCategoryData gameCategoryData;

    void Awake()
    {
        SelectSequentalBoardData();
    }

    private void SelectSequentalBoardData()
    {
        bool found = false;

        foreach (var data in gameCategoryData.categoryRecords)
        {
            if (data.selectedCategoryName == currentGameData.selectedCategoryName)
            {
                print("Category Name in gamedataselector: " + data.selectedCategoryName);
                foreach (var dataSecData in data.sectionDatas.sectionRecords)
                {
                    print("Section Name gamedataselector outline: " + dataSecData.sectionName);

                    if (dataSecData.sectionName == currentGameData.selectedSectionName)
                    {
                        print("Section Name gamedataselector: " + dataSecData.sectionName);
                        foreach (var dataLevel in dataSecData.levelDatas.levelRecords)
                        {
                            print("Board Name gamedataselector outline: " + dataLevel.levelName);
                            if (dataLevel.levelName == currentGameData.selectedLevelName)
                            {
                                print("Board Name gamedataselector: " + dataLevel.levelName);
                                // important
                                var boardIndex = DataSaver.ReadLevelCurrentIndexValues(currentGameData.selectedLevelName); // read index value doc level hien tai
                                print("boardIndex: " + boardIndex);
                                if (boardIndex.z < dataLevel.boardData.Count)
                                {
                                    currentGameData.selectedBoardData = dataLevel.boardData[(int)boardIndex.z];
                                }
                                else
                                {
                                    var randomIndex = Random.Range(0, dataLevel.boardData.Count);
                                    currentGameData.selectedBoardData = dataLevel.boardData[randomIndex];
                                }
                                found = true;
                                break;
                            }
                            if (found) break;
                        }
                        if (found) break;
                    }
                    if (found) break;
                }
                if (found) break;
            }
        }

        if (!found)
        {
            print("board data Eror");
        }
    }
}
