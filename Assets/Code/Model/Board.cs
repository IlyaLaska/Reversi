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

    public int[][] getValidMovesList(IPlayer currPlayer)
    {
        //Debug.Log("WWWWW");
        //Debug.Log(currPlayer.color);
        PlayerEnum currentPlayer = currPlayer.color;
        
        HashSet<int[]> validMovesList = new HashSet<int[]>();
        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardLength; j++)
            {
                //Debug.Log(board[i, j].belongsToPlayer);
                if (board[i,j].belongsToPlayer == currentPlayer)//have to check possible moves for that piece
                {
                    //HashSet<int[]> a = getValidMovesForAPiece(new int[] { i, j }, currentPlayer);
                    validMovesList = new HashSet<int[]> (validMovesList.Concat(getValidMovesForAPiece(new int[] { i, j }, currentPlayer)));
                }
            }
        }
        if(getValidMovesListEvent != null)
            getValidMovesListEvent();

        Debug.Log("WWWWW");
        Debug.Log(validMovesList.Count);
        return validMovesList.ToArray();
    }
    private HashSet<int[]> getValidMovesForAPieceO(int[] boardSquareCoordinates, PlayerEnum currentPlayer)
    {
        //Debug.Log("Input: ");
        //Debug.Log(currentPlayer);
        //Debug.Log("X: " + boardSquareCoordinates[0] + "Y: " + boardSquareCoordinates[1]);

        HashSet<int[]> validMovesList = new HashSet<int[]>();
        int[] tempCoord;
        //check NW
        //Debug.Log("Checking NW");
        if (boardSquareCoordinates[0] > 0 && boardSquareCoordinates[1] > 0)
        {
            tempCoord = (int[]) boardSquareCoordinates.Clone();
            //boardSquareCoordinates.CopyTo(tempCoord, 0);
            do
            {
                tempCoord[0] -= 1;
                tempCoord[1] -= 1;
            } while ((tempCoord[0] > 0 && tempCoord[1] > boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if ((tempCoord[0] != boardSquareCoordinates[0]-1 && tempCoord[1] != boardSquareCoordinates[1] - 1) && board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                //Debug.Log("Adding: X: " + tempCoord[0] + "Y: " + tempCoord[1]);
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.NW });
            }
        }
        //check N
        //Debug.Log("Checking N");
        if (boardSquareCoordinates[1] > 0)
        {
            //tempCoord = boardSquareCoordinates;
            tempCoord = (int[])boardSquareCoordinates.Clone();

            //Debug.Log("temp X: " + tempCoord[0] + "Y: " + tempCoord[1]);
            do
            {
                //tempCoord[0] -= 1;
                tempCoord[1] -= 1;
            } while ((tempCoord[1] > 0) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            //Debug.Log("Ended in: X:" + tempCoord[0] + "Y: " + tempCoord[1]);
            if ((tempCoord[1] != boardSquareCoordinates[1] - 1) && board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                //Debug.Log("Adding: X: " + tempCoord[0] + "Y: " + tempCoord[1]);
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.N});
            }
        }
        //check NE
        //Debug.Log("Checking NE");
        if (boardSquareCoordinates[0] < boardLength && boardSquareCoordinates[1] > 0)
        {
            //tempCoord = boardSquareCoordinates;
            tempCoord = (int[])boardSquareCoordinates.Clone();

            do
            {
                tempCoord[0] += 1;
                tempCoord[1] -= 1;
            } while ((tempCoord[0] < boardLength && tempCoord[1] > 0) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if ((tempCoord[0] != boardSquareCoordinates[0] + 1 && tempCoord[1] != boardSquareCoordinates[1] - 1) && board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                //Debug.Log("Adding: X: " + tempCoord[0] + "Y: " + tempCoord[1]);
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.NE});
            }
        }
        //check E
        if (boardSquareCoordinates[0] < boardLength)
        {
            //tempCoord = boardSquareCoordinates;
            tempCoord = (int[])boardSquareCoordinates.Clone();

            do
            {
                tempCoord[0] += 1;
                //tempCoord[1] -= 1;
            } while ((tempCoord[0] < boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if ((tempCoord[0] != boardSquareCoordinates[0] + 1) && board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.E});
            }
        }
        //check SE
        if (boardSquareCoordinates[0] < boardLength && boardSquareCoordinates[1] < boardLength)
        {
            //tempCoord = boardSquareCoordinates;
            tempCoord = (int[])boardSquareCoordinates.Clone();

            do
            {
                tempCoord[0] += 1;
                tempCoord[1] += 1;
            } while ((tempCoord[0] < boardLength && tempCoord[1] < boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if ((tempCoord[0] != boardSquareCoordinates[0] + 1 && tempCoord[1] != boardSquareCoordinates[1] + 1) && board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.SE });
            }
        }
        //check S
        if (boardSquareCoordinates[1] < boardLength)
        {
            //tempCoord = boardSquareCoordinates;
            tempCoord = (int[])boardSquareCoordinates.Clone();

            do
            {
                //tempCoord[0] -= 1;
                tempCoord[1] += 1;
            } while ((tempCoord[1] < boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if ((tempCoord[1] != boardSquareCoordinates[1] + 1) && board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.S});
            }
        }
        //check SW
        if (boardSquareCoordinates[0] > 0 && boardSquareCoordinates[1] < boardLength)
        {
            //tempCoord = boardSquareCoordinates;
            tempCoord = (int[])boardSquareCoordinates.Clone();

            do
            {
                tempCoord[0] -= 1;
                tempCoord[1] += 1;
            } while ((tempCoord[0] > 0 && tempCoord[1] < boardLength) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if ((tempCoord[0] != boardSquareCoordinates[0] - 1 && tempCoord[1] != boardSquareCoordinates[1] + 1) && board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.SW });
            }
        }
        //check W
        if (boardSquareCoordinates[0] > 0)
        {
            //tempCoord = boardSquareCoordinates;
            tempCoord = (int[])boardSquareCoordinates.Clone();

            do
            {
                tempCoord[0] -= 1;
                //tempCoord[1] -= 1;
            } while ((tempCoord[0] > 0) && board[tempCoord[0], tempCoord[1]].belongsToPlayer != PlayerEnum.none &&
                     board[tempCoord[0], tempCoord[1]].belongsToPlayer != currentPlayer);//next piece belongs to enemy
            if ((tempCoord[0] != boardSquareCoordinates[0] - 1) && board[tempCoord[0], tempCoord[1]].belongsToPlayer == PlayerEnum.none)//encountered empty place
            {
                validMovesList.Add(new int[] { tempCoord[0], tempCoord[1], (int)Direction.W});
            }
        }
        //Debug.Log("Valids:");
        //Debug.Log(validMovesList.Count);
        return validMovesList;
    }

    private HashSet<int[]> getValidMovesForAPiece(int[] boardSquareCoordinates, PlayerEnum currentPlayer)
    {
        //Debug.Log("Input: ");
        //Debug.Log(currentPlayer);
        //Debug.Log("X: " + boardSquareCoordinates[0] + "Y: " + boardSquareCoordinates[1]);

        HashSet<int[]> validMovesList = new HashSet<int[]>();
        int[] tempCoord;

        //check NW
        Debug.Log("Checking NW");

        // NW [-1,-1]
        int[] array;

        array = succesfulMoveInDiraction(boardSquareCoordinates, -1, -1, currentPlayer);
        if (array[0] == 1)//encountered empty place
            {
                //Debug.Log("Adding: X: " + tempCoord[0] + "Y: " + tempCoord[1]);
                validMovesList.Add(new int[] { array[1], array[2], -1, -1 });
            }

        //check N [-1, 0]
        Debug.Log("Checking N");
        array = succesfulMoveInDiraction(boardSquareCoordinates, -1, 0, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + tempCoord[0] + "Y: " + tempCoord[1]);
            validMovesList.Add(new int[] { array[1], array[2], -1, 0 });
        }
        //check NE [+1,-1]
        Debug.Log("Checking NE");

        array = succesfulMoveInDiraction(boardSquareCoordinates, 1, -1, currentPlayer);
        if (array[0] == 1)//encountered empty place
            {
                //Debug.Log("Adding: X: " + tempCoord[0] + "Y: " + tempCoord[1]);
                validMovesList.Add(new int[] { array[1], array[2], 1, -1 });
            }

        //check E[0,1]
        Debug.Log("Checking E");
        array = succesfulMoveInDiraction(boardSquareCoordinates, 0, 1, currentPlayer);
        if (array[0] == 1)//encountered empty place
            {
                validMovesList.Add(new int[] { array[1], array[2], 0, 1 });
            }
        //check SE[1,1]
        Debug.Log("Checking SE");
        array = succesfulMoveInDiraction(boardSquareCoordinates, 1, 1, currentPlayer);
        if (array[0] == 1)//encountered empty place
            {
                validMovesList.Add(new int[] { array[1], array[2], 1, 1 });
            }

        //check S[1,0]
        Debug.Log("Checking S");
        array = succesfulMoveInDiraction(boardSquareCoordinates, 1, 0, currentPlayer);
        if (array[0] == 1)//encountered empty place
            {
                validMovesList.Add(new int[] { array[1], array[2], 1, 0 });
            }

        //check SW[-1,1]
        Debug.Log("Checking SW");
        array = succesfulMoveInDiraction(boardSquareCoordinates, -1, 1, currentPlayer);
        if (array[0] == 1)//encountered empty place
            {
                validMovesList.Add(new int[] { array[1], array[2], -1, 1 });
            }

        //check W[0,-1]
        Debug.Log("Checking W");
        array = succesfulMoveInDiraction(boardSquareCoordinates, 0, -1, currentPlayer);
            if (array[0] == 1)//encountered empty place
            {
                validMovesList.Add(new int[] { array[1], array[2], 0, -1 });
            }
        
        Debug.Log("Valids:");
        Debug.Log(validMovesList.Count);
        return validMovesList;
    }
    public int[] succesfulMoveInDiraction(int[] boardSquareCoordinates, int x, int y, PlayerEnum currentPlayer)
    {
        int hasEnemies = 0;
        bool reachedNullOrAlly = false;
        int[] tempCoords = (int[])boardSquareCoordinates.Clone();

        while (!reachedNullOrAlly)
        {
            tempCoords[0] += x;
            tempCoords[1] += y;

            if (inRange(tempCoords[0]) && inRange(tempCoords[1]))
            {
                Debug.Log("X,y" + tempCoords[0] + " " + tempCoords[1]);

                if (board[tempCoords[0], tempCoords[1]].belongsToPlayer == PlayerEnum.none ||
                     board[tempCoords[0], tempCoords[1]].belongsToPlayer == currentPlayer)
                {
                    reachedNullOrAlly = true;
                } else hasEnemies = 1;
            }
            else reachedNullOrAlly = true;

        }
        

        return new int[] { hasEnemies, tempCoords[0], tempCoords[1] };
    }

    public bool inRange(int value)
    {
        return value < boardLength && value >= 0;
    }


    public int updateBeatPiecesO(List<int[]> validMoves, IPlayer currentPlayer)
    {
        PlayerEnum currentTurn = currentPlayer.color;
        this.board[validMoves[0][1], validMoves[0][0]].belongsToPlayer = currentTurn;
        int changedPieces = 0;
        //int[] tempCoords = { 0, 0 };
        foreach (var move in validMoves)
        {
            Debug.Log("MADE MOVE: " + move[0] + " , " + move[1] + " , " + (Direction) move[2] + " , ");
            if(move[2] == (int) Direction.NW)//move in opposite direction and count flipped Pieces on the way
            {
                if(currentTurn == PlayerEnum.black)
                {
                    while (board[move[1] + 1, move[0] + 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[1] + 1, move[0] + 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                } else if(currentTurn == PlayerEnum.white)
                {
                    while (board[move[1] + 1, move[0] + 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[1] + 1, move[0] + 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]++;
                    }
                }
            } else if (move[2] == (int)Direction.N)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[1] + 1, move[0]].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[1] + 1, move[0]].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        //move[0]++;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[1] + 1, move[0]].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[1] + 1, move[0]].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        //move[0]++;
                        move[1]++;
                    }
                }
            }
            else if(move[2] == (int)Direction.NE)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[1] + 1, move[0]-1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[1] + 1, move[0]-1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]--;
                        move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[1] + 1, move[0]-1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[1] + 1, move[0]-1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]--;
                        move[1]++;
                    }
                }
            }
            else if(move[2] == (int)Direction.E)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[1], move[0]-1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[1], move[0]-1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]--;
                        //move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[1], move[0]-1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[1], move[0]-1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]--;
                        //move[1]++;
                    }
                }
            }
            else if(move[2] == (int)Direction.SE)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[1] - 1, move[0]-1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[1] - 1, move[0]-1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]--;
                        move[1]--;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[1] - 1, move[0]-1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[1] - 1, move[0]-1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]--;
                        move[1]--;
                    }
                }
            }
            else if(move[2] == (int)Direction.S)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[1] - 1, move[0]].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[1] - 1, move[0]].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        //move[0]++;
                        move[1]--;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[1] - 1, move[0]].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[1] - 1, move[0]].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        //move[0]++;
                        move[1]--;
                    }
                }
            }
            else if(move[2] == (int)Direction.SW)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[1] - 1, move[0] + 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        board[move[1] - 1, move[0] + 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        move[1]--;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    while (board[move[1] - 1, move[0] + 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[1] - 1, move[0] + 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        move[1]--;
                    }
                }
            }
            else if(move[2] == (int)Direction.W)
            {
                if (currentTurn == PlayerEnum.black)
                {
                    while (board[move[1], move[0] + 1].belongsToPlayer == PlayerEnum.white)//while next is enemy (must be since we checked)
                    {
                        //Debug.Log("Changing: X: " + (move[0] + 1) + " Y: " + move[1]);
                        board[move[1], move[0] + 1].belongsToPlayer = PlayerEnum.black;
                        changedPieces++;
                        move[0]++;
                        //move[1]++;
                    }
                }
                else if (currentTurn == PlayerEnum.white)
                {
                    //Debug.Log("Board " + (move[0] + 1) + " , " + move[1] + ": " + board[move[1], move[0] + 1].belongsToPlayer);
                    while (board[move[1], move[0] + 1].belongsToPlayer == PlayerEnum.black)//while next is enemy (must be since we checked)
                    {
                        board[move[1], move[0] + 1].belongsToPlayer = PlayerEnum.white;
                        changedPieces++;
                        move[0]++;
                        //move[1]++;
                    }
                }
            }
        }
        if(boardUpdateEvent != null)
            boardUpdateEvent();
        Debug.Log("ChangedPieces: " + changedPieces);
        return changedPieces;
    }

    public int updateBeatPieces(List<int[]> validMoves, IPlayer currentPlayer)
    {
        PlayerEnum currentTurn = currentPlayer.color;

        //this.board[validMoves[0][1], validMoves[0][0]].belongsToPlayer = currentTurn;

        int changedPieces = 0;
        //int[] tempCoords = { 0, 0 };
        foreach (var move in validMoves)
        {
            Debug.Log("MADE MOVE: " + move[0] + " , " + move[1] + " , " + (Direction)move[2] + " , ");

            if (currentTurn == PlayerEnum.black)
            {
                changePieces(move[0], move[1], move[2], move[3], currentTurn, PlayerEnum.white, ref changedPieces);
            }
            else changePieces(move[0], move[1], move[2], move[3], currentTurn, PlayerEnum.black, ref changedPieces);
        }

        if (boardUpdateEvent != null)
            boardUpdateEvent();
        Debug.Log("ChangedPieces: " + changedPieces);
        return changedPieces;
    }

    public void changePieces(int coordX, int coordY, int x, int y, PlayerEnum colorCurrent, PlayerEnum colorEnemy, ref int changed)
    {
        do
        {
            board[coordY - x, coordX - y].belongsToPlayer = colorCurrent;
            changed++;
            coordX -= x;
            coordY -= y;
        }
        while (board[coordY - x, coordX - y].belongsToPlayer == colorEnemy); //while next is enemy (must be since we checked)

    }
}




