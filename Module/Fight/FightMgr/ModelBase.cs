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

    private bool _isStop;  // if the action in turn is over
    public bool IsStop {
        get { return _isStop; }
        set {
            stopObj.SetActive(value);
            if (value == true) {
                bodySp.color = Color.gray;
            } else {
                bodySp.color = Color.white;
            }
            _isStop = value;
        }
    }

    private void Awake() {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();
    }

    protected virtual void Start() {
        AddEvents();
    }

    protected virtual void OnDestroy() {
        RemoveEvents();
    }

    private void Update() {}

    protected virtual void AddEvents() {
        GameApp.MsgCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MsgCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    protected virtual void RemoveEvents() {
        GameApp.MsgCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MsgCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }


    // Message CallBacks ----------------------------------------------------------------
    protected virtual void OnSelectCallBack(object arg) {
        GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);  // tell other units to be unselected
        GameApp.MapManager.ShowStepGrid(this);
    }

    protected virtual void OnUnSelectCallBack(object arg) {
        GameApp.MapManager.HideStepGrid(this);
    }

    // Animations and Actions ----------------------------------------------------------------
    public void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public virtual bool Move(int rowIndex, int columnIndex, float dt) {
        Vector3 pos = GameApp.MapManager.GetBlockPosition(rowIndex, columnIndex);
        if (pos == null) {return true;}

        pos.z = transform.position.z;

        // turn round
        if ((pos.x > transform.position.x && transform.localScale.x < 0) || (pos.x < transform.position.x && transform.localScale.x > 0) ) {
            Flip();
        }

        // too close to target, return true as finished the movement
        if (Vector3.Distance(pos, transform.position) < 0.02f) {
            RowIndex = rowIndex;
            ColIndex = columnIndex;
            transform.position = pos;
            return true;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, dt);
        return false;
    }

    public virtual void GotHit(ISkill skill) {

    }

    public void LookAtModel(ModelBase target) {
        if (target.transform.position.x > transform.position.x && transform.localScale.x < 0) {
            Flip();
        } else if (target.transform.position.x < transform.position.x && transform.localScale.x > 0) {
            Flip();
        }
    }

    public void PlayAnimation(string animName) {
        ani.Play(animName);
    }

    public float GetDis(ModelBase target) {
        return Mathf.Abs(RowIndex - target.RowIndex) + Mathf.Abs(ColIndex - target.ColIndex);
    }

    public virtual void PlayEffect(string effectName) {
        GameObject effect = Instantiate(Resources.Load($"Effect/{effectName}")) as GameObject;
        effect.transform.position = transform.position;
    }

    public virtual void PlaySound(string soundName) { 
        GameApp.SoundManager.playEffect(soundName, transform.position);
    }

}
