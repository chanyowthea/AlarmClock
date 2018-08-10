using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

public enum ETimeType
{
    Weekly,
    Daily,
    Monthly,
    DaysGap,
    Once,
}

public class TimeManager : TSingleton<TimeManager>
{
    public string _CurClipPath;
    List<ClockDataWeekly> _WeeklyDataList = new List<ClockDataWeekly>();

    TimeService _Timer = new TimeService();
    public TimeSpan _MinGap { private set; get; }

    public override void Init()
    {
        base.Init();
        _MinGap = TimeSpan.FromSeconds(0.01f);
    }

    public void AddClock<T>(T data)
        where T : ClockDataCommon
    {
        if (data._Type == ETimeType.Weekly)
        {
            bool isEqual = false;
            for (int i = 0, length = _WeeklyDataList.Count; i < length; i++)
            {
                var w = _WeeklyDataList[i];
                if (w.Equals(data))
                {
                    isEqual = true;
                    break;
                }
            }
            if (!isEqual)
            {
                var ui = UIManager.Instance.Open<MessageUI>();
                ui.SetData("add new clock success! ");
                _WeeklyDataList.Add(data as ClockDataWeekly);
                SortWeeklyData(_WeeklyDataList);
            }
            else
            {
                var ui = UIManager.Instance.Open<MessageUI>();
                ui.SetData("you have add an identical clock already! ");
            }
        }
    }

    public void Update()
    {
        _Timer.UpdateTime();
    }

    public DateTime GetCurTime()
    {
        return DateTime.Now;
    }

    public ClockDataWeekly GetNearestClock()
    {
        if (_WeeklyDataList == null || _WeeklyDataList.Count == 0)
        {
            return null;
        }
        return _WeeklyDataList[0];
    }

    public void PopupNearestClock()
    {
        if (_WeeklyDataList == null || _WeeklyDataList.Count == 0)
        {
            return;
        }
        _WeeklyDataList.RemoveAt(0);
    }

    public DateTime GetNextRingingClock()
    {
        var c = GetNearestClock();
        if (c != null)
        {
            var now = DateTime.Now;
            int hour = c._Time >> 8;
            int minute = 0xff & c._Time;
            DateTime clock = new DateTime(now.Year, now.Month, c._DayOfWeek, hour, minute, 0);
            var span = clock - now;
            if (span < TimeSpan.Zero)
            {
                clock = clock.AddMonths(1);
            }
            return clock;
        }
        return DateTime.MaxValue;
    }

    void SortWeeklyData(List<ClockDataWeekly> list)
    {
        if (list == null)
        {
            return;
        }

        for (int i = 0, length = list.Count; i < length - 1; i++)
        {
            var l = list[i];
            int tag = i;
            for (int j = i + 1; j < length; j++)
            {
                if (list[j]._DayOfWeek < l._DayOfWeek)
                {
                    tag = j;
                }
                else if (list[j]._DayOfWeek == l._DayOfWeek)
                {
                    if (list[j]._Time < l._Time)
                    {
                        tag = j;
                    }
                }
            }
            if (tag != i)
            {
                var temp = list[i];
                list[i] = list[tag];
                list[tag] = temp;
            }
        }
    }
}
