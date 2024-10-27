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

    public SoundManager() 
    {
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        clips = new Dictionary<string, AudioClip>();
    }

    public void playBGM(string res) 
    {
        if (!clips.ContainsKey(res)) {
            clips.Add(res, Resources.Load<AudioClip>($"Sounds/{res}")); 
        }
        bgmSource.clip = clips[res];
        bgmSource.Play();
    }

}
