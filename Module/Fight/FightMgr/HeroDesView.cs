using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hero status view
/// </summary>
public class HeroDesView : BaseView {

    public override void Open(params object[] args) {
        base.Open(args);
        Hero hero = (Hero)args[0];

        Find<Image>("bg/icon").SetIcon(hero.Data["Icon"]);
        Find<Image>("bg/hp/fill").fillAmount = (float)hero.CurHp / hero.MaxHp;
        Find<Text>("bg/hp/txt").text = $"{hero.CurHp} / {hero.MaxHp}";
        Find<Text>("bg/atkTxt/txt").text = hero.Attack.ToString();
        Find<Text>("bg/StepTxt/txt").text = hero.Step.ToString();
    }

}
