using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour
{
    static GameMain _instance = null;

    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    static public GameMain Instance
    {
        get
        {
            //실행중일 때만 UIFormManager Instance를 리턴합니다. 
            if (Application.isPlaying)
            {
                if (null == _instance)
                {
                    var objGameMain = GameObject.Find("@GameMain");
                    if (null != objGameMain)
                    {
                        _instance = objGameMain.AddComponent<GameMain>();
                        DontDestroyOnLoad(objGameMain);
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



    void Start()
    {
        TurnManager.Instance.Init();
        UIFormManager.Instance.OpenUIForm<CommandUIDlg>();
    }




}
