using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Transform boardO;
    public IPlayer playerWhite;
    public IPlayer playerBlack;
    public Game gameO;
    public BoardSquareProperties boardOProps;
    public EventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        boardOProps = boardO.GetComponent<BoardSquareProperties>();
        instantiateBoard();
        playerWhite = new HumanPlayer();
        playerBlack = new HumanPlayer();
        gameO = new Game(playerWhite, playerBlack);
    }
    // Update is called once per frame
    void Update()
    {
        
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
                Instantiate(boardO, new Vector2(x, y), boardO.rotation);
                xPosition++;
            }
            yPosition++;
            xPosition = 0;
        }
        Debug.Log(boardO.tag);
    }
}
