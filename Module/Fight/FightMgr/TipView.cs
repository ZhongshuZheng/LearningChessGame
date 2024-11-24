using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Tip view in the top of the battle screen
/// </summary>
public class TipView : BaseView {

    public override void Open(params object[] args) {
        Find<Text>("content/txt").text = (string)args[0];

        // using DOTeen appendix to define the animation 
        Sequence seq = DOTween.Sequence();
        GameObject tip = Find("content").gameObject;
        seq.Append(tip.transform.DOScale(new Vector2(1, 1), 0.35f).SetEase(Ease.OutBack));  // show
        seq.AppendInterval(0.75f); 
        seq.Append(tip.transform.DOScale(new Vector2(1, 0), 0.15f).SetEase(Ease.Linear));   // hide
        seq.AppendCallback(() => {
            GameApp.ViewManager.Close(ViewId);
        });


    }
}
