using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Controller for the fight model
/// </summary>
public class FightController : BaseController {

    public FightController() : base() {
        GameApp.ViewManager.Register(ViewTypes.FightView, new ViewInfo() {
            PrefabName = "FightView",
            parentTf = GameApp.ViewManager.canvasTf, 
            controller = this, 
            SortingOrder = 0
        });        
        GameApp.ViewManager.Register(ViewTypes.FightSelectHeroView, new ViewInfo() {
            PrefabName = "FightSelectHeroView",
            parentTf = GameApp.ViewManager.canvasTf, 
            controller = this, 
            SortingOrder = 1
        });        

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void InitModuleEvent() {
        RegisterFunc(Defines.beginFight, OnBeginFightCallback);
    }

    public void OnBeginFightCallback(params object[] args) {
        GameApp.FightManager.ChangeState(GameState.Enter);

        GameApp.ViewManager.Open(ViewTypes.FightView);
        GameApp.ViewManager.Open(ViewTypes.FightSelectHeroView);
    }
}
