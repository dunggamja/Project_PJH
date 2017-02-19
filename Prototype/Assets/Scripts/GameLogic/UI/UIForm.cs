using UnityEngine;
using System.Collections;


/// <summary>
/// 버튼, 다이얼로그등에 사용할 UI 는 UIForm 형태로 관리.
/// </summary>
public class UIForm : UIObject
{
    static public Vector2 CENTER_ANCHOR = new Vector2(0.5f, 0.5f);
    
    public virtual void Open()
    {
        IsVisible = true;        
    }


    public virtual void Close()
    {
        IsVisible = false;
    }   

}
