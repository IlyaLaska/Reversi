using System;

public interface IPlayer
{
    public int score { get; set; }
    public PlayerEnum color { get; set; }

    public int[] getMove(int[][] validMoves);
}
