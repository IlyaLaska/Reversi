using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

public class Board
{
    public int boardLength = 8;
    public BoardSquare[,] board;


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
        //Debug.Log("Called boardUpdateEvent");
    }

    public int[][] getValidMovesList(IPlayer currPlayer)
    {
        PlayerEnum currentPlayer = currPlayer.color;

        HashSet<int[]> validMovesList = new HashSet<int[]>();
        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardLength; j++)
            {
                //Debug.Log(board[i, j].belongsToPlayer);
                if (board[i, j].belongsToPlayer == currentPlayer)//have to check possible moves for that piece
                {
                    //HashSet<int[]> a = getValidMovesForAPiece(new int[] { i, j }, currentPlayer);
                    validMovesList = new HashSet<int[]>(validMovesList.Concat(getValidMovesForAPiece(new int[] { j, i }, currentPlayer)));
                }
            }
        }


        Debug.Log("Valid Moves: " + validMovesList.Count);
        //Debug.Log(validMovesList.Count);
        return validMovesList.ToArray();
    }


    private HashSet<int[]> getValidMovesForAPiece(int[] boardSquareCoordinates, PlayerEnum currentPlayer)
    {
        //Debug.Log("Input: ");
        //Debug.Log(currentPlayer);
        //Debug.Log("X: " + boardSquareCoordinates[0] + "Y: " + boardSquareCoordinates[1]);

        HashSet<int[]> validMovesList = new HashSet<int[]>();
        //int[] tempCoord;

        //check NW
        //Debug.Log("Checking NW");

        // NW [-1,-1]
        int[] array;

        array = succesfulMoveInDiraction(boardSquareCoordinates, -1, -1, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + array[1] + "Y: " + array[2]);
            validMovesList.Add(new int[] { array[1], array[2], -1, -1 });
        }

        //check N [-1, 0]
        //Debug.Log("Checking N");
        array = succesfulMoveInDiraction(boardSquareCoordinates, -1, 0, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + array[1] + "Y: " + array[2]);
            validMovesList.Add(new int[] { array[1], array[2], -1, 0 });
        }
        //check NE [+1,-1]
        //Debug.Log("Checking NE");

        array = succesfulMoveInDiraction(boardSquareCoordinates, 1, -1, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + array[1] + "Y: " + array[2]);
            validMovesList.Add(new int[] { array[1], array[2], 1, -1 });
        }

        //check E[0,1]
        //Debug.Log("Checking E");
        array = succesfulMoveInDiraction(boardSquareCoordinates, 0, 1, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + array[1] + "Y: " + array[2]);
            validMovesList.Add(new int[] { array[1], array[2], 0, 1 });
        }
        //check SE[1,1]
        //Debug.Log("Checking SE");
        array = succesfulMoveInDiraction(boardSquareCoordinates, 1, 1, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + array[1] + "Y: " + array[2]);
            validMovesList.Add(new int[] { array[1], array[2], 1, 1 });
        }

        //check S[1,0]
        //Debug.Log("Checking S");
        array = succesfulMoveInDiraction(boardSquareCoordinates, 1, 0, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + array[1] + "Y: " + array[2]);
            validMovesList.Add(new int[] { array[1], array[2], 1, 0 });
        }

        //check SW[-1,1]
        //Debug.Log("Checking SW");
        array = succesfulMoveInDiraction(boardSquareCoordinates, -1, 1, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + array[1] + "Y: " + array[2]);
            validMovesList.Add(new int[] { array[1], array[2], -1, 1 });
        }

        //check W[0,-1]
        //Debug.Log("Checking W");
        array = succesfulMoveInDiraction(boardSquareCoordinates, 0, -1, currentPlayer);
        if (array[0] == 1)//encountered empty place
        {
            //Debug.Log("Adding: X: " + array[1] + "Y: " + array[2]);
            validMovesList.Add(new int[] { array[1], array[2], 0, -1 });
        }

        //Debug.Log("Valids:");
        //Debug.Log(validMovesList.Count);
        return validMovesList;
    }
    public int[] succesfulMoveInDiraction(int[] boardSquareCoordinates, int x, int y, PlayerEnum currentPlayer)
    {
        int hasEnemies = 0;
        bool reachedNullOrAlly = false;
        int[] tempCoords = (int[])boardSquareCoordinates.Clone();
        //Debug.Log("-------- INITIAL Y,X " + boardSquareCoordinates[1] + " " + boardSquareCoordinates[0]);
        while (!reachedNullOrAlly)
        {
            tempCoords[1] += x;
            tempCoords[0] += y;

            if (inRange(tempCoords[0]) && inRange(tempCoords[1]))
            {
                //Debug.Log("Checking Y,X " + tempCoords[1] + " " + tempCoords[0]);

                if (board[tempCoords[1], tempCoords[0]].belongsToPlayer == PlayerEnum.none ||
                     board[tempCoords[1], tempCoords[0]].belongsToPlayer == currentPlayer)
                {
                    if (hasEnemies == 1 && board[tempCoords[1], tempCoords[0]].belongsToPlayer == currentPlayer)
                    {
                        hasEnemies = 0;
                    }
                    //Debug.Log("REACHEDNullOrAlly");
                    reachedNullOrAlly = true;

                }
                else
                {
                    //Debug.Log("HAS ENEMIES");
                    hasEnemies = 1;
                }
            }
            else
            {
                hasEnemies = 0;
                //Debug.Log("reachedNullOrAlly +++ out of bond");
                reachedNullOrAlly = true;
            }

        }


        return new int[] { hasEnemies, tempCoords[0], tempCoords[1] };
    }

    public bool inRange(int value)
    {
        return value < boardLength && value >= 0;
    }


    public int updateBeatPieces(List<int[]> validMoves, IPlayer currentPlayer)
    {
        PlayerEnum currentTurn = currentPlayer.color;
        int changedPieces = 0;

        foreach (var XYDirection in validMoves)
        {
            //Debug.Log("MADE MOVE   X:" + XYDirection[0] + " , Y:" + XYDirection[1] + " , DIRECTION: " + XYDirection[2] + ", " + XYDirection[3]);

            if (currentTurn == PlayerEnum.black)
            {
                //Debug.Log("Enemy color: " + PlayerEnum.white);
                changePieces(XYDirection[0], XYDirection[1], XYDirection[2], XYDirection[3], currentTurn, PlayerEnum.white, ref changedPieces);
            }
            else changePieces(XYDirection[0], XYDirection[1], XYDirection[2], XYDirection[3], currentTurn, PlayerEnum.black, ref changedPieces);
        }

        if (boardUpdateEvent != null)
            boardUpdateEvent();
        Debug.Log("===========ChangedPieces: " + changedPieces);
        return changedPieces;
    }

    public void changePieces(int coordX, int coordY, int x, int y, PlayerEnum colorCurrent, PlayerEnum colorEnemy, ref int changed)
    {
        //Debug.Log("ChangePiece  Y: " + (coordY) + ", X: " + (coordX));
        board[coordY, coordX].belongsToPlayer = colorCurrent;
        //changed++;
        //Debug.Log("belongsToPlayer " + (board[coordY - x, coordX - y].belongsToPlayer));
        //Debug.Log("Y: " + (coordY - x) + ", X: " + (coordX - y));
        while (board[coordY - x, coordX - y].belongsToPlayer == colorEnemy)
        {
            coordX -= y;
            coordY -= x;
            //Debug.Log("ChangePiece  Y: " + (coordY) + ", X: " + (coordX));
            board[coordY, coordX].belongsToPlayer = colorCurrent;
            changed++;

        }
        //while next is enemy (must be since we checked)

    }
}




