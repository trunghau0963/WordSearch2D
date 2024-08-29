using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class GameCategoryData : ScriptableObject
{
    [System.Serializable]
    public struct CategoryRecord
    {
        public string selectedCategoryName;
        public GameSectionData sectionDatas;
    }

    public List<CategoryRecord> categoryRecords;

    private void test()
    {
        foreach (var data in categoryRecords)
        {
            Debug.Log(data.selectedCategoryName);
            foreach(var section in data.sectionDatas.sectionRecords)
            {
                Debug.Log(section.sectionName);
                foreach(var level in section.levelDatas.levelRecords)
                {
                    Debug.Log(level.levelName);
                    foreach(var board in level.boardData)
                    {
                        Debug.Log(board);
                    }
                }
            }
        }
    }
}
