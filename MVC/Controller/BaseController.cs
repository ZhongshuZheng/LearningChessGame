using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Base class for Controler
/// </summary>
public class BaseController 
{
    private Dictionary<string, Action<object[]>> message; // message dictionary
    private BaseModel model; // model

    public BaseController()
    {
        message = new Dictionary<string, Action<object[]>>();
    }

    public virtual void Destory()
    {
        RemoveModuleEvent();
        RemoveGlobalEvent();

    }


    // View Operations --------------------------
    // seems to be called when view is operated by ViewManager
    public virtual void OnLoadView(IBaseView view) {}
    public virtual void OpenView(IBaseView view) {}
    public virtual void CloseView(IBaseView view) {}


    // Model Operations --------------------------
    public void SetModel(BaseModel model)
    {
        this.model = model;
    }

    public BaseModel GetModel()
    {
        return model;
    }

    public T GetModel<T>() where T: BaseModel
    {
        return model as T;
    }

    public BaseModel GetControllerModel(int controllerKey) 
    {
        // get other controller's model
        return GameApp.ControllerManager.GetControllerModel(controllerKey);
    }


    // Message Operations --------------------------
    public void RegisterFunc(string eventName, Action<object[]> callback)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName] += callback;
        }
        else 
        {
            message.Add(eventName, callback);
        }
    }

    public void UnregisterFunc(string eventName, Action<object[]> callback)
    {
        if (message.ContainsKey(eventName))
        {
            message.Remove(eventName);
        }
    }

    public void ApplyFunc(string eventName, params object[] args)
    {
        // apply message of this Module
        if (message.ContainsKey(eventName))
        {
            message[eventName].Invoke(args);  // ?= message[eventName](args);
        }
        else 
        {
            Debug.LogError($"Controller Error: No handler found for event: {eventName}");
        }
    }

    public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
    {
        // apply message of other Controller
        GameApp.ControllerManager.ApplyFunc(controllerKey, eventName, args);
    }

    // Module Func --------------------------
    public virtual void InitModuleEvent() {}
    public virtual void RemoveModuleEvent() {}
    public virtual void InitGlobalEvent() {}
    public virtual void RemoveGlobalEvent() {}

}
