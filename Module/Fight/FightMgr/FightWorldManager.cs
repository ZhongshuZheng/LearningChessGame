using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {
    Idle,
    Enter,
}

/// <summary>
/// Manager for all the unit in the fight ground, including the hero, ememy, turns and so on.
/// </summary>
public class FightWorldManager {

    public GameState gameState = GameState.Idle;

    public List<Hero> heros;

    private FightUnitBase current;
    public FightUnitBase Current { 
        get { return current; }
    }

    public FightWorldManager() {
        heros = new List<Hero>();
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
            case GameState.Enter:
                _current = new FightEnter();
                break;
        }
        _current.Init();
    }

    public void AddHero(Block b, Dictionary<string, string> heroData) {
        GameObject obj = GameObject.Instantiate(Resources.Load($"Model/{heroData["Model"]}")) as GameObject;
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(heroData, b.RowIndex, b.ColIndex);
        heros.Add(hero);
        
        b.Type = BlockType.Obstacle;
    }

}
