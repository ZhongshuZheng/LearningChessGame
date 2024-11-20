using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// A view class for the drag-out hero item
/// </summary>
public class DragHeroView : BaseView {

    private void Update() {
        if (_canvas.enabled == false) return;  

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(transform.position);
        // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }

    public override void Open(params object[] args) {
        base.Open(args); 
        gameObject.GetComponent<Image>().SetIcon((string)args[0]);
        // gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icon/{(string)args[0]}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
