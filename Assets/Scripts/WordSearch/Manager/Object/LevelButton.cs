using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Image isLock;
    public Text Name;

    public void Init(string levelName, bool isLocked)
    {
        Name.text = levelName;
        isLock.gameObject.SetActive(isLocked);
    }
}
