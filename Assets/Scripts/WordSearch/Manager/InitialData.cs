using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialData : MonoBehaviour
{
    public GameData gameData;
    void Awake()
    {
        // // Xóa tất cả dữ liệu trong PlayerPrefs
        // PlayerPrefs.DeleteAll();

        // // Lưu lại để đảm bảo thay đổi được áp dụng
        // PlayerPrefs.Save();

        // Debug.Log("PlayerPrefs đã được xóa!");
        gameData.selectedCategoryName = "Life Sciences";
        gameData.selectedSectionName = "Fit for life";
        gameData.newCategoryName = "";
        gameData.newSectionName = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
