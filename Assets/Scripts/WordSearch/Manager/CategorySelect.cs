using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CategorySelect : MonoBehaviour
{
    public GameData gameData;
    private void OnButtonClick()
    {
        gameData.selectedCategoryName = gameObject.name;
        // DataSaver.SaveCategoryData(gameObject.name, 0);
        // print("Selected Category: " + gameData.selectedCategoryName);
    }
}
