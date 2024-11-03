using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game App Structure to control all the modules
/// 
/// this class will persist during all the Game Runtime
/// so it will just contain the *must be loaded* modules
/// as for the things that only need in one scene, will be loaded by the MonoBehaviour class like GameScene
/// </summary>
public class GameApp : Singleton<GameApp>
{

    public static SoundManager SoundManager;
    public static ControllerManager ControllerManager;
    public static ViewManager ViewManager;

    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
    }

    public override void Update(float dt)
    {
        // Debug.Log(dt);
    }
}
