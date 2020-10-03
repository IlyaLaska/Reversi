using System;
using System.Collections.Generic;
using System.Numerics;

public class Game
{
	public Board gameBoard = new Board();
	public PlayerEnum currentTurn = PlayerEnum.black;
	public IPlayer black;
	public IPlayer white;
	public int[][] validMovesForThisTurn;
	public int moveCounter = 0;
    public bool gameIsOver = false;

    public delegate void GameEnd();
    public static event GameEnd gameEnded;
	//public int blackScore = 2;
	//public int whiteScore = 2;
	public Game(IPlayer first, IPlayer second)
	{
		black = first;
		white = second;
	}

	public void playRound()
    {
		//get valid moves list
		this.validMovesForThisTurn = gameBoard.getValidMovesList(currentTurn);
        if(this.validMovesForThisTurn.Length < 1)
        {
            if (isMaxScore())
                gameEnded();
                // GAME IS OVER EVENT

                // game is not over, no available turns
            playRound();
        }

        IPlayer currentPlayer =  null;
		if (currentTurn == PlayerEnum.black)
        {
            currentPlayer = black;
		}
		else currentPlayer = white;

        //get coordinates of next move

        int[] selectedSquare = currentPlayer.getMove(this.validMovesForThisTurn);

        
        List<int[]> takenMoves = new List<int[]>();
        //list of taken moves (coordinates same, direction different)
        for (int i = 0; i < validMovesForThisTurn.Length; i++)
        {
            if (validMovesForThisTurn[i][0] == selectedSquare[0] && validMovesForThisTurn[i][0] == selectedSquare[0])//one of valids
            {
                takenMoves.Add(validMovesForThisTurn[i]);
            }
        }
        if (takenMoves.Count == 0)//no allowed moves
        {
            //	//INVALID MOVE - restart function?

        }
        int beatPiecesCount = gameBoard.updateBeatPieces(takenMoves, currentTurn);


        currentPlayer.score += beatPiecesCount + 1;

        if (currentTurn == PlayerEnum.black)
        {
            white.score -= beatPiecesCount;
        }
        else black.score -= beatPiecesCount;


        playRound();
    }

    private bool isMaxScore()
    {
        return black.score + white.score == 64;
    }

}
