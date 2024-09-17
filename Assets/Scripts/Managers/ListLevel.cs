using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLevel : MonoBehaviour
{
    string nameOfSection;
    public GameData currentData;

    public List<Level> levels = new List<Level>();  

    public GameObject levelPrefab;

    void Start()
    {
        // if(currentData != null)
        // {
        //     nameOfSection = currentData.selectedSectionName;
        //     foreach (var section in currentData.gameCategoryData.categoryRecords)
        //     {
        //         if(section.selectedCategoryName == currentData.selectedCategoryName)
        //         {
        //             foreach (var sec in section.sectionDatas.sectionRecords)
        //             {
        //                 if(sec.sectionName == nameOfSection)
        //                 {
        //                     foreach (var level in sec.levelDatas.levelRecords)
        //                     {
        //                         levels.Add(level);
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }
        // else
        // {
        //     Debug.Log("Data is null");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
