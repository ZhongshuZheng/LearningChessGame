using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Game Controller, including tasks like start, load, save, exit, etc.
/// </summary>
public class GameController : BaseController
{
    public GameController() : base()
    {
        // do not have a view

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        // Open the login page from the GameUIController
        ApplyControllerFunc(ControllerTypes.GameUIController, Defines.openStartView);
    }

}
