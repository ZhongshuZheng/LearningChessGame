using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Skill command, to run the attack action
/// </summary>
public class SkillCommand : BaseCommand {

    ISkill skill;

    public SkillCommand(ModelBase model) : base(model) {
        skill = model as ISkill;
    }

    public override void Do()
    {
        base.Do();
        List<ModelBase> targets = skill.GetTargets();
        if (targets != null && targets.Count > 0) {
            GameApp.SkillManager.AddSkill(skill, targets, null);
        }

    }


    public override bool Update(float dt) {
        if (!GameApp.SkillManager.IsRunningSKill()) {
            model.IsStop = true;
            return true;
        }

        return false;
    }

}
