using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStarPoint {
    public int RowIndex;
    public int ColIndex;
    public int G; // from begin to current
    public int H; // from current to end
    public int F; // F = H + G
    public AStarPoint Parent; 

    public AStarPoint(int rowIndex, int colIndex) {
        RowIndex = rowIndex;
        ColIndex = colIndex;
        Parent = null; 
    }

    public AStarPoint(int row, int col, AStarPoint parent) {
        RowIndex = row;
        ColIndex = col;
        Parent = parent;
    }

    public int GetG() {
        int _g = 0;
        AStarPoint parent = Parent;
        while (parent != null) {
            _g = _g + 1;
            parent = parent.Parent;
        }
        return _g;
    }

    public int GetH(AStarPoint end) {
        return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColIndex - end.ColIndex);
    }
}

public class AStar {
    private int rowCount;
    private int colCount;
    private List<AStarPoint> open;  // point to check
    private Dictionary<string, AStarPoint> close;  // closed points

    private AStarPoint start;
    private AStarPoint end;

    public AStar(int rowCount, int colCount) {
        this.rowCount = rowCount; 
        this.colCount = colCount;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();
    }

    public AStarPoint IsInOpen(int row, int col) {
        for (int i = 0; i < open.Count; i++) {
            if (open[i].RowIndex == row && open[i].ColIndex == col) {
                return open[i];
            }
        }
        return null;
    }

    public bool IsInClose(int row, int col) {
        if (close.ContainsKey($"{row}_{col}")) {
            return true;
        }
        return false;
    }

    // into
    public bool FindPath(AStarPoint start, AStarPoint end, Action<List<AStarPoint>> callback) {
        this.start = start;
        this.end = end;
        open = new List<AStarPoint>();
        close = new Dictionary<string, AStarPoint>();

        open.Add(start);
        while (true) {
            if (open.Count == 0) {return false;}
            AStarPoint current = open[0];

            open.Remove(current);
            close.Add($"{current.RowIndex}_{current.ColIndex}", current);
            AddAroundInOpen(current);

            // check the target
            AStarPoint endPoint = IsInOpen(end.RowIndex, end.ColIndex);
            if (endPoint != null) {
                callback(GetPath(endPoint));
                return true;
            }
            open.Sort(OpenSort);
        }
    }

    public int OpenSort(AStarPoint a, AStarPoint b) {
        return a.F - b.F;
    }

    public void AddAroundInOpen(AStarPoint current) {
        (int r, int c)[] direction = {(1, 0), (-1, 0), (0, 1), (0, -1)};
        foreach (var d in direction) {
            int newR = current.RowIndex + d.r;
            int newC = current.ColIndex + d.c;
            if (newR < 0 || newR >= rowCount || newC < 0 || newC >= colCount) {
                continue;
            }
            AddOpen(current, newR, newC);
        }
    }

    public void AddOpen(AStarPoint current, int row, int col) {
        if (IsInClose(row, col) == false && IsInOpen(row, col) == null && GameApp.MapManager.GetBlockType(row, col) != BlockType.Obstacle) {
            AStarPoint newPoint = new AStarPoint(row, col, current);
            newPoint.G = newPoint.GetG();
            newPoint.H = newPoint.GetH(end);
            newPoint.F = newPoint.G + newPoint.H;
            open.Add(newPoint);
        }
    }

    public List<AStarPoint> GetPath(AStarPoint point) {
        List<AStarPoint> paths = new List<AStarPoint>();
        paths.Add(point);
        AStarPoint parent = point.Parent;
        while (parent != null) {
            paths.Add(parent);
            parent = parent.Parent;
        }

        paths.Reverse();
        return paths;
    }

}