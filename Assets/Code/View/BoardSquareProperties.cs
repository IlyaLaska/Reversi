using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquareProperties : MonoBehaviour
{
    public Transform PieceOWhite;
    public Transform PieceOBlack;
    public int xPos = 0;
    public int yPos = 0;
    //public bool hasPiece = false;
    public PlayerEnum pieceColor = PlayerEnum.none;

    public delegate void MouseDownEvent();
    public static event MouseDownEvent BoardSquareClicked;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        //.xPos
        Debug.Log("Called MouseDownEvent: X = " + xPos + ", Y = " + yPos);
        if (BoardSquareClicked != null)
            BoardSquareClicked();

        //Instantiate(PieceO, transform.position, PieceO.rotation);
    }
}