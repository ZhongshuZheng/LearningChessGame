using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// some common tools
/// </summary>
public static class Tools {


    // this funciont is called "C# extension method" that add an additional function into an encapsulated class
    public static void SetIcon(this Image img, string res) {
        img.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }

    // check if any 2D object under the MousePointer
    public static void ScreenPointToRay2D(Camera cam, Action<Collider2D> callBack) {
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapCircle(pos, 0.02f);
        callBack?.Invoke(col);
    }

}
