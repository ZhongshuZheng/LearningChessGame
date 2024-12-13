using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// options
public class OptionData {
    public int Id;
    public string EventName;
    public string Name;
}

/// <summary>
/// Model for the fight controller and some related view
/// </summary>
public class FightModel : BaseModel {

    public List<OptionData> options; 
    public ConfigData optionConfig;

    public FightModel(BaseController ctl) : base(ctl) {
        options = new List<OptionData>();
    }

    public override void Init() {
        optionConfig = GameApp.ConfigManager.GetConfigData("option");
        foreach (var item in optionConfig.GetLines()) {
            OptionData opData = new OptionData();
            opData.Id = int.Parse(item.Value["Id"]);
            opData.EventName = item.Value["EventName"];
            opData.Name = item.Value["Name"];
            options.Add(opData);
        }
    }

}
