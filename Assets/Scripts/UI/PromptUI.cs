using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

class PromptUI : BaseUI
{
    [SerializeField] Text _ContentText;
    Action _OnOK;

    // 配置处理
    public PromptUI()
    {
        _NaviData._Layer = EUILayer.Popup;
        _NaviData._Type = EUIType.Coexisting;
    }

    public override void Open(NavigationData data = null)
    {
        base.Open(data);
    }

    public void SetData(string content, Action action = null)
    {
        _ContentText.text = content;
        _OnOK = action;
    }

    public void OnClickOK()
    {
        if (_OnOK != null)
        {
            _OnOK();
        }
        UIManager.Instance.Close(this); 
    }
}
