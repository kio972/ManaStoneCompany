using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneArrangeCap : MonoBehaviour
{
    private Image[] stoneImages;
    [SerializeField]
    private Image manaStoneEx;
    [SerializeField]
    private Transform stoneZone;

    public void OffArrangeCap(List<ManaType> manaData, List<Image> boardImages)
    {
        for (int i = 0; i < manaData.Count; i++)
            stoneImages[i].color = Color.clear;

    }

    public void SetArrangeCap(List<ManaType> manaData, List<Image> boardImages)
    {
        foreach (Image img in boardImages)
            img.color = Color.clear;

        ManaType firstOne = manaData[0];
        ManaType lastOne = manaData[manaData.Count - 1];
        manaData.Insert(0, lastOne);
        manaData.Add(firstOne);
        Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.magenta };
        for (int i = 0; i < manaData.Count; i++)
            stoneImages[i].color = colors[(int)manaData[i]];
    }

    public void Init(int boardSize)
    {

    }
}
