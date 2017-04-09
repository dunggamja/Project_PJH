using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour
{
    static GameMain _instance = null;

    private float _LoadPercent = 0f;
    private float _CurLoadPercent = 0f;

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


    /// <summary>
    /// 게임 첫 실행시 프로세스를 실행시키는 함수입니다. 
    /// [필요기능]
    /// 1. 다운로드 & 업데이트 검사 (X)
    /// 2. 데이터 파일 로드 (O)
    /// 3. 리소스 로드(X)
    /// 4. 계정 체크 (X)
    /// 5. 세이브 파일 로드 (X)
    /// 6. 게임 시작 (O)
    /// 7. 로딩 바 표시.
    /// </summary>
    /// <returns></returns>
    IEnumerator coStart_Process()
    {
        _LoadPercent = 0f;

        //DataLoader.Instance.LoadTextFile()
        yield return null;


        _LoadPercent = 1f;
        yield return null;
    }




}
