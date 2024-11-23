using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CanEditMultipleObjects, CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("SetPositionHere")) {
            Tilemap tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
            Enemy enemy = target as Enemy;

            // 1. set the unit's position into the cell position
            Vector3Int cellPos = tileMap.WorldToCell(enemy.transform.position);
            enemy.transform.position = tileMap.CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, -1);


            // 2. turn the cell position index into the Block index
            // we need to make a turn because that the cell position in tilemap is not from (0,0), but something different
            // for example, in level1, the leftButton, as the first cell in tilemap, pos is (-10, -6) 
            // but we need to record it as (0, 0)
            var allPosition = tileMap.cellBounds.allPositionsWithin;
            int min_x = 0; 
            int min_y = 0;

            if (allPosition.MoveNext()) {   // find the first cell's position in the tilemap if exists
                Vector3Int current = allPosition.Current;
                min_x = current.x;
                min_y = current.y;
            }

            enemy.RowIndex = Mathf.Abs(cellPos.y - min_y);
            enemy.ColIndex = Mathf.Abs(cellPos.x - min_x);
        }
    }

}


