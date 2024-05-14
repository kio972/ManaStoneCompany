using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameBoard : MonoBehaviour
{
    public int _gameSizeRow = 8;
    public int _gameSizeCol = 6;

    private ManaType[,] _boardData;
    public ManaType[,] _BoardData { get => _boardData; }

    private int _forArrangeNumber = 4;

    private readonly int[] dirRow = { 1, -1, 0, 0 };
    private readonly int[] dirCol = { 0, 0, 1, -1 };

    private bool[,] visited;
    private int count;
    private List<(int row, int col)> cords;
    private int loop = 0;

    [SerializeField]
    private BoardDrawer drawer;

    private bool inputLock = false;
    public bool _InputLock { get => inputLock; }

    private int score = 0;
    public int _Score { get => score; }

    public void DFS(ManaType type, int row, int col)
    {
        visited[row, col] = true;
        count++;
        cords.Add((row, col));
        for (int i = 0; i < 4; i++)
        {
            int nextRow = row + dirRow[i];
            int nextCol = col + dirCol[i];
            if (nextRow < 0 || nextRow >= _gameSizeRow || nextCol < 0 || nextCol >= _gameSizeCol)
                continue;
            if (_boardData[nextRow, nextCol] != type || visited[nextRow, nextCol])
                continue;
            DFS(type, nextRow, nextCol);
        }
    }

    public void PopStone(List<(int row, int col)> cords, bool isScore = true)
    {
        foreach((int row, int col) cord in cords)
        {
            _boardData[cord.row, cord.col] = ManaType.None;
        }

        if(isScore)
        {
            int match = cords.Count;
            int loop = this.loop;
            score += (match * 10) * loop;
        }
    }

    public void FillBoard()
    {
        for(int col = 0; col < _gameSizeCol; col++)
        {
            int emptySize = 0;
            List<ManaType> nextCols = new List<ManaType>();
            for(int row = 0; row < _gameSizeRow; row++)
            {
                if (_boardData[row, col] == ManaType.None)
                    emptySize++;
                else
                    nextCols.Add(_boardData[row, col]);
            }

            for (int i = 0; i < emptySize; i++)
                nextCols.Add(SetRamdomManaStone());

            for (int row = 0; row < _gameSizeRow; row++)
                _boardData[row, col] = nextCols[row];
        }
        drawer?.DrawBoard();
        Invoke("SearchBoard", 0.3f);
    }

    public void SearchBoard()
    {
        loop++;
        bool isPopped = false;
        visited = new bool[_gameSizeRow, _gameSizeCol];
        for (int i = 0; i < _gameSizeRow; i++)
        {
            for(int j = 0; j < _gameSizeCol; j++)
            {
                if (visited[i, j] || _boardData[i, j] == ManaType.None)
                    continue;

                count = 0;
                cords = new List<(int row, int col)>();
                ManaType type = _boardData[i, j];
                DFS(type, i, j);
                if (count >= _forArrangeNumber)
                {
                    PopStone(cords);
                    isPopped = true;
                }
            }
        }

        if (isPopped)
            Invoke("FillBoard", 0.3f);
        else
        {
            loop = 0;
            inputLock = false;
            Debug.Log("InputUnLocked");
        }

        drawer?.DrawBoard();
    }

    public void FillBoardDirect()
    {
        for (int col = 0; col < _gameSizeCol; col++)
        {
            int emptySize = 0;
            List<ManaType> nextCols = new List<ManaType>();
            for (int row = 0; row < _gameSizeRow; row++)
            {
                if (_boardData[row, col] == ManaType.None)
                    emptySize++;
                else
                    nextCols.Add(_boardData[row, col]);
            }

            for (int i = 0; i < emptySize; i++)
                nextCols.Add(SetRamdomManaStone());

            for (int row = 0; row < _gameSizeRow; row++)
                _boardData[row, col] = nextCols[row];
        }
        SearchBoardDirect();
    }

    public void SearchBoardDirect()
    {
        bool isPopped = false;
        visited = new bool[_gameSizeRow, _gameSizeCol];
        for (int i = 0; i < _gameSizeRow; i++)
        {
            for (int j = 0; j < _gameSizeCol; j++)
            {
                if (visited[i, j] || _boardData[i, j] == ManaType.None)
                    continue;

                count = 0;
                cords = new List<(int row, int col)>();
                ManaType type = _boardData[i, j];
                DFS(type, i, j);
                if (count >= _forArrangeNumber)
                {
                    PopStone(cords, false);
                    isPopped = true;
                }
            }
        }

        if (isPopped)
            FillBoardDirect();
    }

    public void RotateBoard(int row, int col, int direction)
    {
        inputLock = true;
        Debug.Log("InputLocked");
        int size;
        
        if(direction == 0 || direction == 1)
            size = _gameSizeRow;
        else
            size = _gameSizeCol;

        ManaType[] temp = new ManaType[size];

        if (direction == 0 || direction == 1)
        {
            for (int i = 0; i < size; i++)
            {
                int nextRow = i + dirRow[direction];
                if (nextRow < 0)
                    nextRow = _gameSizeRow - 1;
                else if (nextRow >= _gameSizeRow)
                    nextRow = 0;
                temp[i] = _boardData[nextRow, col];
            }

            for (int i = 0; i < size; i++)
                _boardData[i, col] = temp[i];
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                int nextCol = i + dirCol[direction];

                if (nextCol < 0)
                    nextCol = _gameSizeCol - 1;
                else if (nextCol >= _gameSizeCol)
                    nextCol = 0;
                temp[i] = _boardData[row, nextCol];
            }

            for (int i = 0; i < size; i++)
                _boardData[row, i] = temp[i];
        }

        SearchBoard();
    }

    public ManaType SetRamdomManaStone()
    {
        var manaTypes = System.Enum.GetValues(typeof(ManaType)) as ManaType[];
        int randomType = Random.Range(1, System.Enum.GetValues(typeof(ManaType)).Length);

        return manaTypes[randomType];
    }

    private void SetBoardStatus()
    {
        _boardData = new ManaType[_gameSizeRow, _gameSizeCol];
        for (int i = 0; i < _gameSizeRow; i++)
            for (int j = 0; j < _gameSizeCol; j++)
                _boardData[i, j] = SetRamdomManaStone();

        SearchBoardDirect();

        drawer?.Init(this, _gameSizeRow, _gameSizeCol);
        drawer?.DrawBoard();
    }

    public void Init()
    {
        score = 0;
        SetBoardStatus();
    }

}
