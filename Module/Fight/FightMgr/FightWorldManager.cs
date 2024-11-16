using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {
    Idle,
}

/// <summary>
/// Manager for all the unit in the fight ground, including the hero, ememy, turns and so on.
/// </summary>
public class FightWorldManager {

    public GameState gameState = GameState.Idle;

    private FightUnitBase current;
    public FightUnitBase Current { 
        get { return current; }
    }

    public FightWorldManager() {
        ChangeState(GameState.Idle);
    }

    public void Update(float dt) {
        if (current != null && current.Update(dt) != false) {
            // do sth.
        } else {
            current = null;
        }
    }

    public void ChangeState(GameState state) {
        gameState = state;
        FightUnitBase _current = current; // what is this?
        switch (state) {
            case GameState.Idle:
                _current = new FightIdle();
                break;
        }
        _current.Init();
    }

}
