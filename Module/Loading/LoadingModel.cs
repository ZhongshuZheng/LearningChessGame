using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// model for Loading 
/// </summary>
public class LoadingModel : BaseModel
{
    public String SceneName;
    public Action callback;  // may be called after loading the scene

}
