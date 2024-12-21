using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Enemy class
/// </summary>
public class Enemy : ModelBase {

    protected override void Start() {
        base.Start();

        Data = GameApp.ConfigManager.GetConfigData("enemy").GetDataById(Id);
        Type = int.Parse(Data["Type"]);
        Attack = int.Parse(Data["Attack"]);
        Step = int.Parse(Data["Step"]);
        MaxHp = int.Parse(Data["Hp"]);
        CurHp = MaxHp;

    }

    protected override void OnSelectCallBack(object arg)
    {
        if (GameApp.CommandManager.isRunningCommand) {
            return;
        }
        base.OnSelectCallBack(arg);
        GameApp.ViewManager.Open(ViewTypes.EnemyDesView, this);
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewManager.Close((int)ViewTypes.EnemyDesView);
    }
    
}
