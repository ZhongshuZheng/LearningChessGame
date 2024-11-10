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

    }

    private void OnCloseBtn() {
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";
        loadingModel.callback = () => {
            Controller.ApplyControllerFunc(ControllerTypes.GameUIController, Defines.openStartView);
        };
        ApplyControllerFunction((int)ControllerTypes.LoadingController, Defines.loadingScence, loadingModel);
    }

    public void ShowLevelDes() {
        Find("level").SetActive(true);
    }

    public void HideLevelDes() {
        Find("level").SetActive(false);
    }
}
