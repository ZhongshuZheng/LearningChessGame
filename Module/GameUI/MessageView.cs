using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MessageInfo {
    public string msgTxt;
    public Action okCallback;
    public Action noCallback;
}

/// <summary>
/// Message View
/// </summary>
public class MessageView : BaseView {

    MessageInfo info;

    protected override void OnAwake() {
        base.OnAwake();

        Find<Button>("okBtn").onClick.AddListener(onOkBtn);
        Find<Button>("noBtn").onClick.AddListener(onNoBtn);
    }

    public override void Open(params object[] args)
    {
        info = args[0] as MessageInfo;
        Find<Text>("content/txt").text = info.msgTxt;
    }

    private void onOkBtn() {
        info.okCallback?.Invoke();
    }

    private void onNoBtn() {
        info.noCallback?.Invoke();
        GameApp.ViewManager.Close(ViewId);
    }

}
