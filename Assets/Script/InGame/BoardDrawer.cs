using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardDrawer : MonoBehaviour
{
    private GameBoard gameBoard;

    private ManaStone[,] _manaStoneImages;

    private int _gameSizeRow;
    private int _gameSizeCol;
    [SerializeField]
    private ManaStone _manaStoneEx;
    [SerializeField]
    private Transform _manaZone;

    [SerializeField]
    private Transform _verticalCap;
    [SerializeField]
    private Transform horizontalCap;
    [SerializeField]
    private LayoutGroup layoutGroup;

    public void Init(GameBoard gameBoard, int _gameSizeRow, int _gameSizeCol)
    {
        if (this.gameBoard == gameBoard)
            return;

        this.gameBoard = gameBoard;
        this._gameSizeRow = _gameSizeRow;
        this._gameSizeCol = _gameSizeCol;

        _manaStoneImages = new ManaStone[_gameSizeRow, _gameSizeCol];
        for (int i = 0; i < _gameSizeRow; i++)
        {
            for (int j = 0; j < _gameSizeCol; j++)
            {
                _manaStoneImages[i, j] = Instantiate(_manaStoneEx, _manaZone);
                _manaStoneImages[i, j].Init(i, j);
            }
        }

        _manaStoneEx.gameObject.SetActive(false);
    }

    public void DrawBoard()
    {
        for (int i = 0; i < _gameSizeRow; i++)
            for (int j = 0; j < _gameSizeCol; j++)
                _manaStoneImages[i, j].SetColor(gameBoard._BoardData[i, j]);
    }
}
