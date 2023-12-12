using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    public int _gameSizeRow = 8;
    public int _gameSizeCol = 6;

    private ManaStone[,] _boardData;

    private int _forArrangeNumber = 4;

    [SerializeField]
    private Transform _manaZone;
    [SerializeField]
    private Transform _verticalCap;
    [SerializeField]
    private Transform _horizonalCap;

    private Dictionary<ManaStone, Image> manaStoneDic;

    public void VisualizeStones()
    {
        Image stonePrefab = Resources.Load<Image>("Prefab/ManaStone");
        Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.magenta };
        for (int i = 0; i < _gameSizeRow; i++)
        {
            for (int j = 0; j < _gameSizeCol; j++)
            {
                Image newStone = Instantiate(stonePrefab, _manaZone);
                newStone.color = colors[(int)_boardData[i, j]._ManaType];
                manaStoneDic.Add(_boardData[i, j], newStone);
            }
        }
    }

    public ManaStone SetRamdomManaStone(int row, int col)
    {
        var manaTypes = System.Enum.GetValues(typeof(ManaType)) as ManaType[];
        int randomType = Random.Range(0, System.Enum.GetValues(typeof(ManaType)).Length);


        return new ManaStone(manaTypes[randomType], row, col);
    }

    public void Init()
    {
        _boardData = new ManaStone[_gameSizeRow, _gameSizeCol];

        for(int i = 0; i < _gameSizeRow; i++)
        {
            for (int j = 0; j < _gameSizeCol; j++)
            {
                _boardData[i, j] = SetRamdomManaStone(i, j);
            }
        }

        VisualizeStones();
    }

    private void Awake()
    {
        Init();
    }
}
