using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// some common tools
/// </summary>
public static class Tools {

    public static void SetIcon(this Image img, string res) {
        // this funciont is called "C# extension method" that add an additional function into an encapsulated class
        img.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }

}
