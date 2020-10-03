using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUpdate : MonoBehaviour
{
    public GameObject boardO;
    public BoardSquareProperties boardOProps;
    public GameObject[,] squares;
    public Board board;
    void Start()
    {
        

    }
    private void Awake()
    {
        squares = new GameObject[8, 8];
        boardOProps = boardO.GetComponent<BoardSquareProperties>();
        instantiateBoard();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()    {
        Board.boardUpdateEvent += boardUpdateHandler;
    }
    private void OnDisable()
    {
        Board.boardUpdateEvent -= boardUpdateHandler;
    }

    public void boardUpdateHandler()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                //Debug.Log("SQUARES");
                //Debug.Log(squares[0,0]);
                BoardSquareProperties curProps = squares[y, x].GetComponent<BoardSquareProperties>();
                //Debug.Log("CUR Props");
                //Debug.Log(curProps);
                if (board.board[y,x].belongsToPlayer != curProps.pieceColor)
                {
                    Debug.Log("------------------X " + x + " and Y "+ y);
                    Debug.Log("--PieceO: " + (curProps.pieceO == null));
                    Debug.Log("--PieceColor: " + curProps.pieceColor);
                    if (curProps.pieceO != null)
                    {
                        Debug.Log("+++++++PieceO Name: " + curProps.pieceO.name);
                    }
                    //Update Piece - Destroy old
                    if (curProps.pieceColor == PlayerEnum.white && curProps.pieceO)
                    {
                        curProps.pieceO.transform.position = new Vector2(-100, -100);
                        Debug.Log("------------------DELETE Black");
                        //Destroy(curProps.pieceO);
                    }
                    else if (curProps.pieceColor == PlayerEnum.black && curProps.pieceO)
                    {
                        curProps.pieceO.transform.position = new Vector2(-100, -100);
                        Debug.Log("------------------DELETE Black");
                        //Destroy(curProps.pieceO);
                    }

                    //Update Piece - Create New
                    if (board.board[y,x].belongsToPlayer == PlayerEnum.white)
                    {
                        curProps.pieceO = (GameObject) Instantiate(curProps.PieceOWhite.gameObject, curProps.transform.position, curProps.PieceOWhite.rotation);
                        Debug.Log("------------------Create New WHITE \n" + curProps.pieceO.name);
                    }
                    else if (board.board[y,x].belongsToPlayer == PlayerEnum.black)
                    {
                        curProps.pieceO = (GameObject) Instantiate(curProps.PieceOBlack.gameObject, curProps.transform.position, curProps.PieceOBlack.rotation);
                        Debug.Log(" ------------------Create New BLACK \n" + curProps.pieceO.name);
                    }

                    //change to correct value
                    curProps.pieceColor = board.board[y,x].belongsToPlayer;
                }
            }
        }
    }

    public void instantiateBoard()
    {
        int xPosition = 0, yPosition = 0;
        //Instantiate(boardO, new Vector2(-3.5f, 3.5f), boardO.rotation);
        for (float y = 3.5f; y >= -3.5f; y--)
        {
            for (float x = -3.5f; x <= 3.5f; x++)
            {
                boardOProps.xPos = xPosition;
                boardOProps.yPos = yPosition;
                squares[yPosition, xPosition] = (GameObject)Instantiate(boardO, new Vector2(x, y), Quaternion.identity);
                xPosition++;
            }
            yPosition++;
            xPosition = 0;
        }
        Debug.Log(boardO.tag);
    }

    public void setBoard(Board board)
    {
        this.board = board;
    }
}