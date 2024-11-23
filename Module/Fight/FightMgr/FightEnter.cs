using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// State when enter the fightground
/// </summary>
public class FightEnter : FightUnitBase {

    public override void Init() {
        GameApp.MapManager.Init();
        GameApp.FightManager.EnterFight();
    }
}
