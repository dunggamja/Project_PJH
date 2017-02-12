using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIFormManager
{
    static UIFormManager _instance = null;


    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    static public UIFormManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new UIFormManager();
            }
            return _instance;
        }        
    }

    /// <summary>
    /// UIFormManger에서 관리할 UIForm 리스트
    /// </summary>
    private List<UIForm> _listUIForms = new List<UIForm>();





    /// <summary>
    /// UIForm 을 엽니다. 
    /// </summary>
    /// <typeparam name="T">UIForm을 상속받은 클래스 </typeparam>
    static public void OpenUIForm<T>() where T : UIForm
    {

    }


    /// <summary>
    /// UIForm을 닫습니다. 
    /// </summary>
    /// <typeparam name="T">UIForm을 상속받은 클래스</typeparam>
    static public void CloseUIForm<T>() where T : UIForm
    { }
        

    

}
