using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CategoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public Image progressBarFilling;
    public GameData gameData;
    public Text textProgress;
    // [SerializeField] private GameObject panel;
    ReviewNavigation navigation;



    public void Init(string categoryName, float progress, string progressText)
    {
        text.text = categoryName;
        progressBarFilling.fillAmount = progress;
        textProgress.text = progressText;
        gameObject.name = categoryName;
    }

    void Start()
    {
        navigation = FindAnyObjectByType<ReviewNavigation>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        gameData.selectedCategoryName = gameObject.name;
        navigation.GoToSection();
    }
}
