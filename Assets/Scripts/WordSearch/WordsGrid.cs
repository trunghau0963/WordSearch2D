using System.Collections;
using System.Collections.Generic;
using System.Numerics;
// using Unity.VisualScripting;
// using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class WordsGrid : MonoBehaviour
{
    public GameData currentGameData;
    public GameObject gridSquarePrefab;
    public AlphabetData alphabetData;

    public float squareOffset = 0.0f;
    public float topPosition;

    private List<GameObject> _squareList = new List<GameObject>();

    void Start() {
        SpawnGridSquares();
        SetSquarePostition();
    }

    private void SetSquarePostition(){
        var squareRect = _squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = _squareList[0].GetComponent<Transform>();

        var offset = new UnityEngine.Vector2
        {
            x = (squareRect.width * squareTransform.localScale.x + squareOffset) * 0.01f,
            y = (squareRect.height * squareTransform.localScale.y + squareOffset) * 0.01f
        };

        var startPosition = GetFirstSquarePosition();

        int columnNumber = 0;
        int rowNumber = 0;

        foreach(var square in _squareList){
            if(rowNumber + 1 > currentGameData.selectedBoardData.Rows){
                columnNumber++;
                rowNumber = 0;
            }
            var positionX = startPosition.x + offset.x * columnNumber;
            var positionY = startPosition.y - offset.y * rowNumber;

            square.GetComponent<Transform>().position = new UnityEngine.Vector2(positionX, positionY);
            rowNumber++;
        }
    }

    private UnityEngine.Vector2 GetFirstSquarePosition(){

        var startPosition = new UnityEngine.Vector2(0f,transform.position.y);

        var squareRect = _squareList[0].GetComponent<SpriteRenderer>().sprite.rect;

        var squareTransform = _squareList[0].GetComponent<Transform>();

        var squareSize = new UnityEngine.Vector2(0f,0f);

        squareSize.x = squareRect.width * squareTransform.localScale.x;
        squareSize.y = squareRect.height * squareTransform.localScale.y;

        var midWidthPosition = (((currentGameData.selectedBoardData.Columns - 1) * squareSize.x) / 2) * 0.01f;
        var midWidthHeight = (((currentGameData.selectedBoardData.Rows - 1 )* squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? -midWidthPosition : midWidthPosition;
        startPosition.y += midWidthHeight;

        return startPosition;
    }

    private void SpawnGridSquares(){
        if(currentGameData != null){
            var squareScale = GetSquareScale(new UnityEngine.Vector3(1.5f, 1.5f, 0.1f));
            foreach(var square in currentGameData.selectedBoardData.Boards){
                foreach(var squareLetter in square.Row){
                    var normalLetter = alphabetData.AlphabetNormal.Find(x => x.Letter == squareLetter);
                    var selectedLetter = alphabetData.AlphabetWrong.Find(x => x.Letter == squareLetter);
                    var correctLetter = alphabetData.AlphabetHighlighted.Find(x => x.Letter == squareLetter);

                    if(normalLetter.Image == null || selectedLetter.Image == null){
                        Debug.LogError("Missing image for letter: " + squareLetter);

                        #if UNITY_EDITOR

                        if(UnityEditor.EditorApplication.isPlaying){
                            UnityEditor.EditorApplication.isPlaying = false;
                        }

                        #endif
                    }

                    else {
                        _squareList.Add(Instantiate(gridSquarePrefab));
                        _squareList[_squareList.Count - 1].GetComponent<GridSquare>().SetSprite(normalLetter, selectedLetter, correctLetter);
                        _squareList[_squareList.Count - 1].transform.SetParent(transform);
                        _squareList[_squareList.Count - 1].GetComponent<Transform>().position = new UnityEngine.Vector3(0f,0f,0f);
                        _squareList[_squareList.Count - 1].transform.localScale = squareScale;
                        _squareList[_squareList.Count - 1].GetComponent<GridSquare>().SetIndex(_squareList.Count - 1);
                    }
                }
            }
        }
    }

    private UnityEngine.Vector3 GetSquareScale(UnityEngine.Vector3 defaultScale){
        var finalScale = defaultScale;
        var adjustment = 0.01f;

        while(ShouldScaleDown(finalScale)){
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            if(finalScale.x <= 0 || finalScale.y <= 0){
                finalScale.x = adjustment;
                finalScale.y = adjustment;
                return finalScale;
            }
        }
        return finalScale;
    }

    private bool ShouldScaleDown(UnityEngine.Vector3 targetScale){
        var squareRect = gridSquarePrefab.GetComponent<SpriteRenderer>().sprite.rect;
        var squareSize = new UnityEngine.Vector2(0f,0f);
        var startPosition = new UnityEngine.Vector2(0f,0f); 

        squareSize.x = (squareRect.width * targetScale.x) + squareOffset;
        squareSize.y = (squareRect.height * targetScale.y) + squareOffset;

        var midWidthPosition = ((currentGameData.selectedBoardData.Columns * squareSize.x) / 2) * 0.01f;
        var midHeightPosition = ((currentGameData.selectedBoardData.Rows * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? -midWidthPosition : midWidthPosition;
        startPosition.y = midHeightPosition;

        return startPosition.x < -GetHalfScreenWidth() || startPosition.y > topPosition;


    }

    private float GetHalfScreenWidth(){
        float height = Camera.main.orthographicSize * 2;
        float width = 1.7f * height * Screen.width / Screen.height;
        return width / 2;
    }
}
