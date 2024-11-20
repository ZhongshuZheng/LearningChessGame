using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

/// <summary>
/// One Manager to rule all the controllers
/// 
/// In this Structure, Controllers are used usuall for control/operator with the VIEWs. The Controller do not save any thing.
/// As for the other logic, we use kinds of xxxManager. The manager can save variables.
/// </summary>
public class ControllerManager 
{

    private Dictionary<int, BaseController> _modules;

    public ControllerManager()
    {
        _modules = new Dictionary<int, BaseController>();
    }

    // Controller-Collection Operations --------------------------------
    public void Register(int controllersKey, BaseController controller) 
    {
        if (!_modules.ContainsKey(controllersKey))
        {
            _modules.Add(controllersKey, controller);
        }
    }

    public void Register(ControllerTypes type, BaseController controller)
    {
        Register((int)type, controller);
    }

    public void UnRegister(int controllersKey) 
    {
        if (_modules.ContainsKey(controllersKey))
        {
            _modules.Remove(controllersKey);
        }
    }

    public void InitAllModules() 
    {
        foreach (var item in _modules)
        {
            item.Value.Init();
        }
    }

    public void Clear()
    {
        _modules.Clear();
    }

    public void ClearAllControllers()
    {
        List<int> keys = _modules.Keys.ToList();
        foreach (int key in keys)
        {
            _modules[key].Destory();
            _modules.Remove(key);
        }
    }

    // Module(Controller) Operations ------------------------------ 
    public void ApplyFunc(int controllersKey, string eventName, params object[] args)
    {
        if (_modules.ContainsKey(controllersKey))
        {
            _modules[controllersKey].ApplyFunc(eventName, args);
        }
        else 
        {
            Debug.LogError($"size: {_modules.Count()}");
            Debug.LogError($"ContorllerManager Error: No controller found with key {controllersKey}");
        }
    }

    public BaseModel GetControllerModel(int controllerKey) 
    {
        if (_modules.ContainsKey(controllerKey))
        {
            return _modules[controllerKey].GetModel();
        }
        else
        {
            Debug.LogError($"ContorllerManager Error: No controller found with key {controllerKey}");
            return null;
        }
    }

}
