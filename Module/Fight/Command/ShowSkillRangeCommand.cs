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
            Debug.Log("attack");
            return true;
        } else if (Input.GetMouseButtonDown(1)) {
            skill.HideSkillRange();
            return true;
        }
        return false;
    }

}
