﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Debug = UnityEngine.Debug;


public class Game
{
	public Board gameBoard;
	//public PlayerEnum currentTurn;
	public IPlayer black;
	public IPlayer white;
	public int[][] validMovesAndDirsForThisTurn;
	public int moveCounter = 0;
    public bool gameIsOver = false;
    public IPlayer currentPlayer;

    public delegate void GameEnd();
    public static event GameEnd gameEnded;
	//public int blackScore = 2;
	//public int whiteScore = 2;
	public Game(IPlayer first, IPlayer second)
	{
		black = first;
		white = second;
        gameBoard = new Board();
        //currentTurn = PlayerEnum.black;
        currentPlayer = black;
        //this.playRound();
    }

    public void initGame()
    {

        gameBoard.initBoard();
        updateValidMovesList();
    }
    public void playRound()
    {
        //get coordinates of next move
        List<int[]> takenCoordsAndDirections = getMoveFromPlayer();
        if (takenCoordsAndDirections == null) return;
        int beatPiecesCount = gameBoard.updateBeatPieces(takenCoordsAndDirections, currentPlayer);
        updateScore(beatPiecesCount);

        changePlayer();
        updateValidMovesList();

        //playRound();
    }
    public void updateValidMovesList()//uncomment
    {
        Debug.Log("IN updateValidMovesList. currPlayer: " + currentPlayer.color);
        //get valid moves list
        this.validMovesAndDirsForThisTurn = gameBoard.getValidMovesList(currentPlayer);
        //Debug.Log(validMovesAndDirsForThisTurn.Length);
        if (this.validMovesAndDirsForThisTurn.Length < 1)
        {
            if (isMaxScore())
            {
                gameEnded();
                return;
            }
            // GAME IS OVER EVENT

            // game is not over, no available turns
            changePlayer();
            //updateValidMovesList();//UNCOMMent
            //playRound();
        }
    }
    private bool isMaxScore()
    {
        return black.score + white.score == 64;
    }

    //private void changeTurn()
    //{
    //    currentTurn = currentTurn == PlayerEnum.black ? PlayerEnum.white : PlayerEnum.black;
    //}

    private void changePlayer()
    {
        currentPlayer = currentPlayer == white ? black : white;
    }

    private void updateScore(int beatPiecesCount)
    {
        currentPlayer.score += beatPiecesCount + 1;

        if (currentPlayer == black)
        {
            white.score -= beatPiecesCount;
        }
        else black.score -= beatPiecesCount;
    }

    private List<int[]> getMoveFromPlayer()
    {
        //int[] selectedSquare = currentPlayer.getMove(this.validMovesAndDirsForThisTurn);

        int[] selectedSquare = currentPlayer.currentTurnCoords;
        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAASEL Sq: " + selectedSquare[0] + " , " + selectedSquare[1]);
        List<int[]> takenCoordsAndDirections = new List<int[]>();
        //list of taken moves (coordinates same, direction different)
        for (int i = 0; i < validMovesAndDirsForThisTurn.Length; i++)
        {
            //Debug.Log("ValidM...: " + validMovesAndDirsForThisTurn[i][0] + " , " + validMovesAndDirsForThisTurn[i][1] + " , " + (Direction) validMovesAndDirsForThisTurn[i][2]);
            if (validMovesAndDirsForThisTurn[i][0] == selectedSquare[0] && validMovesAndDirsForThisTurn[i][1] == selectedSquare[1])//one of valids
            {
                takenCoordsAndDirections.Add(validMovesAndDirsForThisTurn[i]);
            }
        }
        if (takenCoordsAndDirections.Count == 0)//no allowed moves
        {
            return null;
            //	//INVALID MOVE - restart function?

        }
        return takenCoordsAndDirections;
    }
}
