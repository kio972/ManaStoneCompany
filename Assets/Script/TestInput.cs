using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestInput : MonoBehaviour
{
    public int targetRow;
    public int targetCol;

    public GameObject canvas;
    public GraphicRaycaster raycaster;
    public PointerEventData pointer;
    public EventSystem eventsystem;

    public GameBoard board;

    private bool inputStart = false;
    private Vector2 startPos;
    private ManaStone curStone;

    public float spacing = 30;

    [SerializeField]
    private Timer timer;

    public void SetRowCol(int row, int col)
    {
        targetRow = row;
        targetCol = col;
    }

    private void Start()
    {
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        eventsystem = GetComponent<EventSystem>();
    }

    private void ToucheEndCheck()
    {
        if (!inputStart)
            return;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            pointer = new PointerEventData(eventsystem);
            pointer.position = Input.mousePosition;
            List<RaycastResult> result = new List<RaycastResult>();
            raycaster.Raycast(pointer, result);

            GameObject hitObj = result[0].gameObject;
            if (hitObj.CompareTag("ManaStone"))
            {
                ManaStone stone = hitObj.GetComponent<ManaStone>();
                targetRow = stone.row;
                targetCol = stone.col;
                inputStart = true;
                stone._StoneImg.raycastTarget = false;
            }
        }
    }

    private void DragCheck()
    {
        if (!inputStart)
            return;

        Vector2 curPos = Input.mousePosition;
        float x = curPos.x - startPos.x;
        float y = curPos.y - startPos.y;
        bool isMove = true;
        if (x >= spacing)
            board?.RotateBoard(targetRow, targetCol, 3);
        else if (x <= -spacing)
            board?.RotateBoard(targetRow, targetCol, 2);
        else if (y >= spacing)
            board?.RotateBoard(targetRow, targetCol, 1);
        else if (y <= -spacing)
            board?.RotateBoard(targetRow, targetCol, 0);
        else
            isMove = false;

        if (isMove)
            inputStart = false;
    }

    private void TouchStartCheck()
    {
        if (inputStart)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pointer = new PointerEventData(eventsystem);
            pointer.position = Input.mousePosition;
            List<RaycastResult> result = new List<RaycastResult>();
            raycaster.Raycast(pointer, result);

            GameObject hitObj = result[0].gameObject;
            if(hitObj.CompareTag("ManaStone"))
            {
                ManaStone stone = hitObj.GetComponent<ManaStone>();
                targetRow = stone.row;
                targetCol = stone.col;
                inputStart = true;
                //stone._StoneImg.raycastTarget = false;
                startPos = Input.mousePosition;
                curStone = stone;
            }
        }
    }

    public void Update()
    {
        if (board._InputLock || !timer._GameStart)
            return;

        TouchStartCheck();
        DragCheck();
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //    board?.RotateBoard(targetRow, targetCol, 1);
        //else if(Input.GetKeyDown(KeyCode.DownArrow))
        //    board?.RotateBoard(targetRow, targetCol, 0);
        //else if(Input.GetKeyDown(KeyCode.LeftArrow))
        //    board?.RotateBoard(targetRow, targetCol, 2);
        //else if(Input.GetKeyDown(KeyCode.RightArrow))
        //    board?.RotateBoard(targetRow, targetCol, 3);
    }

}
