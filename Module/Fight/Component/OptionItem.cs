using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// The little view for the player-action options
/// 
/// I think it is a kind of view
/// </summary>
public class OptionItem : MonoBehaviour {

    OptionData op_data;

    public void Init(OptionData data) {
        op_data = data;
        transform.Find("txt").GetComponent<Text>().text = data.Name;
    }

    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            GameApp.MsgCenter.PostEvent(op_data.EventName);
            GameApp.ViewManager.Close((int)ViewTypes.SelectOptionView);
        });
    }

}
