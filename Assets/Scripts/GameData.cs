using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Data that needs saving/loading
public class GameData
{
    public int gems;
    //public int score;

    public GameData()
    {
        gems = Balance.rareAccountBalance;
        //code to save score
    }
}
