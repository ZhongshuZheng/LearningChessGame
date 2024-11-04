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
        Find<Toggle>("bg/IsOpnSound").onValueChanged.AddListener(onIsStopBtn);
        Find<Slider>("bg/soundCount").onValueChanged.AddListener(onSliderBgmBtn);
        Find<Slider>("bg/effectCount").onValueChanged.AddListener(onSliderEffectBtn);

        Find<Toggle>("bg/IsOpnSound").isOn = GameApp.SoundManager.IsStop;
        Find<Slider>("bg/soundCount").value = GameApp.SoundManager.BgmVolume;
        Find<Slider>("bg/effectCount").value = GameApp.SoundManager.EffectVolume;
    }


    // Button functions ----------------------------------------------------------------
    private void onCloseBtn() 
    {
        GameApp.ViewManager.Close(ViewId);
    }

    private void onIsStopBtn(bool isStop) 
    {
        GameApp.SoundManager.IsStop = isStop;
    }

    private void onSliderBgmBtn(float value)
    {
        GameApp.SoundManager.BgmVolume = value;
    }

    private void onSliderEffectBtn(float value)
    {
        GameApp.SoundManager.EffectVolume = value;
    }

}
