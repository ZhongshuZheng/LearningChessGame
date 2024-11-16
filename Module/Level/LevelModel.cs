using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelData {
    public int id;
    public string name;
    public string sceneName;
    public string des;

    public bool isFinished;  // if ever finished the level

    public LevelData(Dictionary<string, string> levelConfig) {
        id = int.Parse(levelConfig["Id"]);
        name = levelConfig["Name"];
        sceneName = levelConfig["SceneName"];
        des = levelConfig["Des"];

        isFinished = false;
    }
}

/// <summary>
/// Level model
/// 
/// I think this Model is a little strange that as a "model", it contains a Dict wit all the levels which should be managed by
/// a specific LevelDataManager instead of a LevelModel.
/// It makes it a mess that the model is both used by controller to show the view and control all the level infos.
/// </summary>
public class LevelModel : BaseModel {

    private ConfigData levelConfig;
    private Dictionary<int, LevelData> levels;  // all the level data

    public LevelData currentLevel;

    public LevelModel() {
        levels = new Dictionary<int, LevelData>();
    }

    public override void Init() {
        levelConfig = GameApp.ConfigManager.GetConfigData("level");
        foreach (var item in levelConfig.GetLines()) {
            levels[item.Key] = new LevelData(item.Value);
        }
    }

    public LevelData GetLevel(int id) {
        return levels[id];
    }
}
