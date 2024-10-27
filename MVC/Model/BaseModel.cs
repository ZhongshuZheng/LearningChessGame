using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for MODEL
/// </summary>
public class BaseModel 
{
    public BaseController Controller; 

    public BaseModel(BaseController controller)
    {
        Controller = controller;
    }

    public void Init() 
    {

    }
}
