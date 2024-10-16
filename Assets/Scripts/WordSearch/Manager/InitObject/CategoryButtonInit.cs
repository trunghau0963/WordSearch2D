using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButtonInit : MonoBehaviour
{
    public GameDataSave data;

    public GameObject categoryButtonPrefab;

    int finishedSectionCount = 0;

    int totalSectionCount = 0;

    SavingFile savingFile;
    void Awake()
    {
        savingFile = FindAnyObjectByType<SavingFile>();
        data = savingFile.LoadData();
        InitializeCategoryList();
    }

    public void InitializeCategoryList()
    {
        if (data.DataSet != null)
        {
            foreach (Category category in data.DataSet)
            {
                finishedSectionCount = 0;
                totalSectionCount = 0;
                foreach (Section section in category.Sections)
                {
                    totalSectionCount++;
                    if (section.isLock == false)
                    {
                        finishedSectionCount++;
                    }
                }

                string textProgress = finishedSectionCount + "/" + totalSectionCount;
                Button categoryButton = Instantiate(categoryButtonPrefab, transform).GetComponent<Button>();
                categoryButton.GetComponent<CategoryButton>().Init(category.CategoryName, (float)finishedSectionCount / totalSectionCount, textProgress);
                categoryButton.interactable = !category.isLock;
                // Debug.Log(category.CategoryName + " " + (float)finishedSectionCount / totalSectionCount);
            }
        }
    }
}
