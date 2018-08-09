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
        _FullPath = Application.streamingAssetsPath + "";
    }

    public void PlayClip(string clipPath)
    {
        if (clipPath == null)
        {
            return;
        }
        //clipPath = clipPath.Remove(0, clipPath.LastIndexOf('/') + 1);
        //clipPath = clipPath.Substring(0, clipPath.LastIndexOf('.'));
        Debug.Log("PlayClip path=" + clipPath);
        if (_Source.isPlaying)
        {
            _Source.Stop();
        }
        CoroutineUtil.instance.StartCoroutine(Download("file://" + clipPath)); 
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

    public void PlayClip1(string clipPath)
    {
        //if (_Source.isPlaying)
        //{
        //    _Source.Stop();
        //}
        //var clip = Resources.Load<AudioClip>(clipPath);
        //Debug.Log("PlayClip1 clip=" + clip); 
        //if (clip != null)
        //{
        //    _Source.clip = clip;
        //    _Source.Play();
        //}
    }

    public void StopClip()
    {
        if (_Source.isPlaying)
        {
            _Source.Stop();
        }
    }
}
