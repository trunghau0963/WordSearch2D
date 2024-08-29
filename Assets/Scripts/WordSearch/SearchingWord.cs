using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SearchingWord : MonoBehaviour
{
    public Text displayText;
    public Image crossLine;
    private string _word;
    void Start()
    {
        crossLine.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        GameEvents.OnCorrectWord += CorrectWord;
    }

    private void OnDisable()
    {
        GameEvents.OnCorrectWord -= CorrectWord;
    }

    public void Setword(string word){
        _word = word;
        displayText.text = word;
    }

    private void CorrectWord(string word, List<int> squareIdx){
        if(word == _word){
            print("Correct Word");  
            crossLine.gameObject.SetActive(true);
            if(crossLine.gameObject.activeSelf){
                print("Cross Line Active");
            }
            else{
                print("Cross Line Not Active");
            }
        }
    }
}
