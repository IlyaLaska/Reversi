using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EventManager : MonoBehaviour
{
    public delegate void MouseDownEvent();
    public static event MouseDownEvent BoardSquareClicked;
    public BoardSquareProperties boardOProps;


    // Start is called before the first frame update
    void Start()
    {
        boardOProps = gameObject.GetComponent<BoardSquareProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //.xPos
        Debug.Log("Called MouseDownEvent: X = " + boardOProps.xPos + ", Y = " + boardOProps.yPos);
        if (BoardSquareClicked != null)
            BoardSquareClicked();
    }
}
