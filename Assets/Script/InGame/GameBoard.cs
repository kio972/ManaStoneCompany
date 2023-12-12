using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameBoard : MonoBehaviour
{
    public int _gameSizeRow = 8;
    public int _gameSizeCol = 6;

    private ManaType[,] _boardData;

    private int _forArrangeNumber = 4;

    [SerializeField]
    private Transform _manaZone;
    [SerializeField]
    private Transform _verticalCap;
    [SerializeField]
    private Transform horizontalCap;

    [SerializeField]
    private Image _manaStoneEx;
    private Image[,] _manaStoneImages;

    public Image SetStoneImage(int row, int col)
    {
        Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.magenta };
        Image newStone = Instantiate(_manaStoneEx, _manaZone);
        newStone.color = colors[(int)_boardData[row, col]];
        return newStone;
    }

    public ManaType SetRamdomManaStone(int row, int col)
    {
        var manaTypes = System.Enum.GetValues(typeof(ManaType)) as ManaType[];
        int randomType = Random.Range(0, System.Enum.GetValues(typeof(ManaType)).Length);

        return manaTypes[randomType];
    }

    private void SetBoardStatus()
    {
        _boardData = new ManaType[_gameSizeRow, _gameSizeCol];
        _manaStoneImages = new Image[_gameSizeRow, _gameSizeCol];

        for (int i = 0; i < _gameSizeRow; i++)
        {
            for (int j = 0; j < _gameSizeCol; j++)
            {
                _boardData[i, j] = SetRamdomManaStone(i, j);
                _manaStoneImages[i, j] = SetStoneImage(i, j);
            }
        }

        _manaStoneEx.gameObject.SetActive(false);
    }

    public void Init()
    {
        SetBoardStatus();

    }

    private void Awake()
    {
        Init();
    }
}
