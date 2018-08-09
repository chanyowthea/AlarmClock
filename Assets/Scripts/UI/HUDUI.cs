using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

class HUDUI : BaseUI
{
    // 配置处理
    public HUDUI()
    {
        _NaviData._Layer = EUILayer.FullScreen;
        _NaviData._Type = EUIType.FullScreen;
    }

    public override void Open(NavigationData data = null)
    {
        base.Open(data);
        UIManager.Instance.Open<TopResidentUI>();
    }

    internal override void Close()
    {
        base.Close();
    }

    public void OnClickEnd()
    {
        UIManager.Instance.Open<EndUI>();
    }

    public void OnClickTips()
    {
        UIManager.Instance.Open<TipsUI>();
    }

    public void OnClickMessage()
    {
        UIManager.Instance.Open<MessageUI>();
    }

    public void OnClickPrompt()
    {
        UIManager.Instance.Open<PromptUI>();
    }

    public void OnClickBuild()
    {
        UIManager.Instance.Open<BuildUI>();
    }
}
