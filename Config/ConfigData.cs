using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// Class to load the csv
/// </summary>
public class ConfigData 
{
    private Dictionary<int, Dictionary<string, string>> datas;
    public string fileName;

    public ConfigData(string fileName) {
        this.fileName = fileName;
        datas = new Dictionary<int, Dictionary<string, string>>();
    }

    // public TextAsset LoadFile() {
    //     return Resources.Load<TextAsset>($"Data/{fileName}");
    // }

    public void LoadConfig() {
        string[] dataArray = Resources.Load<TextAsset>($"Data/{fileName}").text.Split('\n');
        string[] title = dataArray[0].Trim().Split(',');

        for (int i = 2; i < dataArray.Length; i++) {
            string[] dataLine = dataArray[i].Trim().Split(',');
            Dictionary<string, string> tempData = new Dictionary<string, string>();

            for (int j = 0; j < dataLine.Length; j++) {
                tempData[title[j]] = dataLine[j];
            }
            
            datas[int.Parse(tempData["Id"])] = tempData;
        }
    }

    public Dictionary<string, string> GetDataById(int id) {
        if (datas.ContainsKey(id)) {
            return datas[id];
        } else {
            Debug.LogError($"ConfigData: Wrong config id: {id}");
            return null;
        }
    }

    public Dictionary<int, Dictionary<string, string>> GetLines() {
        return datas;
    }

}
