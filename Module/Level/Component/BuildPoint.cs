using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Trigger for the level select position
/// </summary>
public class BuildPoint : MonoBehaviour
{
    public int LevelId; 

    private void OnTriggerEnter2D(Collider2D collision) {
        GameApp.MsgCenter.PostEvent(Defines.showLevelDesEvent, LevelId);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        GameApp.MsgCenter.PostEvent(Defines.hideLevelDesEvent);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
