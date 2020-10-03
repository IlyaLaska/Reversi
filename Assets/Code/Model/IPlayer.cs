using System;

public interface IPlayer
{
    int score { get; set; }
    PlayerEnum color { get; set; }
    int[] currentTurnCoords { get; set; }

    int[] getMove();
}
