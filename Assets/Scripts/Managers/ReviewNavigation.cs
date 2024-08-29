using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReviewNavigation : MonoBehaviour
{

    [SerializeField] private GameObject[] panels;
    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        SwitchToLevel();
    }
    public void SwitchToCategory()
    {
        gameData.selectedCategoryName = "";
    }

    public void SwitchToSection()
    {
        gameData.selectedSectionName = "";
    }

    public void SwitchToLevel()
    {
        gameData.selectedLevelName = "";
    }
}
