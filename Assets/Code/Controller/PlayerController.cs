using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameMaster gameMaster;
    //public Game game;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        BoardSquareProperties.BoardSquareClicked += boardSquareClickHandler;
        Game.nextMove += nextMoveHandler;
    }

    private void OnDisable()
    {
        BoardSquareProperties.BoardSquareClicked -= boardSquareClickHandler;
        Game.nextMove -= nextMoveHandler;
    }

    public void nextMoveHandler()
    {
        if(!gameMaster.gameO.currentPlayer.isHuman)
        {
            CPUPlayer bot = gameMaster.gameO.currentPlayer as CPUPlayer;
            bot.currentTurnCoords = bot.getMove(gameMaster.gameO.validMovesAndDirsForThisTurn);
            gameMaster.gameO.playRound();
        }

    }

    public void boardSquareClickHandler(int xPos, int yPos)
    {
        //SEND TO MODEL
        gameMaster.gameO.currentPlayer.currentTurnCoords = new int[] { xPos, yPos };
        gameMaster.gameO.playRound();
    }
}
