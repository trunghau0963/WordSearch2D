using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData{
    public List<LevelScore> levelScores = new();
}

[System.Serializable]
public class LevelScore{
    public int level;
    public int score;
}
public class SavingFile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameData data = new();
    public int level;
    public int score;
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1)){
            SaveData();
        }
        if(Input.GetKeyUp(KeyCode.Alpha2)){
            Save(level, score);
        } 
        if(Input.GetKeyUp(KeyCode.Alpha3)){
            GetScore(level);
        }
    }

    public void Save(int level, int score){
        foreach(LevelScore levelScore in data.levelScores){
            if(levelScore.level == level){
                if(levelScore.score < score){
                    levelScore.score = score;
                    SaveData();
                }
                Debug.Log("Score smaller than previous score");
                return;
            }
        }

        LevelScore newLevelScore = new LevelScore();
        newLevelScore.level = level;
        newLevelScore.score = score;
        data.levelScores.Add(newLevelScore);
        SaveData();
    }

    public void GetScore(int level){
        foreach(LevelScore levelScore in data.levelScores){
            if(levelScore.level == level){
                Debug.Log("Score: " + levelScore.score);
                return;
            }
        }
        Debug.Log("No Score Found");
    }
    public void LoadData(){
        string file = "save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file); 

        if(!File.Exists(filePath)){
            File.WriteAllText(filePath, "");
        }

        data = JsonUtility.FromJson<GameData>(File.ReadAllText(filePath));
        Debug.Log("Data Loaded");
    }

    public void SaveData(){
        string file = "save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(filePath, json);
        Debug.Log("Data Saved to: " + filePath);
    }
}
