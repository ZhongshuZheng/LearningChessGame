using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loading Controller
/// 
/// open the loading view, onload the given model and callback the model's callback function
/// </summary>
public class LoadingController : BaseController
{

    AsyncOperation asyncOp;

    public LoadingController() : base() {
        GameApp.ViewManager.Register(ViewTypes.LoadingView, new ViewInfo {
            PrefabName = "LoadingView", 
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            SortingOrder = 999
        });

        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void InitModuleEvent() {
        RegisterFunc(Defines.loadingScence, loadingScence);

    }

    private void loadingScence(params object[] args) {
        // 1. open the loading view
        GameApp.ViewManager.Open(ViewTypes.LoadingView);

        // 2. load the model and async load the next Scene
        LoadingModel loadingmodel = args[0] as LoadingModel;
        SetModel(loadingmodel);
        asyncOp = SceneManager.LoadSceneAsync(loadingmodel.SceneName);
        asyncOp.completed += onLoadedEndCallBack;

    }

    private void onLoadedEndCallBack(AsyncOperation op) {
        op.completed -= onLoadedEndCallBack;

        // delay for a little while
        GameApp.TimerManager.Register(1f, () => {

            GetModel<LoadingModel>().callback?.Invoke();

            GameApp.ViewManager.Close((int)ViewTypes.LoadingView);
        });
    }

}
