using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Controller for the fight model
/// </summary>
public class FightController : BaseController {

    public FightController() : base() {
        SetModel(new FightModel(this));

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
        GameApp.ViewManager.Register(ViewTypes.DragHeroView, new ViewInfo() {
            PrefabName = "DragHeroView",
            parentTf = GameApp.ViewManager.worldCanvasTf,  // I don't know what is the difference exactly
            controller = this, 
            SortingOrder = 2
        });
        GameApp.ViewManager.Register(ViewTypes.TipView, new ViewInfo() {
            PrefabName = "TipView",
            parentTf = GameApp.ViewManager.canvasTf,  
            controller = this, 
            SortingOrder = 2 
        });
        GameApp.ViewManager.Register(ViewTypes.HeroDesView, new ViewInfo() {
            PrefabName = "HeroDesView",
            parentTf = GameApp.ViewManager.canvasTf,  
            controller = this, 
            SortingOrder = 2
        });
        GameApp.ViewManager.Register(ViewTypes.EnemyDesView, new ViewInfo() {
            PrefabName = "EnemyDesView",
            parentTf = GameApp.ViewManager.canvasTf,  
            controller = this, 
            SortingOrder = 2
        });
        GameApp.ViewManager.Register(ViewTypes.SelectOptionView, new ViewInfo() {
            PrefabName = "SelectOptionView",
            parentTf = GameApp.ViewManager.canvasTf,  
            controller = this, 
            SortingOrder = 2
        });

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init() {
        model.Init();
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
