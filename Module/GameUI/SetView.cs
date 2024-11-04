using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// set menu view
/// </summary>
public class SetView : BaseView
{

    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bg/closeBtn").onClick.AddListener(onCloseBtn);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Button functions ----------------------------------------------------------------
    private void onCloseBtn() 
    {
        GameApp.ViewManager.Close(ViewId);
    }

}
