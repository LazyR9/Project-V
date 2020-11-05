using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{

    public int playerHealth;
    public string levelName;
    public bool isMultiplayer;

    public LevelData(LevelManager manager)
    {
        playerHealth = manager.playerHealth;
        levelName = manager.levelName;
        isMultiplayer = manager.isMultiplayer;
    }

}
