using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The game class which inherit from MonoBehaviour, need to be mounted by a game-object
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
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }
}
