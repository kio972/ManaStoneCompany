using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    GameObject scoreBoard;

    public float targetTime = 60;
    private float curTime = 0;

    private bool gameStart = false;
    public bool _GameStart { get => gameStart; }
    public void EndTimer()
    {
        gameStart = false;
        scoreBoard?.SetActive(true);
    }

    public void StartTimer()
    {
        curTime = 0;
        gameStart = true;
    }

    public void Update()
    {
        if (!gameStart)
            return;

        curTime += Time.deltaTime;
        float val = Mathf.Clamp01(1 - (curTime / targetTime));
        slider.value = val;
        text.text = (targetTime - curTime).ToString("0");
        if (val <= 0)
            EndTimer();
    }

}
