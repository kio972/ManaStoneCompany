using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface BoardObject
{
    public int row { get; set; }
    public int col { get; set; }
}

public enum ManaType
{
    None,
    Red,
    Green,
    Blue,
    Orange,
    Yellow,
}

public class ManaStone : MonoBehaviour, BoardObject
{
    private Image stoneImg;
    public Image _StoneImg { get => stoneImg; }
    private int rowIndex;
    private int colIndex;

    public int row { get => rowIndex; set => rowIndex = value; }
    public int col { get => colIndex; set => colIndex = value; }

    private BoardDrawer drawer;

    private TestInput input;
    private TestInput _Input
    {
        get
        {
            if (input == null)
                input = FindObjectOfType<TestInput>();
            return input;
        }
    }

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Sprite[] popSprites;

    readonly Color[] colors = new Color[] { Color.white, Color.red, Color.green, Color.blue, Color.magenta, Color.yellow };

    ManaType curType;

    public void SetColor(ManaType type)
    {
        //if (type == ManaType.None)
        //    stoneImg.sprite = popSprites[(int)curType];
        //else

        stoneImg.sprite = sprites[(int)type];
        curType = type;
    }

    public void OnClick()
    {
        _Input?.SetRowCol(rowIndex, colIndex);
    }

    public void Init(int row, int col)
    {
        rowIndex = row;
        colIndex = col;
        stoneImg = GetComponentInChildren<Image>();
    }

    public void SetManaStone(ManaType stoneType)
    {
        
    }
}
