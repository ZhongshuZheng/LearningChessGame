using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Player's turn 
/// </summary>
public class FightPlayerUnit : FightUnitBase {

    public override void Init() {
        base.Init();
        GameApp.ViewManager.Open(ViewTypes.TipView, "Your Turn");
    }
}
