using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for common game UIs, including settign menu, memo menu, start menu, etc.
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        // Start game View
        GameApp.ViewManager.Register(ViewTypes.StartView, new ViewInfo() {
            PrefabName = "StartView", 
            controller = this, 
            parentTf = GameApp.ViewManager.canvasTf
        });
        // Set menu View
        GameApp.ViewManager.Register(ViewTypes.SetView, new ViewInfo() {
            PrefabName = "SetView", 
            controller = this, 
            parentTf = GameApp.ViewManager.canvasTf
        });


        InitGlobalEvent();
        InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.openStartView, openStartView);
        RegisterFunc(Defines.openSetView, openSetView);
    }


    // Call back function to be register in module --------------------------------
    private void openStartView(params object[] args) 
    {
        GameApp.ViewManager.Open(ViewTypes.StartView, args);
    }

    private void openSetView(params object[] args) 
    {
        GameApp.ViewManager.Open(ViewTypes.SetView, args);
    }
}
