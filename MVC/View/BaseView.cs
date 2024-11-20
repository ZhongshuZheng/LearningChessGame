using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for view
/// 
/// i dont know why a view must be attached to a specific controller for now. I think they could be sepreated
/// 
/// Besides, this *VIEW* in this structure can be regarded as a kind of MonoBehaviour with a useful "Open" and "Find" method. The author use
/// this view-class ot controll the prefabs that can be "open" and used as a UI element. As for the other prefabs, they use MonoBehaviour directly
/// </summary>
public class BaseView : MonoBehaviour, IBaseView
{
    public int ViewId {get; set; }
    public BaseController Controller {get; set; }

    protected Canvas _canvas;
    protected Dictionary<string, GameObject> m_cache_gos = new Dictionary<string, GameObject>();    // Cache for game objects (after be found), record the things in the view

    private bool _isInit = false;


    // MonoBehaviour --------------------------------
    void Awake()
    {
        _canvas = gameObject.GetComponent<Canvas>();
        OnAwake();
    }

    void Start()
    {
        OnStart();
        
    }

    protected virtual void OnAwake() 
    {

    }

    protected virtual void OnStart() 
    {

    } 


    // Initializiation --------------------------------
    public virtual void InitUI()
    {
    }

    public virtual void InitData()
    {
        _isInit = true;
    }


    // Check Statues --------------------------------
    public bool IsInit() 
    {
        return _isInit;
    }

    public bool IsShow() 
    {
        return _canvas.enabled == true;
    }


    // Operations --------------------------------
    // these operations are usually called by ViewManager
    public virtual void Open(params object[] args)
    {
    }

    public virtual void Close(params object[] args)
    {
        SetVisible(false);
    }

    public void SetVisible(bool value)
    {
        _canvas.enabled = value;
    }

    public void DestoryView()
    {
        Controller = null;
        Destroy(gameObject);
    }


    // Controller Funs --------------------------------
    public void ApplyFunc(string eventName, params object[] args)
    {
        Controller.ApplyFunc(eventName, args);
    }

    public void ApplyControllerFunction(int controllerKey, string eventName, params object[] args) 
    {
        Controller.ApplyControllerFunc(controllerKey, eventName, args);
    }


    // GameObject Get Funs --------------------------------
    // used to get things and buttons from the view
    public GameObject Find(string res) 
    {
        if (!m_cache_gos.ContainsKey(res))
        {
            m_cache_gos.Add(res, transform.Find(res).gameObject);
        }
        return m_cache_gos[res];
    }

    public T Find<T>(string res) where T : Component
    {
        GameObject obj = Find(res);
        return obj.GetComponent<T>();
    }

}
