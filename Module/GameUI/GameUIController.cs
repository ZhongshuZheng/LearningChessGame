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
            parentTf = GameApp.ViewManager.canvasTf,
            SortingOrder = 0
        });
        // Set menu View
        GameApp.ViewManager.Register(ViewTypes.SetView, new ViewInfo() {
            PrefabName = "SetView", 
            controller = this, 
            parentTf = GameApp.ViewManager.canvasTf,
            SortingOrder = 1
        });
        // Message View
        GameApp.ViewManager.Register(ViewTypes.MessageView, new ViewInfo() {
            PrefabName = "MessageView", 
            controller = this, 
            parentTf = GameApp.ViewManager.canvasTf,
            SortingOrder = 999
        });


        InitGlobalEvent();
        InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.openStartView, openStartView);
        RegisterFunc(Defines.openSetView, openSetView);
        RegisterFunc(Defines.openMessageView, openMessageView);
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

    private void openMessageView(params object[] args) {
        GameApp.ViewManager.Open(ViewTypes.MessageView, args);
    }
}
