using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

class EndUI : BaseUI
{
    // 配置处理
    public EndUI()
    {
        _NaviData._Layer = EUILayer.FullScreen;
    }

    public override void Open(NavigationData data = null)
    {
        base.Open(data);
    }

    public void OnClickEnd()
    {

    }
}
