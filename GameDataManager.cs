using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manager for main game data, including basic information of player
/// </summary>
public class GameDataManager {

    public List<int> heros;

    public int Money;

    public GameDataManager() {
        heros = new List<int>();

        // the default hero id 
        heros.Add(10001);
        heros.Add(10002);
        heros.Add(10003);
    }

}
