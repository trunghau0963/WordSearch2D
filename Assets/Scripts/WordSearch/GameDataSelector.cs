using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// dung de chon boardata phu hop voi category, section, level
/// </summary>
public class GameDataSelector : MonoBehaviour
{
    public GameData currentGameData;

    public GameDataSave data;

    public SavingFile savingFile;



    void Awake()
    {
        savingFile = FindAnyObjectByType<SavingFile>();
        data = savingFile.LoadData();
        SelectSequentalBoardData();
    }

    private void SelectSequentalBoardData()
    {
        if (data.DataSet != null)
        {
            foreach (var category in data.DataSet)
            {
                if (category.CategoryName == currentGameData.selectedCategoryName)
                {
                    foreach (var section in category.Sections)
                    {
                        if (section.SectionName == currentGameData.selectedSectionName)
                        {
                            foreach (var level in section.Levels)
                            {
                                if (level.Name == currentGameData.selectedLevelName)
                                {
                                    foreach (var board in level.Boards)
                                    {
                                        if (!level.isCompleted)
                                        {
                                            // Skip the board if it is completed
                                            if (board.isCompleted)
                                            {
                                                continue;
                                            }
                                            // if (!board.isLock)
                                            // {
                                            if (board.index < level.Boards.Count)
                                            {
                                                currentGameData.selectedBoardName = board.Name;
                                                currentGameData.selectedBoardData = board.boardData;
                                                return;
                                            }
                                            // }
                                        }
                                        else {
                                            // if the level is completed, select the next board
                                            if (board.index < level.Boards.Count)
                                            {
                                                currentGameData.selectedBoardName = board.Name;
                                                currentGameData.selectedBoardData = board.boardData;
                                                return;
                                            }
                                        }
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
