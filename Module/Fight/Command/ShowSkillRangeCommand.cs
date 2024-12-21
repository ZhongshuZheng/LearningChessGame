using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command to show the skill range
/// </summary>
public class ShowSkillRangeCommand : BaseCommand {

    ISkill skill;

    public ShowSkillRangeCommand(ModelBase model) : base(model) {
        skill = model as ISkill;
    }

    public override void Do() {
        skill.ShowSkillRange();
    }

    public override bool Update(float dt) {
        if (Input.GetMouseButtonDown(0)) {
            skill.HideSkillRange();
            if (skill.GetTargets() != null) {
                GameApp.CommandManager.AddCommand(new SkillCommand(model));
                return true;
            }
            skill.ShowSkillRange();
        } else if (Input.GetMouseButtonDown(1)) {
            skill.HideSkillRange();
            GameApp.CommandManager.UnDo();
            GameApp.CommandManager.UnDo();
            return true;
        }
        return false;
    }

}
