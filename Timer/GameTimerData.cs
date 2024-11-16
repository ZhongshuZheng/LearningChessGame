using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// the timer
/// </summary>
public class GameTimerData {
    private float time;
    private Action callback; 

    public GameTimerData(float t, Action cb) {
        time = t;
        callback = cb;
    }

    public bool Update(float dt) {
        time -= dt;
        if (time <= 0) {
            callback.Invoke();
            return true;
        }
        return false;
    }
}
