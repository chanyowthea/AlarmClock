using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

class MessageUI : BaseUI
{
    [SerializeField] Text _Text; 
    // 配置处理
    public MessageUI()
    {
        _NaviData._Layer = EUILayer.Tips;
        _NaviData._Type = EUIType.Coexisting;
    }

    public override void Open(NavigationData data = null)
    {
        base.Open(data);
        Invoke("WaitClose", 3f); 
    }

    public void SetData(string msg)
    {
        _Text.text = msg;
    }

    void WaitClose()
    {
        CloseExternal();
    }
}
