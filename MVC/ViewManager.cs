using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewInfo
{
    public string PrefabName; // name of the prefab
    public Transform parentTf; // parent transform which the view is attached
    public BaseController controller; // controller which the view is attached
    public int SortingOrder;  // sorting order in the canvas

}


/// <summary>
/// manager to manage all the views
/// </summary>
public class ViewManager 
{
    public Transform canvasTf; // current canvas transform, in this project it is about the Screen
    public Transform worldCanvasTf; // world canvas transform, in this project it seems is about the Map
    private Dictionary<int, IBaseView> _opens; // opened views
    private Dictionary<int, IBaseView> _viewCache; // views cache, if a view has been ever opened, it will be stored in cache
    private Dictionary<int, ViewInfo> _views; // view info dictionary

    public ViewManager()
    {
        canvasTf = GameObject.Find("Canvas").transform;
        worldCanvasTf = GameObject.Find("WorldCanvas").transform;

        _opens = new Dictionary<int, IBaseView>();
        _viewCache = new Dictionary<int, IBaseView>();
        _views = new Dictionary<int, ViewInfo>();
    }


    // view info reigiters ------------------------------------------
    public void Register(int key, ViewInfo viewinfo) 
    {
        if (!_views.ContainsKey(key))
        {
            _views[key] = viewinfo;
        }
    }

    public void Register(ViewTypes viewtype, ViewInfo viewinfo)
    {
        Register((int)viewtype, viewinfo);
    }

    public void UnRegister(int key)
    {
        if (_views.ContainsKey(key))
        {
            _views.Remove(key);
        }
    }


    // view operations ------------------------------------
    public bool IsOpen(int key)
    {
        // check if a view is open
        return _opens.ContainsKey(key);
    }

    // Get
    public IBaseView GetView(int key) 
    {
        if (_opens.ContainsKey(key))
        {
            return _opens[key];
        }
        else if (_viewCache.ContainsKey(key))
        {
            return _viewCache[key];
        }
        return null;
    }

    public T GetView<T>(int key) where T : class, IBaseView
    {
        IBaseView view = GetView(key);
        if (view != null)
        {
            return GetView(key) as T;
        }
        return null;
    }

    // remove
    public void RemoveView(int key)
    {
        _views.Remove(key);
        _opens.Remove(key);
        _viewCache.Remove(key);
    }

    public void RemoveViewByController(BaseController ctl)
    {
        foreach (var item in _views) 
        {
            if (item.Value.controller == ctl)
            {
                RemoveView(item.Key);
            }
        }
    }

    // Open
    public void Open(int key, params object[] args)
    {
        IBaseView view = GetView(key);
        ViewInfo viewInfo = _views[key];

        // if do not be opened, load resource and attach a view-script
        if (view == null)
        {
            // 1. Load teh prefab as game-object
            GameObject uiObj = UnityEngine.Object.Instantiate(Resources.Load($"View/{viewInfo.PrefabName}"), viewInfo.parentTf) as GameObject;

            // 2. Add some necessary components as a view
            Canvas canvas = uiObj.GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = uiObj.AddComponent<Canvas>();
            }
            if (uiObj.GetComponent<GraphicRaycaster>() == null)
            {
                uiObj.AddComponent<GraphicRaycaster>();
            }
            canvas.overrideSorting = true;   // can be reset the canvas layer
            canvas.sortingOrder = viewInfo.SortingOrder;

            // 3. Add scirpt into the game-object
            string view_type = ((ViewTypes)key).ToString();  // get name from ViewType Enum
            view = uiObj.AddComponent(Type.GetType(view_type)) as IBaseView;
            view.ViewId = key;
            view.Controller = viewInfo.controller;

            // 4. Onload the view
            _viewCache.Add(key, view);
            viewInfo.controller.OnLoadView(view);
        }

        // if opened already, return
        if (IsOpen(key)) 
        {
            return;
        }

        // 5. initialize and open the view
        _opens.Add(key, view);
        if (!view.IsInit())
        {
            view.InitData();
            view.InitUI();
        }
        view.SetVisible(true);
        view.Open(args);
        viewInfo.controller.OpenView(view);
    }

    public void Open(ViewTypes viewtype, params object[] args)
    {
        Open((int)viewtype, args);
    }

    // Close
    public void Close(int key, params object[] args)
    {
        if (IsOpen(key))
        {
            IBaseView view = GetView(key);
            if (view != null)
            {
                view.Close(args);
                _opens.Remove(key);
                _views[key].controller.CloseView(view);
            }
        }
    }

    // Destory
    public void Destroy(int key)
    {
        IBaseView view = GetView(key);
        if (view != null)
        {
            UnRegister(key);
            view.DestoryView();
            _viewCache.Remove(key);

        }
    }

}
