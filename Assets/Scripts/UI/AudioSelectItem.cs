using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

public class AudioSelectItem : MonoBehaviour, ILoopScrollRectItem
{
    [SerializeField] Text _text;
    string _ClipPath; 
    public void SetData(params object[] data)
    {
        if (data == null || data.Length == 0)
        {
            return;
        }
        _ClipPath = data[0] as string;
        _text.text = _ClipPath;
    }

    void ScrollCellIndex(int idx)
    {

    }

    public void OnClickPlay()
    {
        AudioManager.instance.PlayClip(_ClipPath);
    }

    public void OnClickSet()
    {
        Debug.Log("OnClickSet _ClipName=" + _ClipPath); 
        TimeManager.instance._CurClipPath = _ClipPath;
        UIManager.Instance.Close<AudioSelectUI>();
    }
}
