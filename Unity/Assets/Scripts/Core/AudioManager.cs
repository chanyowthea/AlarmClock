using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] AudioSource _Source;
    public string _FullPath { private set; get; }

    protected override void Awake()
    {
        base.Awake();
        _FullPath = Application.streamingAssetsPath + "/";
    }

    public void PlayClip(string clipPath)
    {
        if (clipPath == null)
        {
            return;
        }
#if UNITY_ANDROID && !UNITY_EDITOR
        clipPath = "jar:file:///" + Application.dataPath + "!/assets/" + clipPath;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        clipPath = "file://" + Application.dataPath + "/StreamingAssets/" + clipPath;
#endif
        Debug.Log("PlayClip path=" + clipPath);
        if (_Source.isPlaying)
        {
            _Source.Stop();
        }
        CoroutineUtil.instance.StartCoroutine(Download(clipPath));
    }

    IEnumerator Download(string clipPath)
    {
        WWW www = new WWW(clipPath);
        yield return www;
        var clip = www.GetAudioClip();
        if (clip != null)
        {
            _Source.clip = clip;
            _Source.Play();
        }
    }

    public void StopClip()
    {
        if (_Source.isPlaying)
        {
            _Source.Stop();
        }
    }
    
    public void StartPlay()
    {
        try
        {
            AndroidJavaObject activity;
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("PlayAudio", "Candy");
        }
        catch (Exception ex)
        {
            Debug.LogError("StartPlay=" + ex.Message);
        }
    }

    public void StopPlay()
    {
        try
        {
            AndroidJavaObject activity;
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("StopAudio");
        }
        catch (Exception ex)
        {
            Debug.LogError("StopPlay=" + ex.Message);
        }
    }
}
