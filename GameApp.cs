using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game App Structure to control all the modules
/// </summary>
public class GameApp : Singleton<GameApp>
{

    public static SoundManager SoundManager;
    public static ControllerManager ControllerManager;

    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
    }

    public override void Update(float dt)
    {
        // Debug.Log(dt);
    }
}
