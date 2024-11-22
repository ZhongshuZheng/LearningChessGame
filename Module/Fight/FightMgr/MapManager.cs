using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


/// <summary>
/// Mamager for the tilemap
/// </summary>
public class MapManager {

    private Tilemap tilemap;
    public Block[,] mapArr;

    private int rowCount;
    private int colCount;

    public void Init() {
        tilemap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();

        rowCount = 12;
        colCount = 20;

        mapArr = new Block[rowCount, colCount];


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

    public BlockType GetBlockType(int row, int col) {
        if (row < rowCount && col < colCount) {
            return mapArr[row, col].Type;
        }
        Debug.LogError("MapManager: GetBlockType out of range");
        return BlockType.Obstacle;
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


}
