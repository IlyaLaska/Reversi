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
        squares = new GameObject[8,8];
        boardOProps = boardO.GetComponent<BoardSquareProperties>();
        instantiateBoard();

    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
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
                if(board.board[x,y].belongsToPlayer != squares[x,y].GetComponent<BoardSquareProperties>().pieceColor)
                {
                    //Update Piece
                    Destroy (squares[x, y].GetComponent<BoardSquareProperties>().PieceO);

                    //change to correct value
                    squares[x, y].GetComponent<BoardSquareProperties>().pieceColor = board.board[x, y].belongsToPlayer;
                }
            }
        }
    }

    void instantiateBoard()
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