using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;



public class SpriteData
{
    public int _Idx = 0;    
    public float _Time_Frame = 0f;
    public Image _Image = null;
}

public class SpriteDataContainer
{
    List<SpriteData> _listSprData = new List<SpriteData>();    
    public float _Speed = 1f;       //배속
    public float _Time = 0f;        //애니메이션 시간.

}

/// <summary>
/// 스프라이트 편집 클래스
/// 1. 스프라이트 팩커에 들어있는 스프라이트들을 편집해서 재생하자. 
/// 2. 재생이 컴포넌트상에서 확인 가능해야 함. 
/// 3. 프레임 재생 속도 조절 가능
/// 4. 일괄적인 재생 배속의 변화는 가능해야 한다.    
/// </summary>
public class SpriteManager : MonoBehaviour
{
    static SpriteManager _instance = null;


    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    static public SpriteManager Instance
    {
        get
        {
            //실행중일 때만 UIFormManager Instance를 리턴합니다. 
            if (Application.isPlaying)
            {
                if (null == _instance)
                {
                    //_instance = new UIFormManager();
                    var objManager = GameObject.Find("@SpriteManager");
                    if (null != objManager)
                    {
                        _instance = objManager.AddComponent<SpriteManager>();
                        DontDestroyOnLoad(objManager);
                    }
                    else
                    {
                        Debug.Log("can't find @SpriteManager...");
                    }


                }
                return _instance;
            }
            Debug.Log("Application is editmode");
            // 편집모드일때는 Null 값 리턴
            return null;
        }
    }

    public Dictionary<int, SpriteDataContainer> _dicSpriteContainer = new Dictionary<int, SpriteDataContainer>();


}
