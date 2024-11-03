using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The game class which inherit from MonoBehaviour, need to be mounted by a game-object
/// 
/// this class is just for *LOGIN* game scene, which regists modules and settings in the GameApp
/// </summary>
public class GameScene : MonoBehaviour
{

    float dt;
    public Texture2D mouseTxt; // mouse pointer

    public void Awake() 
    {
        GameApp.Instance.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);
        GameApp.SoundManager.playBGM("login");

        RegisterModule();
        InitModule();
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }

    // Register the module which is need in the login page
    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerTypes.GameUIController, new GameUIController());
        GameApp.ControllerManager.Register(ControllerTypes.Game, new GameController());

    }

    // Initialize all the controllers in the controller manager
    void InitModule()
    {
        GameApp.ControllerManager.InitAllModules();
    }
}
