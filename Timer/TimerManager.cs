using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Timer manager to manage timer
/// 
/// Strange... why we need this manager instead of the original GameTimer?
/// </summary>
public class TimerManager {
    GameTimer timer;

    public TimerManager() {
        timer = new GameTimer();
    }

    public void Register(float t, Action cb) {
        timer.RegisterTimer(t, cb);
    }

    public void Update(float dt) {
        timer.Update(dt);
    }
}
