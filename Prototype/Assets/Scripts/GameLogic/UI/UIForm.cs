using UnityEngine;
using System.Collections;


/// <summary>
/// 버튼, 다이얼로그등에 사용할 UI 는 UIForm 형태로 관리.
/// </summary>
public class UIForm : UIObject
{
    static public Vector2 CENTER_ANCHOR = new Vector2(0.5f, 0.5f);

    private bool _initialized = false;


    public virtual void Open()
    {
        Initialize();

        IsVisible = true;
        SetDepthLast();
    }


    public virtual void Close()
    {
        IsVisible = false;
    }


    /// <summary>
    /// 현재 패널안에서 가장 마지막에 표시합니다. 
    /// </summary>
    public void SetDepthLast()
    {
        transform.SetAsLastSibling();
    }



    private void Initialize()
    {
        if (_initialized)
            return;

        _initialized = true;
    }
}
