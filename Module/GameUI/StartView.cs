using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Start view page
/// </summary>
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("startBtn").onClick.AddListener(onStartBtn);
        Find<Button>("setBtn").onClick.AddListener(onSetBtn);
        Find<Button>("quitBtn").onClick.AddListener(onQuitBtn);
    }

    // Button Events ----------------------------------------------------------------
    private void onStartBtn()
    {}

    private void onSetBtn()
    {
        Controller.ApplyFunc(Defines.openSetView);
    }

    private void onQuitBtn()
    {}
}
