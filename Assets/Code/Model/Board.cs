using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

public class Board
{
	public int boardLength = 8;
    public BoardSquare[,] board;

    public delegate void GetValidMovesListEvent();
    public static event GetValidMovesListEvent getValidMovesListEvent;
    public delegate void BoardUpdateEvent();
    public static event BoardUpdateEvent boardUpdateEvent;

    public Board()
	{
		this.board = new BoardSquare[boardLength, boardLength];
	}

	public void initBoard()
    {
        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardLength; j++)
            {
                this.board[i, j] = new BoardSquare();
            }
        }

        //Add starting Pieces
        this.board[3, 3].belongsToPlayer = PlayerEnum.white;
        this.board[4, 4].belongsToPlayer = PlayerEnum.white;

        this.board[3, 4].belongsToPlayer = PlayerEnum.black;
        this.board[4, 3].belongsToPlayer = PlayerEnum.black;

        boardUpdateEvent();
        Debug.Log("Called boardUpdateEvent");
    }
    public int[][] getValidMovesList(PlayerEnum currentPlayer)
    {
        HashSet<int[]> validMovesList = new HashSet<int[]>();
        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardLength; j++)
            {
                if (board[i,j].belongsToPlayer == currentPlayer)//have to check possible moves for that piece
                {
                    validMovesList.Concat(getValidMovesForAPiece(new int[] {i, j}, currentPlayer));
                }
            }
        }
        getValidMovesListEvent();
        return validMovesList.ToArray();
    }
    private HashSet<int[]> getValidMovesForAPiece(int[] boardSquareCoordinates, PlayerEnum currentPlayer)
    {
        HashSet<int[]> validMovesList = new HashSet<int[]>();
        int[] tempCoord;
        //check NW
        if(boardSquareCoordinates[0]>0 && boardSquareCoordinates[1] > 0)
        {
            tempCoord = boardSquareCoordinates;
            do
            {
                tempCoord[0] -= 1;
                tempCoord[1] -= 1;
            } while ((tempCoord[0] > 0 && tempCoord[1] > boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if (board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.NW });
            }
        }
        //check N
        if (boardSquareCoordinates[1] > 0)
        {
            tempCoord = boardSquareCoordinates;
            do
            {
                //tempCoord[0] -= 1;
                tempCoord[1] -= 1;
            } while ((tempCoord[1] > 0) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if (board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.N});
            }
        }
        //check NE
        if (boardSquareCoordinates[0] < boardLength && boardSquareCoordinates[1] > 0)
        {
            tempCoord = boardSquareCoordinates;
            do
            {
                tempCoord[0] += 1;
                tempCoord[1] -= 1;
            } while ((tempCoord[0] < boardLength && tempCoord[1] > 0) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if (board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.NE});
            }
        }
        //check E
        if (boardSquareCoordinates[0] < boardLength)
        {
            tempCoord = boardSquareCoordinates;
            do
            {
                tempCoord[0] += 1;
                //tempCoord[1] -= 1;
            } while ((tempCoord[0] < boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if (board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.E});
            }
        }
        //check SE
        if (boardSquareCoordinates[0] < boardLength && boardSquareCoordinates[1] < boardLength)
        {
            tempCoord = boardSquareCoordinates;
            do
            {
                tempCoord[0] += 1;
                tempCoord[1] += 1;
            } while ((tempCoord[0] < boardLength && tempCoord[1] < boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if (board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.SE });
            }
        }
        //check S
        if (boardSquareCoordinates[1] < boardLength)
        {
            tempCoord = boardSquareCoordinates;
            do
            {
                //tempCoord[0] -= 1;
                tempCoord[1] += 1;
            } while ((tempCoord[1] < boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if (board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.S});
            }
        }
        //check SW
        if (boardSquareCoordinates[0] > 0 && boardSquareCoordinates[1] < boardLength)
        {
            tempCoord = boardSquareCoordinates;
            do
            {
                tempCoord[0] -= 1;
                tempCoord[1] += 1;
            } while ((tempCoord[0] > 0 && tempCoord[1] < boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if (board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.SW });
            }
        }
        //check W
        if (boardSquareCoordinates[0] > 0)
        {
            tempCoord = boardSquareCoordinates;
            do
            {
                tempCoord[0] -= 1;
                //tempCoord[1] -= 1;
            } while ((tempCoord[0] > 0) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if (board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.W});
            }
        }
            
        return validMovesList;
    }

    public int updateBeatPieces(List<int[]> validMoves, PlayerEnum currentTurn)
    {
        int changedPieces = 0;
        //int[] tempCoords = { 0, 0 };
        foreach (var move in validMoves)
        {
            if(move[3] == (int) Direction.NW)//move in opposite direction and count flipped Pieces on the way
            {
                if(currentTurn == PlayerEnum.black)
                {
                    while (board[move[0] + 1, move[1] + 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1] + 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                } else if(currentTurn == PlayerEnum.white)
                {
                    while (board[move[0] + 1, move[1] + 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1] + 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            } else if (move[3] == (int)Direction.N)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[0], move[1] + 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[0], move[1] + 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[0], move[1] + 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[0], move[1] + 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            }
            else if(move[3] == (int)Direction.NE)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[0] + 1, move[1] - 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1] - 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[0] + 1, move[1] - 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1] - 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            }
            else if(move[3] == (int)Direction.E)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[0] - 1, move[1]].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[0] - 1, move[1]].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[0] - 1, move[1]].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[0] - 1, move[1]].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            }
            else if(move[3] == (int)Direction.SE)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[0] - 1, move[1] - 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[0] - 1, move[1] - 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[0] - 1, move[1] - 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1] + 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            }
            else if(move[3] == (int)Direction.S)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[0], move[1] - 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[0], move[1] - 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[0], move[1] - 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[0], move[1] - 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            }
            else if(move[3] == (int)Direction.SW)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[0] + 1, move[1] - 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1] - 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[0] + 1, move[1] - 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1] - 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            }
            else if(move[3] == (int)Direction.W)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[0] + 1, move[1]].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1]].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[0] + 1, move[1]].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[0] + 1, move[1]].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            }
        }
        boardUpdateEvent();
        return changedPieces;
    }
}
