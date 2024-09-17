using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionButton : MonoBehaviour
{
    public Image isLock;
    public Text title;

    public Image progressBarFilling;

    public void Init(string sectionName, float progress, bool isLocked)
    {
        title.text = sectionName;
        isLock.gameObject.SetActive(isLocked);
        progressBarFilling.fillAmount = progress;
    }

}
