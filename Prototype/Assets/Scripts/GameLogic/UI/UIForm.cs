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
    /// UIForm을 초기화 합니다. 
    ///  - UIForm::Open 시 실행됩니다.
    /// (이미 초기화 되었을 경우에는 실행되지 않습니다.)
    /// </summary>
    protected virtual void Initialize()
    {
        if (_initialized)
            return;

        _initialized = true;

        BindUIObjects();
    }


    /// <summary>
    /// 현재 패널안에서 가장 마지막에 표시합니다. 
    /// </summary>
    public void SetDepthLast()
    {
        transform.SetAsLastSibling();
    }


    /// <summary>
    /// UIObject 들을 바인딩 할 수 있습니다.
    ///   - 각 UIForm에서 자주 사용하는 객체들은 여기서 바인딩 해놓고 사용하는 걸 추천합니다.
    ///   - 또한 UIObject들에 이벤트 연결도 이곳에서 하는 걸 추천합니다. 
    /// </summary>
    protected virtual void BindUIObjects()
    {
    }


    
}
