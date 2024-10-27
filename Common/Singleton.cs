using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

/// <summary>
/// Singleton base class
/// </summary>
public class Singleton<T>
{
    private static readonly T instance = Activator.CreateInstance<T>();

    public static T Instance
    {
        get 
        {
            return instance;
        }
    }

    // initialization
    public virtual void Init() 
    {

    }

    // update per frame
    public virtual void Update(float dt) 
    {

    }

    // release memory
    public virtual void Destory() 
    {

    }
}
