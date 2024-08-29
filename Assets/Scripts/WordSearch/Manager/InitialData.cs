using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialData : MonoBehaviour
{
    public GameData gameData;
    void Start()
    {
        // // Xóa tất cả dữ liệu trong PlayerPrefs
        // PlayerPrefs.DeleteAll();

        // // Lưu lại để đảm bảo thay đổi được áp dụng
        // PlayerPrefs.Save();

        // Debug.Log("PlayerPrefs đã được xóa!");
        gameData.selectedCategoryName = "Life Sciences";
        DataSaver.SaveCategoryData("Life Sciences", 0);
        gameData.selectedSectionName = "Fit For Life";
        DataSaver.SaveSectionData("Fit For Life", new Vector2(0, 0));


    }

    // Update is called once per frame
    void Update()
    {

    }
}
