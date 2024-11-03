using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public delegate void EnableSquareSelection();
    public static event EnableSquareSelection OnEnableSquareSelection;
    public static void EnableSquareSelectionMethod()
    {
        OnEnableSquareSelection?.Invoke();
    }

    public delegate void DisableSquareSelection();
    public static event DisableSquareSelection OnDisableSquareSelection;
    public static void DisableSquareSelectionMethod()
    {
        OnDisableSquareSelection?.Invoke();
    }

    public delegate void SelectSquare(UnityEngine.Vector3 position);
    public static event SelectSquare OnSelectSquare;
    public static void SelectSquareMethod(UnityEngine.Vector3 position)
    {
        OnSelectSquare?.Invoke(position);
    }

    public delegate void CheckSquare(string letter, UnityEngine.Vector3 position, int index);
    public static event CheckSquare OnCheckSquare;
    public static void CheckSquareMethod(string letter, UnityEngine.Vector3 position, int index)
    {
        OnCheckSquare?.Invoke(letter, position, index);
    }

    public delegate void ClearSelection();
    public static event ClearSelection OnClearSelection;
    public static void ClearSelectionMethod()
    {
        OnClearSelection?.Invoke();
    }

    public delegate void CorrectWord(string word, List<int> squareIdx);
    public static event CorrectWord OnCorrectWord;
    public static void CorrectWordMethod(string word, List<int> squareIdx)
    {
        OnCorrectWord?.Invoke(word, squareIdx);
    }


    public delegate void BoardComplete();
    public static event BoardComplete OnBoardComplete;
    public static void BoardCompleteMethod()
    {
        if (OnBoardComplete != null)
        {
            OnBoardComplete();
        }
    }


    public delegate void ShowPopup(bool isCompletedLevel);
    public static event ShowPopup OnShowPopup;
    public static void ShowPopupMethod(bool isCompletedLevel)
    {
        if (OnShowPopup != null)
        {
            OnShowPopup(isCompletedLevel);
        }
    }


    public delegate void UnlockNextBoard();
    public static event UnlockNextBoard OnUnlockNextBoard;
    public static void OnUnlockNextBoardMethod()
    {
        if (OnUnlockNextBoard != null)
        {
            OnUnlockNextBoard();
        }
    }

    public delegate void LoadNextBoard();
    public static event LoadNextBoard OnLoadNextBoard;
    public static void LoadNextBoardMethod()
    {
        if (OnLoadNextBoard != null)
        {
            OnLoadNextBoard();
        }
    }


    
    public delegate void GameOver();
    public static event GameOver OnGameOver;
    public static void GameOverlMethod()
    {
        if (OnGameOver != null)
        {
            OnGameOver();
        }
    }

}
