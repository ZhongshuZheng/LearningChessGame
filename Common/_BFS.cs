using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class _BFS {

    public class Point {
        public int RowIndex;
        public int ColumnIndex;
        public Point Father;

        public Point(int row, int col) {
            RowIndex = row;
            ColumnIndex = col;
        }
        public Point(int row, int col, Point f) {
            RowIndex = row;
            ColumnIndex = col;
            Father = f;
        }
    }

    public int RowCount; 
    public int ColumnCount;

    public Dictionary<string, Point> Finds;  // save the point founded

    public _BFS(int row, int col) {
        RowCount = row;
        ColumnCount = col;
        Finds = new Dictionary<string, Point>();
    }

    /// <summary>
    /// BFS search the possible locations under steps.
    /// </summary>
    /// <param name="row">start point row</param>
    /// <param name="col">start point col</param>
    /// <param name="step">just the step</param>
    /// <returns></returns>
    public List<Point> Search(int row, int col, int step) {

        Point startPoint = new Point(row, col, null);

        List<Point> searching = new List<Point>() {startPoint};        

        for (int s = 0; s < step; s++) {

            List<Point> nextStep = new List<Point>();
            foreach (Point i in searching) {
                _checkArround(nextStep, i.RowIndex, i.ColumnIndex, i);
            }

            if (nextStep.Count == 0) {break;}

            searching.Clear();
            searching.AddRange(nextStep);
        }
        return Finds.Values.ToList();
    }


    // go around the point
    private void _checkArround(List<Point> nextSp, int rowIndex, int columnIndex, Point p) {
        (int r, int c)[] directions = {(1, 0), (0, -1), (-1, 0), (0, 1)};
        foreach (var d in directions) {
            int newR = rowIndex + d.r;
            int newC = columnIndex + d.c;
            if (newR < 0 || newR >= RowCount || newC < 0 || newC >= ColumnCount) {
                continue;
            }
            if (!Finds.ContainsKey($"{newR}_{newC}") && GameApp.MapManager.GetBlockType(newR, newC) != BlockType.Obstacle) {
                Point newP = new Point(newR, newC, p);
                nextSp.Add(newP);
                Finds.Add($"{newR}_{newC}", newP);
            }
        }

    }


}
