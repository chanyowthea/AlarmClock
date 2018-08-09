using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

class BuildUI : BaseUI
{
    TipsUI _Tips; 
    // 配置处理
    public BuildUI()
    {
        _NaviData._Layer = EUILayer.FullScreen;
    }

    public override void Open(NavigationData data = null)
    {
        base.Open(data);
    }

    public void OnClickMessage()
    {
        UIManager.Instance.Open<MessageUI>();
    }

    public void OnTipsShow()
    {
        _Tips = UIManager.Instance.Open<TipsUI>();
        _Tips.SetData(Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0)); 
    }

    public void OnTipsHide()
    {
        UIManager.Instance.Close(_Tips);
    }
}
