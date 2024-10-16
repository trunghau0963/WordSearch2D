using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public string selectedCategoryName;
    public string selectedSectionName;

    public string newCategoryName;
    public string newSectionName;
    public string selectedLevelName;

    public string selectedBoardName;
    public BoardData selectedBoardData;

}
