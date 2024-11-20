using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Base class for Battle Unit
/// 
/// I don't think this class name "ModelBase" is a good name 
/// </summary>
public class ModelBase : MonoBehaviour {

    public int Id;
    public Dictionary<string, string> Data;
    public int Type;
    public int Step;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp;
    public GameObject stopObj;  // the symbol when finished the action
    public Animator ani; 

    private void Awake() {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();
    }

    private void Start() {}
    private void Update() {}

}
