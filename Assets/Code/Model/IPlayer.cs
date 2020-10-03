using System;

public interface IPlayer
{
    int score { get; set; }
    PlayerEnum color { get; set; }

    int[] getMove(int[][] validMoves);
}
