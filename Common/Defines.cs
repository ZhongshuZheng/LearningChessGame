using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Defines
{
    // event names for GameUIController
    public static readonly string openStartView = "openStartView";
    public static readonly string openSetView = "openSetView";
    public static readonly string openMessageView = "openMessageView";

    // event names for LoadingController
    public static readonly string loadingScence = "loadingScence";

    // event names for LevelController
    public static readonly string openSelectLevelView = "openSelectLevelView";

    // event names for FightController
    public static readonly string beginFight = "beginFight";



    // Global Message Func
    public static readonly string showLevelDesEvent = "showLevelDesEvent";
    public static readonly string hideLevelDesEvent = "hideLevelDesEvent";
    public static readonly string OnSelectEvent = "OnSelectEvent";
    public static readonly string OnUnSelectEvent = "OnUnSelectEvent";


    // Temp Message Func
    public static readonly string OnAttackEvent = "OnAttackEvent";
    public static readonly string OnIdleEvent = "OnIdleEvent";
    public static readonly string OnCancelEvent = "OnCancelEvent";
    public static readonly string OnRemoveHeroToSceneEvent = "OnRemoveHeroToSceneEvent";

}
