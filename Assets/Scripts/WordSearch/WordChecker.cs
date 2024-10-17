using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordChecker : MonoBehaviour
{
    public GameData currentGameData;

    private GameDataSelector gameDataSelector;

    [SerializeField] private GameDataSave dataSave;
    private string _word;

    private int _assignedPoints = 0;
    private int _completedWords = 0;
    private Ray _rayUp, _rayDown, _rayLeft, _rayRight;
    private Ray _rayDiagonalUpLeft, _rayDiagonalUpRight, _rayDiagonalDownLeft, _rayDiagonalDownRight;
    private Ray _currentRay = new Ray();
    private Vector3 _rayStartPositon;

    private List<int> _correctSquaresList = new();

    private void OnEnable()
    {
        GameEvents.OnCheckSquare += SquareSelected;
        GameEvents.OnClearSelection += ClearSelection;
        GameEvents.OnBoardComplete += () => _completedWords++;
        GameEvents.OnLoadNextLevel += LoadNextLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= SquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
        GameEvents.OnBoardComplete -= () => _completedWords++;
        GameEvents.OnLoadNextLevel -= LoadNextLevel;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene("WordSearchGameScene");
    }

    void Start()
    {
        _assignedPoints = 0;
        _completedWords = 0;
        gameDataSelector = FindAnyObjectByType<GameDataSelector>();
        dataSave = gameDataSelector.data;
    }

    void Update()
    {
        if (_assignedPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(_rayUp.origin, _rayUp.direction * 4);
            Debug.DrawRay(_rayDown.origin, _rayDown.direction * 4);
            Debug.DrawRay(_rayLeft.origin, _rayLeft.direction * 4);
            Debug.DrawRay(_rayRight.origin, _rayRight.direction * 4);
            Debug.DrawRay(_rayDiagonalUpLeft.origin, _rayDiagonalUpLeft.direction * 4);
            Debug.DrawRay(_rayDiagonalUpRight.origin, _rayDiagonalUpRight.direction * 4);
            Debug.DrawRay(_rayDiagonalDownLeft.origin, _rayDiagonalDownLeft.direction * 4);
            Debug.DrawRay(_rayDiagonalDownRight.origin, _rayDiagonalDownRight.direction * 4);
        }
    }

    public void SquareSelected(string letter, Vector3 position, int index)
    {
        if (_assignedPoints == 0)
        {
            _rayStartPositon = position;
            _correctSquaresList.Add(index);
            _word += letter;

            _rayUp = new Ray(new Vector2(position.x, position.y), new Vector2(0, 1));
            _rayDown = new Ray(new Vector2(position.x, position.y), new Vector2(0, -1));
            _rayLeft = new Ray(new Vector2(position.x, position.y), new Vector2(-1, 0));
            _rayRight = new Ray(new Vector2(position.x, position.y), new Vector2(1, 0));
            _rayDiagonalUpLeft = new Ray(new Vector2(position.x, position.y), new Vector2(-1, 1));
            _rayDiagonalUpRight = new Ray(new Vector2(position.x, position.y), new Vector2(1, 1));
            _rayDiagonalDownLeft = new Ray(new Vector2(position.x, position.y), new Vector2(-1, -1));
            _rayDiagonalDownRight = new Ray(new Vector2(position.x, position.y), new Vector2(1, -1));
        }
        else if (_assignedPoints == 1)
        {
            _correctSquaresList.Add(index);
            _currentRay = SelectRay(_rayStartPositon, position);
            GameEvents.SelectSquareMethod(position);
            _word += letter;
            CheckWord();
        }
        else
        {
            if (IsPointOnTheRay(_currentRay, position))
            {
                _correctSquaresList.Add(index);
                GameEvents.SelectSquareMethod(position);
                _word += letter;
                CheckWord();
            }
            else
            {
                _word = string.Empty;
                _correctSquaresList.Clear();
                _assignedPoints = 0;
                return;
            }
        }

        _assignedPoints++;
    }

    private void CheckWord()
    {
        foreach (var word in currentGameData.selectedBoardData.SearchWords)
        {
            if (word.Word == _word)
            {
                GameEvents.CorrectWordMethod(_word, _correctSquaresList);
                _completedWords++;
                _word = string.Empty;
                _correctSquaresList.Clear();
                CheckBoardComplete();
                return;
            }
        }
    }

    private bool IsPointOnTheRay(Ray currentRay, Vector3 point)
    {
        var hits = Physics.RaycastAll(currentRay, 100f);
        foreach (var hit in hits)
        {
            if (hit.transform.position == point)
            {
                return true;
            }
        }
        return false;
    }

    private Ray SelectRay(Vector2 firstPosion, Vector2 secondPosition)
    {

        var direction = (secondPosition - firstPosion).normalized;
        float tolerance = 0.01f;

        if (Math.Abs(direction.x) < tolerance && Math.Abs(direction.y - 1f) < tolerance)
        {
            return _rayUp;
        }

        if (Math.Abs(direction.x) < tolerance && Math.Abs(direction.y + 1f) < tolerance)
        {
            return _rayDown;
        }

        if (Math.Abs(direction.x + 1f) < tolerance && Math.Abs(direction.y) < tolerance)
        {
            return _rayLeft;
        }

        if (Math.Abs(direction.x - 1f) < tolerance && Math.Abs(direction.y) < tolerance)
        {
            return _rayRight;
        }

        if (direction.x > 0 && direction.y > 0)
        {
            return _rayDiagonalUpRight;
        }

        if (direction.x > 0 && direction.y < 0)
        {
            return _rayDiagonalDownRight;
        }

        if (direction.x < 0 && direction.y > 0)
        {
            return _rayDiagonalUpLeft;
        }

        if (direction.x < 0 && direction.y < 0)
        {
            return _rayDiagonalDownLeft;
        }
        return _rayUp;
    }

    private void ClearSelection()
    {
        _assignedPoints = 0;
        _correctSquaresList.Clear();
        _word = string.Empty;
    }

    private void CheckBoardComplete()
    {
        bool loadNextLevel = false;
        if (currentGameData.selectedBoardData.SearchWords.Count == _completedWords)
        {
            Section section = gameDataSelector.savingFile.LoadSection(currentGameData.selectedCategoryName, currentGameData.selectedSectionName);
            Level level = gameDataSelector.savingFile.LoadLevel(currentGameData.selectedCategoryName, currentGameData.selectedSectionName, currentGameData.selectedLevelName);

            if (level == null)
            {
                Debug.LogError("Level is null");
                return;
            }
            Debug.Log("Level is not null" + level.Name);
            int currentBoardIndex = 0;
            foreach (var board in level.Boards)
            {
                if (currentGameData.selectedBoardName == board.Name)
                {
                    Debug.Log(currentGameData.selectedBoardName + "=" + board.Name);
                    currentBoardIndex = board.index;
                    int nextBoardIndex = board.index + 1;
                    if (nextBoardIndex < level.Boards.Count)
                    {
                        Debug.Log("Current board index: " + currentBoardIndex);
                        Debug.Log("Current board name: " + level.Boards[currentBoardIndex].Name);
                        Debug.Log("Next board index: " + nextBoardIndex);
                        Debug.Log("Next board name: " + level.Boards[nextBoardIndex].Name);
                        currentGameData.selectedBoardName = level.Boards[nextBoardIndex].Name;
                        currentGameData.selectedBoardData = level.Boards[nextBoardIndex].boardData;

                        int score = (int)(level.Boards[nextBoardIndex].boardData.timeInSeconds * 10);

                        BoardList currentBoard = level.Boards[currentBoardIndex];
                        currentBoard.isCompleted = true;
                        currentBoard.Score = score;
                        gameDataSelector.savingFile.SaveBoardData(currentGameData.selectedCategoryName, currentGameData.selectedSectionName, currentGameData.selectedLevelName, currentBoard);
                        BoardList nextBoard = level.Boards[nextBoardIndex];
                        nextBoard.isLock = false;
                        nextBoard.index = nextBoardIndex;
                        gameDataSelector.savingFile.SaveBoardData(currentGameData.selectedCategoryName, currentGameData.selectedSectionName, currentGameData.selectedLevelName, nextBoard);
                        loadNextLevel = true;
                    }
                    else
                    {
                        SceneManager.LoadScene("MainMenu");
                    }
                    break;
                }
            }

            int currentLevelSize = section.Levels.Find(l => l.Name == currentGameData.selectedLevelName).Boards.Count;

            Debug.Log("Current board index: " + currentBoardIndex);
            Debug.Log("Current level size: " + currentLevelSize);
            if ((currentBoardIndex + 1) >= currentLevelSize)
            {
                int currentLevelIndex = 0;
                for (int i = 0; i < section.Levels.Count; i++)
                {
                    if (section.Levels[i].Name == currentGameData.selectedLevelName)
                    {
                        currentLevelIndex = i;
                        break;
                    }
                }

                if ((currentLevelIndex + 1) < section.Levels.Count)
                {
                    Level currentLevel = section.Levels[currentLevelIndex];
                    // currentLevel.isLock = true;
                    gameDataSelector.savingFile.SaveLevelData(currentGameData.selectedCategoryName, currentGameData.selectedSectionName, currentLevel);
                    Level nextLevel = section.Levels[currentLevelIndex + 1];
                    nextLevel.isLock = false;
                    gameDataSelector.savingFile.SaveLevelData(currentGameData.selectedCategoryName, currentGameData.selectedSectionName, nextLevel);
                    loadNextLevel = true;
                }
                else
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
            else
            {
                Debug.Log("Board complete");
                GameEvents.BoardCompleteMethod();
            }
            if (loadNextLevel)
            {
                GameEvents.OnUnlockNextLevelMethod();
            }
        }
    }
    // private void CheckBoardComplete()
    // {
    //     bool loadNextLevel = false;
    //     if (currentGameData.selectedBoardData.SearchWords.Count == _completedWords)
    //     {

    //         var categoryName = currentGameData.selectedCategoryName;
    //         var sectionName = currentGameData.selectedSectionName;
    //         var levelName = currentGameData.selectedLevelName;

    //         Section section = gameDataSelector.savingFile.LoadSection(categoryName, sectionName);
    //         Level level = gameDataSelector.savingFile.LoadLevel(categoryName, sectionName, levelName);

    //         var currentBoardIndex = 0;
    //         var nextBoardIndex = -1;
    //         var currentLevelIndex = 0;
    //         bool readNextLevelName = false;
    //         foreach (var dataCat in gameCategoryData.categoryRecords)
    //         {
    //             if (dataCat.selectedCategoryName == categoryName)
    //             {
    //                 int idxCategory = DataSaver.ReadCategoryCurrentIndexValues(categoryName);
    //                 foreach (var section in dataCat.sectionDatas.sectionRecords)
    //                 {
    //                     if (section.sectionName == sectionName)
    //                     {
    //                         var tempIdx = DataSaver.ReadSectionCurrentIndexValues(sectionName);
    //                         int idxSection = (int)tempIdx.y;
    //                         // foreach(var dataLevel in gameLevelData.levelDatas){
    //                         //     if(dataLevel.levelName == levelName){
    //                         //         nextBoardIndex = currentBoardIndex + 1;
    //                         //         if(nextBoardIndex < dataLevel.boardData.Count){
    //                         //             DataSaver.SaveCategoryData(levelName, nextBoardIndex);
    //                         //             loadNextLevel = true;
    //                         //         }
    //                         //     }
    //                         // }
    //                         for (int i = 0; i < section.levelDatas.levelRecords.Count; i++)
    //                         {
    //                             if (readNextLevelName)
    //                             {
    //                                 // nextBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(gameLevelData.levelRecords[i].levelName);
    //                                 var temp = DataSaver.ReadLevelCurrentIndexValues(section.levelDatas.levelRecords[i].levelName);
    //                                 nextBoardIndex = (int)temp.z;
    //                                 readNextLevelName = false;

    //                             }

    //                             if (section.levelDatas.levelRecords[i].levelName == levelName)
    //                             {
    //                                 // nextBoardIndex = currentBoardIndex + 1;
    //                                 // if(nextBoardIndex < gameLevelData.levelRecords.Count){
    //                                 //     DataSaver.SaveCategoryData(gameLevelData.levelRecords[nextBoardIndex].levelName, 0);
    //                                 //     loadNextLevel = true;
    //                                 // }
    //                                 // else{
    //                                 //     readNextLevelName = true;
    //                                 // }
    //                                 readNextLevelName = true;
    //                                 currentLevelIndex = i;
    //                             }
    //                         }

    //                         var currentLevelSize = section.levelDatas.levelRecords[currentLevelIndex].boardData.Count; // dem so board cua level hien tai
    //                         if (currentBoardIndex < currentLevelSize)
    //                         {
    //                             currentBoardIndex++;
    //                         }
    //                         DataSaver.SaveLevelData(section.levelDatas.levelRecords[currentLevelIndex].levelName, new Vector3(idxCategory, idxSection, currentBoardIndex));
    //                         DataSaver.SaveSectionData(section.sectionName, new Vector2(idxCategory, currentLevelIndex));
    //                         DataSaver.SaveCategoryData(categoryName, currentBoardIndex);

    //                         //unlock next level
    //                         if (currentBoardIndex >= currentLevelSize)
    //                         {
    //                             currentLevelIndex++;
    //                             if (currentLevelIndex < section.levelDatas.levelRecords.Count)
    //                             { // if not last level
    //                                 levelName = section.levelDatas.levelRecords[currentLevelIndex].levelName;
    //                                 currentBoardIndex = 0;
    //                                 loadNextLevel = true;

    //                                 if (nextBoardIndex <= 0)
    //                                 {

    //                                     // DataSaver.SaveCategoryData(levelName, currentBoardIndex);
    //                                     DataSaver.SaveLevelData(levelName, new Vector3(idxCategory, idxSection, currentBoardIndex));
    //                                 }
    //                             }
    //                             else
    //                             {
    //                                 SceneManager.LoadScene("MainMenu");
    //                             }
    //                         }
    //                         else
    //                         {
    //                             GameEvents.BoardCompleteMethod();
    //                         }

    //                         if (loadNextLevel)
    //                         {
    //                             GameEvents.OnUnlockNextLevelMethod();
    //                         }

    //                     }
    //                 }
    //             }
    //         }
    //     }
    // }
}
