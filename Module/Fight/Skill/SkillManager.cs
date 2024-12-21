using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;


/// <summary>
/// The class to collect and run all the skill. 
/// Besides, this class has an own GameTimer, outside of the TimerManager, inside.
/// 
/// Why we need this, or in other word, how we implement the Attack action:
/// the skill program flow is like this:
///     1. we click the "Attack" button, then call for the ShowSkillRangeCommand;
///     2. during the ShowSKillRangeCommand, we click the target and finished the ...RangeCommand. The SkillCommand will be Added after that;
///     3. in the SkillCommand, we check sth. and add a new "Run Skill" action by, SkillManager, this class;
///     4. this class run the skill progress by a timer, funcitons in the SKillHelper and some balabala.
///     
/// Meanwhile, i think this class can be replaced by the SKillCommand and GameTimer. Do we really need this class to control the running of
/// skills? I think not till now. We can change the flow like this:
///     1. click the "Attack" and call the "ShowSKillRangeCommand" as well;
///     2. Add "SkillCommand" by click sth. during the "ShowSkillRangeCommand";
///     3. In the Do of the "SkillCommand", run the skill progress with GameTimer, and record the finished flag by the callback fun.
/// the difference is that this short implementation can not load many skills in the same time.
/// </summary>
public class SkillManager {

    private GameTimer timer;
    private Queue<(ISkill skill, List<ModelBase> targets, Action callback)> skills;

    public SkillManager() {
        timer = new GameTimer();
        skills = new Queue<(ISkill skill, List<ModelBase> targets, Action callback)>();
    }

    public void Update(float dt) {
        timer.Update(dt);
        if (timer.Count() == 0 && skills.Count > 0) {
            var next = skills.Dequeue();
            if (next.targets != null) {
                UseSkill(next.skill, next.targets, next.callback);
            }
        }
    }

    public void AddSkill(ISkill skill, List<ModelBase> targets, Action callback) {
        skills.Enqueue((skill, targets, callback));
    }

    public void UseSkill(ISkill skill, List<ModelBase> targets, Action callback) {
        ModelBase current = skill as ModelBase;
        int targetCount = math.min(skill.skillPro.AttackCount, targets.Count);
        if (targetCount <= 0) {
            return;
        }

        current.LookAtModel(targets[0]);
        current.PlaySound(skill.skillPro.Sound);
        current.PlayAnimation(skill.skillPro.AniName);

        // delay attack
        timer.RegisterTimer(skill.skillPro.AttackTime, () => {
            for (int i = 0; i < targetCount; i++) {
                targets[i].GotHit(skill);
            }
        });

        // finished and back to idle 
        timer.RegisterTimer(skill.skillPro.Time, () => {
            current.PlayAnimation("idle");
            callback?.Invoke();
        });

    }

    public bool IsRunningSKill() {
        if (timer.Count() == 0 && skills.Count == 0) {
            return false;
        }
        return true;
    }

    public void Clear() {
        timer.Break();
        skills.Clear();
    }


}
