using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for base class of VIEW
/// </summary>
public interface IBaseView 
{
    int ViewId {get; set; }
    BaseController Controller {get; set; }


    // Initialization --------------------------
    void InitUI(); // initialize the UI

    void InitData(); // initialize the data


    // Check status --------------------------
    bool IsInit(); // true if initialization

    bool IsShow(); // true if the view is visible


    // Operations --------------------------
    void Open(params object[] args); // open the view

    void Close(params object[] args); // close the view

    void DestoryView(); // destroy the view

    void SetVisible(bool visible); // set if the view is visible


    // Controller Operation --------------------------
    void ApplyFunc(string eventName, params object[] args); // apply a controller's function
    
    void ApplyControllerFunction(int contorllerKey, string eventName, params object[] args); //apply a other controller's function
}
