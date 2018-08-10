using System.Collections;
using System.Collections.Generic;
using System.IO;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

class AudioSelectUI : BaseUI
{
    public static AudioSelectUI _Instance;
    [SerializeField] LoopVerticalScrollRect _Rect;

    // 配置处理
    public AudioSelectUI()
    {
        _NaviData._Layer = EUILayer.FullScreen;
        _NaviData._Type = EUIType.FullScreen;
    }

    public override void Open(NavigationData data = null)
    {
        base.Open(data);
        UIManager.Instance.Open<TopResidentUI>();

        try
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _Rect.SetData("Alarm.mp3");
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
            _Rect.SetData("Alarm.mp3");
            //_Rect.SetData(Recursive(AudioManager.instance._FullPath).ToArray());
#endif
            _Rect.RefillCells();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("AudioSelectUI ex=" + ex.Message);
        }

    }

    internal override void Close()
    {
        AudioManager.instance.StopClip();
        base.Close();
    }

    internal override void Show()
    {
        base.Show();
    }

    private void Awake()
    {
        _Instance = this;
    }

    public static List<string> Recursive(string path)
    {
        List<string> files = new List<string>();

        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);

        // 获取本目录子一级文件
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (!ext.Equals(".mp3") && !ext.Equals(".ogg"))
            {
                continue;
            }
            files.Add(filename.Replace('\\', '/'));
        }

        // 遍历子二级文件夹
        foreach (string dir in dirs)
        {
            var l = Recursive(dir);
            if (l.Count > 0)
            {
                files.AddRange(l);
            }
        }
        return files;
    }
}
