using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ManaType
{
    Red,
    Green,
    Blue,
    Orange,
    
}

public class ManaStone
{
    private ManaType _manaType;
    public ManaType _ManaType { get => _manaType; }

    private int _row = -1;
    private int _col = -1;


    public void Init(ManaType manaType, int row, int col)
    {
        _manaType = manaType;
        _row = row;
        _col = col;
    }

    public ManaStone(ManaType manaType, int row, int col)
    {
        Init(manaType, row, col);
    }
}
