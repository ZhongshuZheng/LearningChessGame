using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


/// <summary>
/// BGM & Sound Manager
/// </summary>
public class SoundManager
{

    private AudioSource bgmSource;
    private Dictionary<string, AudioClip> clips; // cache for audio clips

    private bool isStop;
    public bool IsStop
    {
        get
        {
            return isStop;
        }
        set
        {
            isStop = value;
            if (isStop == true)
            {
                bgmSource.Pause();
            } 
            else 
            {
                bgmSource.Play();
            }
        }
    }

    private float bgmVolume;
    public float BgmVolume
    {
        get
        {
            return bgmVolume;
        }
        set 
        {
            bgmVolume = value;
            bgmSource.volume = value;
        }
    }

    private float effectVolume;
    public float EffectVolume
    {
        get
        {
            return effectVolume;
        }
        set
        {
            effectVolume = value;
        }
    }

    public SoundManager() 
    {
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        clips = new Dictionary<string, AudioClip>();

        isStop = false;
        bgmVolume = 0.5f;
        effectVolume = 0.5f;
    }

    public void playBGM(string res) 
    {
        if(isStop)
        {
            return;
        }
        if (!clips.ContainsKey(res)) {
            clips.Add(res, Resources.Load<AudioClip>($"Sounds/{res}")); 
        }
        bgmSource.clip = clips[res];
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

}
