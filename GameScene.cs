using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The game class which inherit from MonoBehaviour, need to be mounted by a game-object
/// 
/// this class will continue to work all the time
/// why dont i do everything in GameAPP? In fact, i dnot know ...
/// as for now, i think that they are the same
/// </summary>
public class GameScene : MonoBehaviour
{

    float dt;
    public Texture2D mouseTxt; // mouse pointer

    private bool isLoaded = false;

    public void Awake() 
    {
        if (isLoaded == true) {
            // i dont know why to do this. Maybe it is to keep that there is only one GameScene, a kind of singleton with MonoBehaviour
            Destroy(gameObject);
        } else {
            isLoaded = true;
            DontDestroyOnLoad(gameObject);
            GameApp.Instance.Init();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);
        GameApp.SoundManager.playBGM("login");

        RegisterConfigs();  
        GameApp.ConfigManager.LoadAllConfigs();
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
        GameApp.ControllerManager.Register(ControllerTypes.LoadingController, new LoadingController());

    }

    void RegisterConfigs() {
        string[] configFiles = {
            "enemy",
            "level",
            "option",
            "player",
            "role",
            "skill",
        };
        foreach (var file in configFiles) {
            GameApp.ConfigManager.Register(file, new ConfigData(file));
        }
    }

    // Initialize all the controllers in the controller manager
    void InitModule()
    {
        GameApp.ControllerManager.InitAllModules();
    }
}
