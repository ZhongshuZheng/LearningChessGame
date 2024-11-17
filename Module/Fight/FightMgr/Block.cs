using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BlockType {
    None,
    Obstacle,
}

/// <summary>
/// Block Class
/// 
/// In order to script for the tiles in the tilemap, a kind of "Block" prefab is made to locate each tile.
/// And then this script is attached to the Block prefabs
/// </summary>
public class Block : MonoBehaviour {

    public int RowIndex;
    public int ColIndex;
    public BlockType Type;
    private SpriteRenderer selectSp;
    private SpriteRenderer gridSp;
    private SpriteRenderer dirSp;

    void Awake() {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();
    } 

    private void OnMouseEnter() {
        selectSp.enabled = true;
    }

    private void OnMouseExit() {
        selectSp.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
