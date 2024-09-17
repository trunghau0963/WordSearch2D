using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CategoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public Image progressBarFilling;

    public Text textProgress;

    public void Init(string categoryName, float progress, string progressText)
    {
        text.text = categoryName;
        progressBarFilling.fillAmount = progress;
        textProgress.text = progressText;
    }
}
