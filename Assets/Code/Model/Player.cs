using System;

public class Player : IPlayer
{
    public PlayerEnum color { get;private set; }

    public Player(bool isWhite) {
        this.isWhite = isWhite;
    }
}