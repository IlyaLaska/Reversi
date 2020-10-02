using System;

public interface IPlayer
{
    public PlayerEnum color { get; set; }

    public int[] getMove();
}
