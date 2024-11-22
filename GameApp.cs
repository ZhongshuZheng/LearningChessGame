using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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
    public static ConfigManager ConfigManager;
    public static CameraManager CameraManager;
    public static MessageCenter MsgCenter;
    public static TimerManager TimerManager;
    public static FightWorldManager FightManager;
    public static MapManager MapManager;
    public static GameDataManager GameDataManager;
    public static UserInputManager UserInputManager;

    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
        ConfigManager = new ConfigManager();
        CameraManager = new CameraManager();
        MsgCenter = new MessageCenter();
        TimerManager = new TimerManager(); 
        FightManager = new FightWorldManager();
        MapManager = new MapManager();
        GameDataManager = new GameDataManager();
        UserInputManager = new UserInputManager();
    }

    public override void Update(float dt)
    {
        // Debug.Log(dt);
        TimerManager.Update(dt);
        FightManager.Update(dt);
        UserInputManager.Update();
    }
}
