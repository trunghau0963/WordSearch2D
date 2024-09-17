using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SectionSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameData gameData;

    private void OnButtonClick()
    {
        gameData.selectedSectionName = gameObject.name;
        // print("Selected Category: " + gameData.selectedCategoryName);
    }
}
