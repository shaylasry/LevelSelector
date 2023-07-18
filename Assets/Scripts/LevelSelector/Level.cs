using System;

[Serializable]
public class Level
{
    public int levelNumber;
    public string levelName;
    public int scoring;
    public bool isAvailable;

    public Level(int levelNumber, string levelName, int scoring, bool isAvailable)
    {
        this.levelNumber = levelNumber;
        this.levelName = levelName;
        this.scoring = scoring;
        this.isAvailable = isAvailable;
    }
}