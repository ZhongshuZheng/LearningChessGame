using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Config manager to manage all the configs from csv
/// </summary>
public class ConfigManager 
{
    private Dictionary<string, ConfigData> loadList;  // Registered config datas
    private Dictionary<string, ConfigData> configs;  // Loaded config datas

    public ConfigManager() {
        loadList = new Dictionary<string, ConfigData>();
        configs = new Dictionary<string, ConfigData>();
    }

    public void Register(string fileName, ConfigData config) {
        loadList[fileName] = config;
    }

    public void LoadAllConfigs() {
        foreach (var item in loadList) {
            item.Value.LoadConfig();
            configs[item.Key] = item.Value;
        }
        loadList.Clear();
    }

    public ConfigData GetConfigData(string name) {
        if (configs.ContainsKey(name)) {
            return configs[name];
        } else {
            Debug.LogError($"ConfigManager: Got missed config {name}");
            return null;
        }
    }
}
