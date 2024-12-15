using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public enum BlockDirection {
    none = -1,
    down,
    horizontal,
    left,
    left_down,
    left_up,
    right,
    right_down,
    right_up,
    up,
    vertical,
    max
}

/// <summary>
/// Mamager for the tilemap
/// </summary>
public class MapManager {

    private Tilemap tilemap;
    public Block[,] mapArr;

    public int rowCount;
    public int colCount;

    public List<Sprite> dirSpArr;  // list for the target direction

    public void Init() {
        tilemap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();

        rowCount = 12;
        colCount = 20;

        mapArr = new Block[rowCount, colCount];
        dirSpArr = new List<Sprite>();
        for (int i = 0; i < (int)BlockDirection.max; i++) {
            dirSpArr.Add(Resources.Load<Sprite>($"Icon/{(BlockDirection)i}"));
        }
        


        // Scan the tilemap and generate the Block
        // 1. Scan the tilemap position and collect
        List<Vector3Int> tempPosArr = new List<Vector3Int>();  // temporary array for the tile positions
        foreach (var pos in tilemap.cellBounds.allPositionsWithin) {
            if (tilemap.HasTile(pos)) {
                tempPosArr.Add(pos);
            }
        }

        // 2. Generate Block prefab, add script and set position
        Object prefabObj = Resources.Load("Model/block");
        for (int i = 0; i < tempPosArr.Count; i++) { 
            int row = i / colCount;
            int col = i % colCount;
            Block iBlock = (Object.Instantiate(prefabObj) as GameObject).AddComponent<Block>();
            iBlock.RowIndex = row;
            iBlock.ColIndex = col;
            iBlock.transform.position = tilemap.CellToWorld(tempPosArr[i]) + new Vector3(0.5f, 0.5f, 0);

            // 3. collect into Array
            mapArr[row, col] = iBlock;
        }

    }

    public Vector3 GetBlockPosition(int row, int col) {
        return mapArr[row, col].transform.position;
    }

    public BlockType GetBlockType(int row, int col) {
        if (row < rowCount && col < colCount) {
            return mapArr[row, col].Type;
        }
        Debug.LogError($"MapManager: GetBlockType out of range, from {row},{col} in {rowCount},{colCount}");
        return BlockType.Obstacle;
    }

    public void SetBlockType(int row, int col, BlockType type) {
        if (row < rowCount && col < colCount) {
            mapArr[row, col].Type = type;
            return;
        }
        Debug.LogError("MapManager: SetBlockType out of range, from {row},{col} in {rowCount},{colCount}");
    }


    // Message ----------------------------------------------------------------
    public void ShowStepGrid(ModelBase unit) {
        _BFS bfs = new _BFS(rowCount, colCount);
        List<_BFS.Point> points = bfs.Search(unit.RowIndex, unit.ColIndex, unit.Step);
        foreach (var point in points) {
            mapArr[point.RowIndex, point.ColumnIndex].ShowGrid(Color.blue);
        }
    }

    public void HideStepGrid(ModelBase unit) {
        // it is not a good implementation
        _BFS bfs = new _BFS(rowCount, colCount);
        List<_BFS.Point> points = bfs.Search(unit.RowIndex, unit.ColIndex, unit.Step);
        foreach (var point in points) {
            mapArr[point.RowIndex, point.ColumnIndex].HideGrid();
        }
    }

    public void SetBlockDir(int row, int col, BlockDirection dir, Color color) {
        mapArr[row, col].SetDirSp(dirSpArr[(int)dir], color);
    }

    public BlockDirection ComputeBlockDirection(AStarPoint start, AStarPoint cur, AStarPoint end) {
        // from begin to next 
        if (end == null) {
            int row_offset = cur.RowIndex - start.RowIndex;
            int col_offset = cur.ColIndex - start.ColIndex;
            if (row_offset == 0) {
                return BlockDirection.horizontal;
            } else if (col_offset == 0) {
                return BlockDirection.vertical;
            }
            return BlockDirection.none;
        }
        // from current to end
        else if (start == null) {
            int row_offset = end.RowIndex - cur.RowIndex;
            int col_offset = end.ColIndex - cur.ColIndex;
            if (col_offset > 0) {
                return BlockDirection.right;
            } else if (col_offset < 0) {
                return BlockDirection.left;
            } else if (row_offset > 0) {
                return BlockDirection.up;
            } else if (row_offset < 0) {
                return BlockDirection.down;
            } else {
                return BlockDirection.none;
            }
        }
        // from pre to current to next
        else {
            // when compute this direction, it is same for the current that it comes from start to end or from end to start
            // you can draw it on the paper to check
            int row_offset1 = cur.RowIndex - start.RowIndex;
            int col_offset1 = cur.ColIndex - start.ColIndex;
            int row_offset2 = cur.RowIndex - end.RowIndex;
            int col_offset2 = cur.ColIndex - end.ColIndex;

            int row_sum = row_offset1 + row_offset2;
            int col_sum = col_offset1 + col_offset2;
            (int r, int c) dir = (row_sum, col_sum);
            
            if (dir == (1, 1)) {
                // means that the current point is at the (1,1) position of the pattern
                return BlockDirection.left_down;
            } else if (dir == (1, -1)) {
                return BlockDirection.right_down;
            } else if (dir == (-1, 1)) {
                return BlockDirection.left_up;
            } else if (dir == (-1, -1)) {
                return BlockDirection.right_up;
            } else if (row_offset1 == 0 && row_offset2 == 0) {
                return BlockDirection.horizontal;
            } else if (col_offset1 ==  0 && col_offset2 == 0) {
                return BlockDirection.vertical;
            }

            return BlockDirection.none;
        }
    }


    // Skill range functions ----------------------------------------------------------------
    public void ShowSkillRange(ModelBase model, int attackRange, Color color) {
        // collect the grid in a square, and every one if it's distance is under the given range
        int minRow = model.RowIndex - attackRange > 0 ? model.RowIndex - attackRange : 0;
        int maxRow = model.RowIndex + attackRange < rowCount - 1 ? model.RowIndex + attackRange : rowCount - 1;
        int minCol = model.ColIndex - attackRange > 0 ? model.ColIndex - attackRange : 0;
        int maxCol = model.ColIndex + attackRange < colCount - 1 ? model.ColIndex + attackRange : colCount - 1;

        for (int r = minRow; r <= maxRow; r++) {
            for (int c = minCol; c <= maxCol; c++) {
                if (Mathf.Abs(model.RowIndex - r) + Mathf.Abs(model.ColIndex - c) <= attackRange) {
                    mapArr[r, c].ShowGrid(color);
                }
            }
        }
    }

    public void HideSkillRange(ModelBase model, int attackRange) {
        // collect the grid in a square, and every one if it's distance is under the given range
        int minRow = model.RowIndex - attackRange > 0 ? model.RowIndex - attackRange : 0;
        int maxRow = model.RowIndex + attackRange < rowCount - 1 ? model.RowIndex + attackRange : rowCount - 1;
        int minCol = model.ColIndex - attackRange > 0 ? model.ColIndex - attackRange : 0;
        int maxCol = model.ColIndex + attackRange < colCount - 1 ? model.ColIndex + attackRange : colCount - 1;

        for (int r = minRow; r <= maxRow; r++) {
            for (int c = minCol; c <= maxCol; c++) {
                if (Mathf.Abs(model.RowIndex - r) + Mathf.Abs(model.ColIndex - c) <= attackRange) {
                    mapArr[r, c].HideGrid();
                }
            }
        }
    }


}
