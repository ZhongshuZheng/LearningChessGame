using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// base class for command
/// </summary>
public class BaseCommand {

    public ModelBase model;  // target of the commandA
    protected bool isFinished; 

    public BaseCommand(ModelBase model) {
        this.model = model;
        isFinished = false;
    }

    public virtual bool Update(float dt) {
        return isFinished;
    }

    public virtual void Do() {

    }

    public virtual void UnDo() {

    }

}
