using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public IPlayer playerWhite;
    public IPlayer playerBlack;
    public Game gameO;
    public BoardUpdate boardUpdateO;
    public BoardSquareProperties boardSquarePropertiesO;
 
    //public EventManager eventManager;

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new HumanPlayer();
        playerBlack = new HumanPlayer();
        gameO = new Game(playerWhite, playerBlack);
        boardUpdateO = GameObject.FindObjectOfType<BoardUpdate>();
        boardUpdateO.setBoard(gameO.gameBoard);
        boardSquarePropertiesO = GameObject.FindObjectOfType<BoardSquareProperties>();
        //boardSquarePropertiesO.setGame(gameO);
        //Debug.Log("AAAA");
        //Debug.Log(boardSquarePropertiesO.game);
        gameO.gameBoard.initBoard();
    }
    // Update is called once per frame
    void Update()
    {

    }

}

