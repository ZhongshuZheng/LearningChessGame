using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controller for the level select module
/// </summary>
public class LevelController : BaseController
{
    public LevelController() : base() {
        GameApp.ViewManager.Register(ViewTypes.SelectLevelView, new ViewInfo() {
            PrefabName = "SelectLevelView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            SortingOrder = 0
        });

        SetModel(new LevelModel());

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init() {
        GetModel().Init();
    }


    // event 
    public override void InitModuleEvent() {
        RegisterFunc(Defines.openSelectLevelView, openSelectLevelView);
    }

    private void openSelectLevelView(params object[] args) {
        GameApp.ViewManager.Open(ViewTypes.SelectLevelView, args);
    }


    // message 
    public override void InitGlobalEvent() {
        GameApp.MsgCenter.AddEvent(Defines.showLevelDesEvent, showLevelDesEvent);
        GameApp.MsgCenter.AddEvent(Defines.hideLevelDesEvent, hideLevelDesEvent);
    }

    public override void RemoveGlobalEvent() {
        GameApp.MsgCenter.RemoveEvent(Defines.showLevelDesEvent, showLevelDesEvent);
        GameApp.MsgCenter.RemoveEvent(Defines.hideLevelDesEvent, hideLevelDesEvent);
    }

    private void showLevelDesEvent(object arg) {
        LevelModel model = GetModel<LevelModel>();
        model.currentLevel = model.GetLevel((int)arg);
        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewTypes.SelectLevelView).ShowLevelDes(
            model.currentLevel
        );
    }

    private void hideLevelDesEvent(object arg) {
        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewTypes.SelectLevelView).HideLevelDes();
    }



}
