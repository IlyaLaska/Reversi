using System;

public class Player : IPlayer
{
    public bool isWhite;
    public PlayerEnum color { get; set; }
    public int[] currentTurnCoords { get; set; }
    int IPlayer.score { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    PlayerEnum IPlayer.color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Player(bool isWhite) {
        this.isWhite = isWhite;
    }

    //int[] IPlayer.getMove(int[][] validMoves)
    //{
    //    throw new NotImplementedException();
    //}
}