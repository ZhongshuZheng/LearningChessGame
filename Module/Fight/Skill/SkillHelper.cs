using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Skill helper for ISkill 
/// </summary>
public static class SkillHelper {

    public static bool IsModelInSkillRange(this ISkill skill, ModelBase model) {
        ModelBase unit = skill as ModelBase;
        if (skill.skillPro.AttackRange >= unit.GetDis(model)) {
            return true;
        }
        return false;
    }

    public static List<ModelBase> GetTargets(this ISkill skill) {
        switch (skill.skillPro.Target) {
            case 0:
                // mouse point to the target
                return GetMousePositionTarget(skill);
            case 1:
                // all the heros and enemys in the range
                return GetUnitsInRange(skill);
            case 2:
                // all the heros in the range
                return GetHerosInRange(skill);
        }
        return null;
    }

    private static List<ModelBase> GetMousePositionTarget(ISkill skill) {
        ModelBase unit = skill as ModelBase;
        Collider2D targetCol = Tools.ScreenPointToRay2D(Camera.main);
        if (targetCol == null) {
            return null;
        }
        ModelBase model = targetCol.GetComponent<ModelBase>();
        if (model == null || model.Type != skill.skillPro.TargetType || unit.GetDis(model) > skill.skillPro.AttackRange) {
            return null;
        }
        return new List<ModelBase> {model};
    }

    private static List<ModelBase> GetUnitsInRange(ISkill skill) {
        List<ModelBase> units = new List<ModelBase>();
        foreach (var unit in GameApp.FightManager.heros) {
            if (skill.IsModelInSkillRange(unit)) {
                units.Add(unit);
            }
        }
        foreach (var unit in GameApp.FightManager.enemies) {
            if (skill.IsModelInSkillRange(unit)) {
                units.Add(unit);
            }
        }
        return units;
    }

    private static List<ModelBase> GetHerosInRange(ISkill skill) {
        List<ModelBase> units = new List<ModelBase>();
        foreach (var unit in GameApp.FightManager.heros) {
            if (skill.IsModelInSkillRange(unit)) {
                units.Add(unit);
            }
        }
        return units;
    }

}
