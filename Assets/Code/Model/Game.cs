using System;
using System.Numerics;

public class Game
{
	public Board gameBoard = new Board();
	public PlayerEnum currentTurn = PlayerEnum.black;
	public IPlayer black;
	public IPlayer white;
	public Game(IPlayer first, IPlayer second)
	{
		black = first;
		white = second;
	}
	public void playRound()
    {
		//get valid moves list
		int[][] validMovesList = gameBoard.getValidMovesList(currentTurn);

		//get coordinates of next move
		int[] selectedSquare = { 0, 0 };
		if (currentTurn == PlayerEnum.black)
        {
			selectedSquare = black.getMove();
		}
		else if(currentTurn == PlayerEnum.white)
        {
			selectedSquare = white.getMove();
		}

		
		if(Array.Exists<int[]>(validMovesList, el => el == selectedSquare))
        {
			gameBoard.updateBeatPieces();
			
        } else
        {
			//INVALID MOVE - restart function?
        }
	}
}
