using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// select hero menu in fight view
/// </summary>
public class FightSelectHeroView : BaseView {

    public override void Open(params object[] args) {
        base.Open(args);

        // 1. get SelectHeroIconButton prefab
        GameObject prefabObj = Find("bottom/grid/item");
        Transform parentTf = Find("bottom/grid").transform;

        // 2. load the hero information & generate prefab and attach a script to it
        ConfigData heroData = GameApp.ConfigManager.GetConfigData("player");
        for (int i = 0; i < GameApp.GameDataManager.heros.Count; i++) {
            int heroId = GameApp.GameDataManager.heros[i];
            Dictionary<string, string> hero = heroData.GetDataById(heroId);

            GameObject icon = Object.Instantiate(prefabObj, parentTf);

            // attach script and show
            HeroItem heroItem = icon.AddComponent<HeroItem>();
            heroItem.Init(heroData.GetDataById(heroId));
        }




    }

}