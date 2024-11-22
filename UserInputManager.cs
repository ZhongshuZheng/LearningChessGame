using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Manager for user input, such as mouse clicks and keyboard interactions
/// </summary>
public class UserInputManager {

    public void Update() {
        if (Input.GetMouseButton(0)) {

            if (EventSystem.current.IsPointerOverGameObject()) {
                // click some UI

            } else {
                Tools.ScreenPointToRay2D(Camera.main, (Collider2D col) => {
                    if (col != null) {
                        GameApp.MsgCenter.PostEvent(col.gameObject, Defines.OnSelectEvent);
                    } else {
                        GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);
                    }
                });
            }

        }
        
    }
}
