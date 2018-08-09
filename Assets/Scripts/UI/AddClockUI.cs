using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

class AddClockUI : BaseUI
{
    [SerializeField] Dropdown _Drop;
    [SerializeField] Text _CurAudioText;
    [SerializeField] InputField _DateInput;
    [SerializeField] InputField _HourInput;
    [SerializeField] InputField _MinuteInput;
    [SerializeField] InputField _NameInput;
    List<string> _TimeTypes = new List<string>();

    public AddClockUI()
    {
        _NaviData._Layer = EUILayer.FullScreen;
        _NaviData._Type = EUIType.FullScreen;
    }

    public override void Open(NavigationData data = null)
    {
        _Drop.ClearOptions();
        var ns = Enum.GetNames(typeof(ETimeType));
        _TimeTypes.AddRange(ns);
        base.Open(data);
        _Drop.AddOptions(_TimeTypes);
    }

    internal override void Show()
    {
        base.Show();
        _CurAudioText.text = TimeManager.instance._CurClipPath;
    }

    public void OnSelectAudio()
    {
        UIManager.Instance.Open<AudioSelectUI>();
    }

    public void OnAddClock()
    {
        ETimeType type = (ETimeType)Enum.Parse(typeof(ETimeType), _TimeTypes[_Drop.value]);
        if (type == ETimeType.Weekly)
        {
            ClockDataWeekly w = new ClockDataWeekly();
            w._Type = type;
            int hour;
            int minute;
            int date; 
            if (!int.TryParse(_HourInput.text, out hour) || !int.TryParse(_MinuteInput.text, out minute) || !int.TryParse(_DateInput.text, out date))
            {
                date = 1; 
                hour = 12;
                minute = 0;
            }
            w._DayOfWeek = date;
            w._Time = (hour << 8) | minute;
            w._AudioPath = TimeManager.instance._CurClipPath;
            w._Name = _NameInput.text; 
            TimeManager.instance.AddClock(w);
        }
        UIManager.Instance.Close(this); 
    }
}
