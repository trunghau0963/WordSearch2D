using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{

    // void Start()
    // {
    //     // Xóa tất cả dữ liệu trong PlayerPrefs
    //     PlayerPrefs.DeleteAll();

    //     // Lưu lại để đảm bảo thay đổi được áp dụng
    //     PlayerPrefs.Save();

    //     Debug.Log("PlayerPrefs đã được xóa!");
    // }

    public static Vector3 ReadLevelCurrentIndexValues(string name)
    {
        var value = new Vector3(-1, -1, -1);
        if (PlayerPrefs.HasKey(name))
        {
            var indexString = PlayerPrefs.GetString(name);
            var indices = indexString.Split(',');
            if (indices.Length == 3)
            {
                if (int.TryParse(indices[0], out int x) &&
                    int.TryParse(indices[1], out int y) &&
                    int.TryParse(indices[2], out int z))
                {
                    value = new Vector3(x, y, z);
                }
                else
                {
                    Debug.LogError("Failed to parse indices: " + indexString);
                }
            }
            else
            {
                Debug.LogError("Invalid index string format: " + indexString);
            }
        }

        // print("Level Name: " + name + " Value: " + value);
        return value;
    }

    public static Vector2 ReadSectionCurrentIndexValues(string name)
    {
        var value = new Vector2(-1, -1);
        if (PlayerPrefs.HasKey(name))
        {
            var indexString = PlayerPrefs.GetString(name);
            var indices = indexString.Split(',');
            value.x = int.Parse(indices[0]);
            value.y = int.Parse(indices[1]);
        }

        // print("Section Name: " + name + " Value: " + value);
        return value;
    }

    public static int ReadCategoryCurrentIndexValues(string name)
    {
        var value = -1;
        if (PlayerPrefs.HasKey(name))
        {
            value = PlayerPrefs.GetInt(name);
        }

        // print("Category Name: " + name + " Value: " + value);
        return value;
    }

    public static void SaveLevelData(string name, Vector3 index)
    {
        PlayerPrefs.SetString(name, $"{index.x},{index.y},{index.z}");
        PlayerPrefs.Save();
    }

    public static void SaveSectionData(string name, Vector2 index)
    {
        PlayerPrefs.SetString(name, $"{index.x},{index.y}");
        PlayerPrefs.Save();
    }

    public static void SaveCategoryData(string name, int index)
    {
        PlayerPrefs.SetInt(name, index);
        PlayerPrefs.Save();
    }

    public static void ClearGameLevelData(GameLevelData levelData)
    {
        foreach (var board in levelData.levelRecords)
        {
            PlayerPrefs.SetString(board.levelName, "-1,-1,-1");
        }

        PlayerPrefs.SetString(levelData.levelRecords[0].levelName, "0,0,0");
        PlayerPrefs.Save();
    }

    // public static void ClearGameSectionData(GameSectionData sectionData)
    // {
    //     foreach (var levels in sectionData.levelRecords)
    //     {
    //         PlayerPrefs.SetString(levels.levelName, "-1,-1,-1");
    //     }

    //     PlayerPrefs.SetString(sectionData.levelRecords[0].levelName, "0,0,0");
    //     PlayerPrefs.Save();
    // }

    public static void ClearGameCategoryData(GameCategoryData categoryData)
    {
        foreach (var category in categoryData.categoryRecords)
        {
            PlayerPrefs.SetInt(category.selectedCategoryName, -1);
        }

        PlayerPrefs.Save();
    }

    public static void ClearGameData(GameCategoryData categoryData)
    {
        foreach (var category in categoryData.categoryRecords)
        {

            foreach (var levels in category.sectionDatas.sectionRecords)
            {

                foreach (var board in levels.levelDatas.levelRecords)
                {
                    PlayerPrefs.SetString(board.levelName, "-1,-1,-1");
                }
            }
        }


        if (categoryData.categoryRecords.Count > 0)
        {
            var firstCategory = categoryData.categoryRecords[0];
            SaveCategoryData(firstCategory.selectedCategoryName, 0);

            if (firstCategory.sectionDatas.sectionRecords.Count > 0)
            {
                var firstSection = firstCategory.sectionDatas.sectionRecords[0];
                SaveSectionData(firstSection.sectionName, new Vector2(0, 0));

                if (firstSection.levelDatas.levelRecords.Count > 0)
                {
                    var firstBoard = firstSection.levelDatas.levelRecords[0];
                    SaveLevelData(firstBoard.levelName, new Vector3(0, 0, 0));
                }
            }
        }


        PlayerPrefs.Save();
    }

    public static void ResetFirstLevel(string name)
    {
        PlayerPrefs.SetString(name, "0,0,0");
        PlayerPrefs.Save();
    }
}
