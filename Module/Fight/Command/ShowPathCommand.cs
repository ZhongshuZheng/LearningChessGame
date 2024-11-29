using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Command for showing the path
/// 
/// i dont think this funciton should be called as a command
/// </summary>
public class ShowPathCommand : BaseCommand {

    Collider2D preBlock;
    List<AStarPoint> prePath;
    AStar astar;

    AStarPoint start; 


    public ShowPathCommand(ModelBase model) : base(model) {
        preBlock = null;
        prePath = new List<AStarPoint>();
        astar = new AStar(GameApp.MapManager.rowCount, GameApp.MapManager.colCount);
        start = new AStarPoint(model.RowIndex, model.ColIndex);
    }

    public override bool Update(float dt) {

        if (Input.GetMouseButtonDown(0)) {
            // click the left mouse and going on the movement
            GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);
            ClearPreDirections();


            return true;
        }
        Collider2D col = Tools.ScreenPointToRay2D(Camera.main);
        if (col != null && col != preBlock) {
            preBlock = col;
            Block pointTo = col.GetComponent<Block>();

            ClearPreDirections();
            if (pointTo != null) {
                AStarPoint end = new AStarPoint(pointTo.RowIndex, pointTo.ColIndex);
                astar.FindPath(start, end, UpdatePath);
            }
        }

        return false;
    }

    private void UpdatePath(List<AStarPoint> path) {
        if (path.Count < 2 || path.Count - 1 > model.Step) {
            return;
        }
        for (int i = 1; i < path.Count; i++) {
            BlockDirection dir;
            if (i == path.Count - 1) {
                dir = GameApp.MapManager.ComputeBlockDirection(null, path[i - 1], path[i]);
            } else {
                dir = GameApp.MapManager.ComputeBlockDirection(path[i - 1], path[i], path[i + 1]);
            }
            GameApp.MapManager.SetBlockDir(path[i].RowIndex, path[i].ColIndex, dir, Color.yellow);
        }
        prePath = path;
    }

    private void ClearPreDirections() {
        // clear the old path
        foreach (var point in prePath) {
            GameApp.MapManager.mapArr[point.RowIndex, point.ColIndex].SetDirSp(null, Color.white);
        }
        prePath.Clear();
    }

}
