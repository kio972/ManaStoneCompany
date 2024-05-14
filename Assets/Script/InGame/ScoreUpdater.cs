using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField]
    private GameBoard board;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Update()
    {
        if (board == null || text == null)
            return;

        text.text = "Score : "+ board._Score.ToString();
    }
}
