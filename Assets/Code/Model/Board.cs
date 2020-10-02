using System;

public class Board
{
	public int boardLength = 8;
    public BoardSquare[,] board;

	public Board()
	{
		board = new BoardSquare[boardLength, boardLength];
	}

	public void initBoard()
    {
        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardLength; j++)
            {
                board[i,j] = new BoardSquare();
            }
        }

        //Add starting Pieces
        board[3, 3].belongsToPlayer = PlayerEnum.white;
        board[4, 4].belongsToPlayer = PlayerEnum.white;

        board[3, 4].belongsToPlayer = PlayerEnum.black;
        board[4, 3].belongsToPlayer = PlayerEnum.black;
    }
    public int[][] getValidMovesList(PlayerEnum currentPlayer)
    {

        return 
    }
}
