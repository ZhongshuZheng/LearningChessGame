using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class of Movement command
/// </summary>
public class MoveCommand : BaseCommand {

    List<AStarPoint> paths;
    int current_index;

    AStarPoint current;

    int preRow;
    int preColumn;

    public MoveCommand(ModelBase model, List<AStarPoint> paths) : base(model) {
        this.paths = paths;

        current = new AStarPoint(model.RowIndex, model.ColIndex);
        preRow = model.RowIndex;
        preColumn = model.ColIndex;
        current_index = 0;
    }

    public override bool Update(float dt) {
        if (current_index < paths.Count) {
            // move and check if the movement is over
            if (model.Move(paths[current_index].RowIndex, paths[current_index].ColIndex, dt * 5)) {
                current = paths[current_index];
                current_index++;
            }
            model.PlayAnimation("move");
            return false;
        }

        // finished all the paths movement
        model.PlayAnimation("idle");

        // show action options list
        GameApp.ViewManager.Open(ViewTypes.SelectOptionView, model, new Vector2(model.transform.position.x, model.transform.position.y));

        return true;
    }

    public override void Do() {
        GameApp.MapManager.SetBlockType(preRow, preColumn, BlockType.None);
        GameApp.MapManager.SetBlockType(paths[paths.Count - 1].RowIndex, paths[paths.Count - 1].ColIndex, BlockType.Obstacle);
    }

    public override void UnDo() {
        model.RowIndex = preRow;
        model.ColIndex = preColumn;
        Vector3 pos = GameApp.MapManager.GetBlockPosition(preRow, preColumn);
        pos.z = model.transform.position.z;

        model.transform.position = pos;
        GameApp.MapManager.SetBlockType(preRow, preColumn, BlockType.Obstacle);
        GameApp.MapManager.SetBlockType(current.RowIndex, current.ColIndex, BlockType.None);
    }

}
