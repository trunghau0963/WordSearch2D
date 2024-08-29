using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetData : MonoBehaviour
{
     public Button resetButton;

    void Start()
    {
        // Gán hàm ResetData vào sự kiện onClick của button
        resetButton.onClick.AddListener(Reset);
    }

    void Reset()
    {
        // Xóa tất cả dữ liệu trong PlayerPrefs
        PlayerPrefs.DeleteAll();

        // Cập nhật lại các cài đặt hoặc giá trị trong game về trạng thái ban đầu (nếu cần)
        // Ví dụ:
        // PlayerPrefs.SetInt("level", 1);
        // PlayerPrefs.SetFloat("volume", 1.0f);
        // PlayerPrefs.SetString("playerName", "DefaultName");

        // Lưu lại những thay đổi (nếu có)
        PlayerPrefs.Save();

        // In ra console để kiểm tra
        Debug.Log("PlayerPrefs đã được reset.");
    }
}
