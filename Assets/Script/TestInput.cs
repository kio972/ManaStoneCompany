using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    public int targetRow;
    public int targetCol;

    public GameBoard board;

    public void SetRowCol(int row, int col)
    {
        targetRow = row;
        targetCol = col;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            board?.RotateBoard(targetRow, targetCol, 1);
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            board?.RotateBoard(targetRow, targetCol, 0);
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
            board?.RotateBoard(targetRow, targetCol, 2);
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            board?.RotateBoard(targetRow, targetCol, 3);
    }

}
