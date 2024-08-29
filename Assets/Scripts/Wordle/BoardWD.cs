using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class BoardWD : MonoBehaviour
{
    private Row[] rows;
    private int rowIndex;
    private int columnIndex;

    private string[] solutions;
    private string[] validWords;
    private string word;

    [Header("Tiles")]
    public Tiles.State emptyState;
    public Tiles.State occupiedState;
    public Tiles.State correctState;
    public Tiles.State wrongSpotState;
    public Tiles.State incorrectState;

    [Header("UI")]
    public GameObject tryAgainButton;
    public GameObject newWordButton;
    public GameObject invalidWordText;

    // Start is called before the first frame update
    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        // LoadData();
        // NewGame();
        Key.OnKeyPressed += KeyPressCallback;
        // Debug.Log("BoardWD Start");
        LoadData();
        NewGame();
    }


    // Update is called once per frame

    private void update()
    {
        // Row currentRow = rows[rowIndex];
        // if(columnIndex >= currentRow.tiles.Length)
        // {
        //     CheckWord();
        // }
    }

    private void LoadData()
    {
        TextAsset textFile = Resources.Load("official_wordle_common") as TextAsset;
        solutions = textFile.text.Split('\n');

        textFile = Resources.Load("official_wordle_all") as TextAsset;
        validWords = textFile.text.Split('\n');
    }

    public void NewGame()
    {
        ClearBoard();
        SetRandomWord();

        enabled = true;
    }

    public void TryAgain()
    {
        ClearBoard();
        enabled = true;
    }

    private void SetRandomWord()
    {
        word = solutions[Random.Range(0, solutions.Length)];
        word = word.ToLower().Trim();
        Debug.Log("Word: " + word);
    }

    private void KeyPressCallback(string letter)
    {
        Row currentRow = rows[rowIndex];
        if (letter == "Delete")
        {
            if (columnIndex > 0)
            {
                columnIndex--;
                currentRow.tiles[columnIndex].SetLetter("");
                currentRow.tiles[columnIndex].SetState(emptyState);
            }
            return;
        }

        if (columnIndex >= currentRow.tiles.Length)
        {
            // if(rowIndex < rows.Length)
            // {
            //     rowIndex++;
            //     columnIndex = 0;
            // }
            if (letter == "Enter")
            {
                SubmitRow(currentRow);
                return;
            }
        }
        if (rowIndex < rows.Length && columnIndex < rows[rowIndex].tiles.Length && letter != "Enter" && letter != "Delete")
        {
            currentRow.tiles[columnIndex].SetLetter(letter.ToString().ToUpper());
            currentRow.tiles[columnIndex].SetState(occupiedState);
            columnIndex++;
            // Debug.Log("Letter set: " + letter);
        }
    }

    private void SubmitRow(Row row)
    {
        // Debug.Log("SubmitRow"); 
        // if (!IsValidWord(row.word))
        // {
        //     invalidWordText.SetActive(true);
        //     return;
        // }

        string remaining = word;

        Debug.Log("Word: " + word);

        // Check correct/incorrect letters first
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tiles tile = row.tiles[i];
            string letter = tile.letter.ToLower();
            // Debug.Log("Letter: " + letter + " " + letter.Length);
            // Debug.Log("Word letter: " + word[i].ToString() + " and " + word[i].ToString().Length);
            // Debug.Log("chheck correct" + letter == word[i].ToString());
            if (letter == word[i].ToString())
            {
                // Debug.Log("Correct letter: " + letter);
                // Correct state
                tile.SetState(correctState);

                // remaining = remaining.Remove(i, 1);
                // remaining = remaining.Insert(i, " ");
            }
            else if (!word.Contains(letter))
            {
                tile.SetState(incorrectState);
            }
        }

        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tiles tile = row.tiles[i];

            // Debug.Log("Tile state: " + tile.state);   

            if (tile.state != correctState && tile.state != incorrectState)
            {
                // Debug.Log("Wrong spot");
                string letter = tile.letter.ToLower();
                if (remaining.Contains(letter))
                {
                    tile.SetState(wrongSpotState);

                    // int index = remaining.IndexOf(letter);
                    // remaining = remaining.Remove(index, 1);
                    // remaining = remaining.Insert(index, " ");
                }
                else
                {
                    tile.SetState(incorrectState);
                }
            }
        }

        if (HasWon(row))
        {
            enabled = false;
        }

        rowIndex++;
        columnIndex = 0;

        if (rowIndex >= rows.Length)
        {
            enabled = false;
        }
    }

    private bool IsValidWord(string word)
    {
        for (int i = 0; i < validWords.Length; i++)
        {
            if (string.Equals(word, validWords[i], System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private bool HasWon(Row row)
    {
        for (int i = 0; i < row.tiles.Length; i++)
        {
            if (row.tiles[i].state != correctState)
            {
                return false;
            }
        }

        return true;
    }

    private void ClearBoard()
    {
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].tiles.Length; col++)
            {
                rows[row].tiles[col].SetLetter("");
                rows[row].tiles[col].SetState(emptyState);
            }
        }

        rowIndex = 0;
        columnIndex = 0;
    }

    private void OnEnable()
    {
        tryAgainButton.SetActive(false);
        newWordButton.SetActive(false);
    }

    private void OnDisable()
    {
        tryAgainButton.SetActive(true);
        newWordButton.SetActive(true);
    }


}
