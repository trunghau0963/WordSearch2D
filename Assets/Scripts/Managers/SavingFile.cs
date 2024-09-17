using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class GameDataSave
{
    public List<Category> DataSet;
}

[System.Serializable]
// [CreateAssetMenu]
public class Category
{
    public string CategoryName;
    public bool isLock = true;
    public List<Section> Sections;
}

[System.Serializable]
public class Section
{
    public string SectionName;
    public bool isLock = true;
    public List<Level> Levels;
}

[System.Serializable]
// [CreateAssetMenu]
public class Level
{
    public string Name;
    public bool isLock = true;
    public List<BoardList> Boards;
}

[System.Serializable]
public class BoardList
{

    public string Name;
    public bool isLock;
    public int Score;
    public BoardData boardData;
}

public class SavingFile : MonoBehaviour
{
    public string categoryName;
    public string sectionName;
    public string levelName;

    public string boardName;
    public int score;
    public int time;
    public bool isLock;
    public GameDataSave data;

    public GameObject CategoryList;
    public GameObject SectionList;
    public GameObject LevelList;

    public string nameFile;

    void Start()
    {
        // data = ScriptableObject.CreateInstance<GameData>();
        // data.Initialize();
        data = LoadData();
        // SaveData();
    }

    void Update()
    {
    }



    public void Save(string categoryName, string sectionName, string levelName, string boardName, int score, int time, bool isLock)
    {
        Category category = data.DataSet.Find(c => c.CategoryName == categoryName);
        if (category == null)
        {
            category = new Category();
            category.CategoryName = categoryName;
            category.Sections = new List<Section>();
            data.DataSet.Add(category);
        }

        Section section = category.Sections.Find(s => s.SectionName == sectionName);
        if (section == null)
        {
            section = new Section();
            section.SectionName = sectionName;
            section.Levels = new List<Level>();
            category.Sections.Add(section);
        }

        Level level = section.Levels.Find(l => l.Name == levelName);
        if (level == null)
        {
            level = new Level();
            level.Name = levelName;
            level.Boards = new List<BoardList>();
            section.Levels.Add(level);
        }

        BoardList board = level.Boards.Find(b => b.Name == boardName);
        if (board == null)
        {
            board = new BoardList();
            board.Name = boardName;
            board.Score = score;
            board.isLock = isLock;
            level.Boards.Add(board);
        }

        Debug.Log("Data Saved");
    }

    public void GetScore(string categoryName, string sectionName, string levelName, string boardName)
    {
        Category category = data.DataSet.Find(c => c.CategoryName == categoryName);
        if (category == null)
        {
            Debug.Log("Category not found");
            return;
        }

        Section section = category.Sections.Find(s => s.SectionName == sectionName);
        if (section == null)
        {
            Debug.Log("Section not found");
            return;
        }

        Level level = section.Levels.Find(l => l.Name == levelName);
        if (level == null)
        {
            Debug.Log("Level not found");
            return;
        }

        BoardList board = level.Boards.Find(b => b.Name == boardName);
        if (board == null)
        {
            Debug.Log("Board not found");
            return;
        }

        Debug.Log("Score: " + board.Score);

    }

    public GameDataSave LoadData()
    {
        string file = nameFile + ".json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        GameDataSave data = new GameDataSave();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameDataSave>(json);
            Debug.Log("Data Loaded");
        }
        else
        {
            // GameDataSave data = new GameDataSave();
            // data.DataSet = new List<Category>();
            Debug.Log("No Data Found");
        }

        return data;
    }
    public void SaveData()
    {
        string file = nameFile + ".json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        string json = JsonUtility.ToJson(data);  // true for pretty-printing the JSON
        File.WriteAllText(filePath, json);

        Debug.Log("Data Saved to: " + filePath);
    }
}