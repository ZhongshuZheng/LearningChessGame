using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// view of the player-action option list
/// </summary>
public class SelectOptionView : BaseView {

    Dictionary<int, OptionItem> optionDict;

    public override void InitData()
    {
        base.InitData();
        
        // Create and collect all the possible options
        optionDict = new Dictionary<int, OptionItem>();

        FightModel fightModel = Controller.GetModel() as FightModel;
        foreach (var option in fightModel.options) {

            Transform tf = Find("bg/grid").transform;
            GameObject optPrefab = Find("bg/grid/item");

            GameObject opt = Instantiate(optPrefab, tf);
            opt.SetActive(false);
            OptionItem optScript = opt.AddComponent<OptionItem>();
            optScript.Init(option);

            optionDict.Add(option.Id, optScript);
        }

    }

    public override void Open(params object[] args) {
        // Open the option list for the given Hero.
        // Needs two parameters
        // 1. string: the hero's datas
        // 2. vector2: the showing positoin

        if (args.Length < 2) {
            Debug.LogError("SelectionOptionView: no parameter, needs two");
            return;
        }

        Hero hero = args[0] as Hero;
        string dataString = hero.Data["Event"];
        Vector2 pos = (Vector2)args[1];
        if (dataString == "" || dataString == null) {
            Debug.LogWarning("SelectionOptionView: failed to load hero's event list");
            return;
        }

        // move to the right position 
        Find("bg/grid").transform.position = pos;

        // show the options
        ClearOptions();
        foreach (var option in dataString.Split("-")) {
            optionDict[int.Parse(option)].gameObject.SetActive(true);
        }

    }

    public void ClearOptions() {
        foreach (var option in optionDict.Values) {
            option.gameObject.SetActive(false);
        }
    }

}
