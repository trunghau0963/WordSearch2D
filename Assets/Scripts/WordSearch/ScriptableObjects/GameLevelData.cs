using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class GameLevelData : ScriptableObject
{
    [System.Serializable]
    public struct LevelRecord
    {
        public string categoryName;
        public string sectionName;
        public string levelName;
        public List<BoardData> boardData;
    }

    public List<LevelRecord> levelRecords;

}
