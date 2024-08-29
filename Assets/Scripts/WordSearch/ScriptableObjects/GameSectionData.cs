using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class GameSectionData : ScriptableObject
{
    [System.Serializable]
    public struct SectionRecord
    {
        public string categoryName;
        public string sectionName;
        public GameLevelData levelDatas;
    }

    public List<SectionRecord> sectionRecords;
}
