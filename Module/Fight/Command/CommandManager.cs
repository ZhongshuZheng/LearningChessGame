using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Command manager
/// </summary>
public class CommandManager {

    private Queue<BaseCommand> willDoCommandsQueue;
    private Stack<BaseCommand> unDoStack;
    private BaseCommand currentCommand;

    public CommandManager() {
        willDoCommandsQueue = new Queue<BaseCommand>();
        unDoStack = new Stack<BaseCommand>();
    }

    public bool isRunningCommand { get {return currentCommand != null; }}

    public void AddCommand(BaseCommand command) {
        willDoCommandsQueue.Enqueue(command);
        unDoStack.Push(command);
    }

    public void Update(float dt) {
        if (currentCommand != null) {
            if (currentCommand.Update(dt)) {
                currentCommand = null;
            }
        } 
        if (currentCommand == null && willDoCommandsQueue.Count > 0) {
            currentCommand = willDoCommandsQueue.Dequeue(); 
            currentCommand.Do();
        }
    }

    public void Clear() {
        willDoCommandsQueue.Clear();
        unDoStack.Clear();
        currentCommand = null;
    }

    public void UnDo() {
        if (unDoStack.Count > 0) {
            unDoStack.Pop().UnDo();
        }
    }

}
