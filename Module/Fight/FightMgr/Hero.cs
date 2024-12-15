using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Hero Monobehaviour in battle ground
/// </summary>
public class Hero : ModelBase, ISkill {

    public void Init(Dictionary<string, string> data, int row, int col) {
        this.Data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(data["Id"]);
        Type = int.Parse(data["Type"]);
        Attack = int.Parse(data["Attack"]);
        Step = int.Parse(data["Step"]);
        MaxHp = int.Parse(data["Hp"]);
        CurHp = MaxHp;
        skillPro = new SkillProperty(int.Parse(data["Skill"]));
    }

    protected override void OnSelectCallBack(object arg)
    {
        base.OnSelectCallBack(arg);
        GameApp.ViewManager.Open(ViewTypes.HeroDesView, this);

        // call the showPath command && register option events
        if (GameApp.FightManager.gameState == GameState.FightPlayerUnit && !IsStop && !GameApp.CommandManager.isRunningCommand) {
            addOptionEvents(); 
            GameApp.CommandManager.AddCommand(new ShowPathCommand(this));
        }
    }

    protected override void OnUnSelectCallBack(object arg)
    {
        base.OnUnSelectCallBack(arg);
        GameApp.ViewManager.Close((int)ViewTypes.HeroDesView);
    }

    private void addOptionEvents() {
        // add hero's options into Temp Events
        GameApp.MsgCenter.AddTempEvent(Defines.OnAttackEvent, onAttackCallBack);
        GameApp.MsgCenter.AddTempEvent(Defines.OnIdleEvent, onIdleCallBack);
        GameApp.MsgCenter.AddTempEvent(Defines.OnCancelEvent, onCancelCallBack);
    }

    // Option events -----------------------------------------------------------------
    private void onAttackCallBack(object arg) {
        GameApp.CommandManager.AddCommand(new ShowSkillRangeCommand(this));
    }

    private void onIdleCallBack(object arg) {
        IsStop = true;
    }

    private void onCancelCallBack(object arg) {
        GameApp.CommandManager.UnDo();
    }

    // ISkill implementation ----------------------------------------------------------------
    public SkillProperty skillPro { get; set; }

    public void ShowSkillRange() {
        GameApp.MapManager.ShowSkillRange(this, skillPro.AttackRange, Color.red);
    }

    public void HideSkillRange() {
        GameApp.MapManager.HideSkillRange(this, skillPro.AttackRange);
    }

}
