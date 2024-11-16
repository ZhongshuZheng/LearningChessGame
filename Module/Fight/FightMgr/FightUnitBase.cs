using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// State Machine Pattern for Fight State. unit state base class
/// </summary>
public class FightUnitBase {

    public virtual void Init() {

    }

    public virtual bool Update(float dt) {
        return false;
    }
}
