using System;
using UnityEngine.Serialization;

[Serializable]
public class Level
{
    public int levelNum;
    public string levelName;
    public int scoring;
    public bool isAvailable;

    public Level(int levelNum, string levelName, int scoring, bool isAvailable)
    {
        this.levelNum = levelNum;
        this.levelName = levelName;
        this.scoring = scoring;
        this.isAvailable = isAvailable;
    }
}