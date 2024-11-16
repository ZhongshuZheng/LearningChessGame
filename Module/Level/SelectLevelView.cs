using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// level select view
/// </summary>
public class SelectLevelView : BaseView
{

    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("close").onClick.AddListener(OnCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(OnBattleBtn);

    }

    private void OnCloseBtn() {
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";
        loadingModel.callback = () => {
            Controller.ApplyControllerFunc(ControllerTypes.GameUIController, Defines.openStartView);
        };
        ApplyControllerFunction((int)ControllerTypes.LoadingController, Defines.loadingScence, loadingModel);
    }

    public void ShowLevelDes(LevelData levelData) {
        Find<Text>("level/name/txt").text = levelData.name;
        Find<Text>("level/des/txt").text = levelData.des;
        Find("level").SetActive(true);
    }

    public void HideLevelDes() {
        Find("level").SetActive(false);
    }

    public void OnBattleBtn() {
        GameApp.ViewManager.Close(ViewId);
        GameApp.CameraManager.ResetPostion();

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = Controller.GetModel<LevelModel>().currentLevel.sceneName;
        loadingModel.callback = () => {
            // show some thing about battle view
            Controller.ApplyControllerFunc((int)ControllerTypes.FightController, Defines.beginFight);
        };
        ApplyControllerFunction((int)ControllerTypes.LoadingController, Defines.loadingScence, loadingModel);
    }
}
