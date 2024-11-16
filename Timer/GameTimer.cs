using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Timer controller 
/// </summary>
public class GameTimer {
    private List<GameTimerData> timers;

    public GameTimer() {
        timers = new List<GameTimerData>();
    }

    public void RegisterTimer(float time, Action callback) {
        timers.Add(new GameTimerData(time, callback));
    }

    public void Update(float dt) {
        for (int i = 0; i < timers.Count; i++) {
            if (timers[i].Update(dt) == true) {
                timers.RemoveAt(i);
            }
        }
    }

    public void Break() {
        timers.Clear();
    }

    public int Count() {
        return timers.Count;
    }

}
