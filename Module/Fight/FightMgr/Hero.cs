using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Hero Monobehaviour in battle ground
/// </summary>
public class Hero : ModelBase {

    public void Init(Dictionary<string, string> data, int row, int col) {
        this.Data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(data["Id"]);
        Type = int.Parse(data["Type"]);
        Attack = int.Parse(data["Attack"]);
        Step = int.Parse(data["Step"]);
        MaxHp = int.Parse(data["Hp"]);
        CurHp = MaxHp;
    }

}
