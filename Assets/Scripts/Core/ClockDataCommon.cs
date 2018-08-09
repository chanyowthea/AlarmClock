using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daily, Once
public class ClockDataCommon
{
    public ETimeType _Type;
    public string _Name;
    public string _AudioPath;
    public int _Time; // 前面1字节存储时，后面1字节存储分

    public override bool Equals(object obj)
    {
        ClockDataCommon data = obj as ClockDataCommon;
        if (data == null)
        {
            return false;
        }
        return data._Type == _Type && data._Time == _Time;
    }
}
public class ClockDataWeekly : ClockDataCommon
{
    public int _DayOfWeek;

    public override bool Equals(object obj)
    {
        ClockDataWeekly data = obj as ClockDataWeekly;
        if (data == null)
        {
            return false;
        }
        return base.Equals(obj) && data._DayOfWeek == _DayOfWeek;
    }
}
public class ClockDataDaysGap : ClockDataCommon
{
    public int _GapDays;

    public override bool Equals(object obj)
    {
        ClockDataDaysGap data = obj as ClockDataDaysGap;
        if (data == null)
        {
            return false;
        }
        return base.Equals(obj) && data._GapDays == _GapDays;
    }
}
public class ClockDataMonthly : ClockDataCommon
{
    public int _DateInMonth;

    public override bool Equals(object obj)
    {
        ClockDataMonthly data = obj as ClockDataMonthly;
        if (data == null)
        {
            return false;
        }
        return base.Equals(obj) && data._DateInMonth == _DateInMonth;
    }
}

