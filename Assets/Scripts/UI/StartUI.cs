using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

class StartUI : BaseUI
{
    [SerializeField] Text _NextRingingTimeText;
    [SerializeField] Text _LogText;
    DateTime _NextRingingClock;

    public static StartUI Instance{ private set; get;}

    public StartUI()
    {
        _NaviData._Layer = EUILayer.FullScreen;
        _NaviData._Type = EUIType.FullScreen;
    }

    public void LogCallback(string condition, string stackTrace, LogType type)
    {
        _LogText.text += "\n\ncondition=" + condition + "\nstackTrace=" + stackTrace;
    }

    public override void Open(NavigationData data = null)
    {
        Instance = this; 
        Application.logMessageReceived += LogCallback;

        _LogText.text = null; 
        AudioManager.instance.PlayClip1("Audio/Conversation");
        UIManager.Instance.Open<TopResidentUI>();
        base.Open(data);
    }

    internal override void Close()
    {
        Application.logMessageReceived -= LogCallback;
        base.Close();
    }

    internal override void Show()
    {
        base.Show();
        _NextRingingClock = TimeManager.instance.GetNextRingingClock();

        _NextRingingClock = DateTime.Now;
        _NextRingingClock.AddSeconds(1);

        if (_NextRingingClock == DateTime.MaxValue)
        {
            _NextRingingTimeText.text = "you has no alarm clock on list! ";
        }
    }

    public void OnClickAdd()
    {
        UIManager.Instance.Open<AddClockUI>();
    }

    private void Update()
    {
        if (_NextRingingClock == DateTime.MaxValue)
        {
            return;
        }

        var span = _NextRingingClock - DateTime.Now;
        _NextRingingTimeText.text = span.ToString();
        if (span < TimeManager.instance._MinGap)
        {
            var c = TimeManager.instance.GetNearestClock();
            if (c != null)
            {
                TimeManager.instance.PopupNearestClock();
                var ui = UIManager.Instance.Open<PromptUI>();
                AudioManager.instance.PlayClip(c._AudioPath);
                ui.SetData(c._Name + " alarm clock ringing! ", () =>
                {
                    AudioManager.instance.StopClip();
                });
            }
            _NextRingingClock = DateTime.MaxValue;
        }
    }
}
