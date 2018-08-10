using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

class TipsUI : BaseUI
{
    [SerializeField] Transform _Window;
    // 配置处理
    public TipsUI()
    {
        _NaviData._Layer = EUILayer.Tips;
        _NaviData._Type = EUIType.Coexisting;
    }

    public override void Open(NavigationData data = null)
    {
        base.Open(data);
    }

    public void SetData(Vector3 pos)
    {
        _Window.transform.localPosition = pos;
    }
}
