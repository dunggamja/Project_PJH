using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// UIForm들을 관리하는 클래스.
/// </summary>
public class UIFormManager : MonoBehaviour
{
    static UIFormManager _instance = null;


    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    static public UIFormManager Instance
    {
        get
        {
            //실행중일 때만 UIFormManager Instance를 리턴합니다. 
            if (Application.isPlaying)
            {
                if (null == _instance)
                {
                    //_instance = new UIFormManager();
                    var objManager = GameObject.Find("@UIFormManager");
                    if (null != objManager)
                    {
                        _instance = objManager.AddComponent<UIFormManager>();
                        DontDestroyOnLoad(objManager);
                    }
                    else
                    {
                        Debug.Log("can't find @UIFormManager...");
                    }

                    
                }
                return _instance;
            }
            Debug.Log("Application is editmode");
            // 편집모드일때는 Null 값 리턴
            return null;
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
    public void OpenUIForm<T>() where T : UIForm
    {
        for (int i = 0; i < _listUIForms.Count; ++i)
        {
            //리스트에 있는 UIForm이면 Open처리.
            if (_listUIForms[i] is T)
            {
                _listUIForms[i].Open();
                return;
            }
        }

        //파일 경로
        string strTypeName = typeof(T).Name;        
        string strPath = string.Format("UI/UIForm/{0}", strTypeName);


        //리소스 로드.
        var uiPrefab = Resources.Load(strPath) as GameObject;


        // 생성 및 리스트에 추가.
        var uiobj = Instantiate(uiPrefab, this.transform, false) as GameObject;
        if (null == uiobj)
        {
            Debug.Log("Instantiate UIForm is failed!!!");
            return;
        }

        var uiFormComponent = uiobj.AddComponent<T>();
        if (null == uiFormComponent)
        {
            Debug.Log("AddComponent is failed!!!" + uiFormComponent.name);
            return;
        }
        uiobj.name = strTypeName;

        _listUIForms.Add(uiFormComponent);        
        uiFormComponent.Open();  

    }


    /// <summary>
    /// UIForm을 닫습니다. 
    /// </summary>
    /// <typeparam name="T">UIForm을 상속받은 클래스</typeparam>
    public void CloseUIForm<T>() where T : UIForm
    {
        for (int i = 0; i < _listUIForms.Count; ++i)
        {
            //리스트에 있는 UIForm이면 Open처리.
            if (_listUIForms[i] is T)
            {
                _listUIForms[i].Close();
                return;
            }
        }
    }


    /// <summary>
    /// UIForm을 게임상에서 삭제합니다. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void DestroyCloseUIForm<T>() where T : UIForm
    {
        UIForm destroyUIForm = null;

        for (int i = 0; i < _listUIForms.Count; ++i)
        {
            //리스트에서 제거.
            if (_listUIForms[i] is T)
            {
                _listUIForms[i].Close();
                destroyUIForm = _listUIForms[i];
                _listUIForms.Remove(_listUIForms[i]);
                break;
            }
        }

        //삭제.
        if (null != destroyUIForm)
        {
            Destroy(destroyUIForm.gameObject);
        }
    }


    /// <summary>
    /// 모든 UIForm을 닫습니다. 
    /// </summary>
    public void CloseAllUIForms()
    {
        for (int i = 0; i < _listUIForms.Count; ++i)
            _listUIForms[i].Close();
    }

    /// <summary>
    /// 모든 UIForm을 게임상에서 삭제합니다.
    /// </summary>
    public void DestroyAllUIForms()
    {
        var listDestroyUIForms = _listUIForms.FindAll((uiform) => { return true; });

        for (int i = 0; i < listDestroyUIForms.Count; ++i)
        {
            listDestroyUIForms[i].Close();
            Destroy(listDestroyUIForms[i].gameObject);
        }

        _listUIForms.RemoveAll( (uiform) => {  return true; });
    }

}
