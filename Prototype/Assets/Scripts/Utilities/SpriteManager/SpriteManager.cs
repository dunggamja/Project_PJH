using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


[Serializable]
public class SpriteData
{
    [SerializeField] public int _Idx = 0;            //인덱스
    [SerializeField] public float _Time_Frame = 0f;  //보여질 시간.
    [SerializeField] public Sprite _Image = null;    //스프라이트
}

[Serializable]
public class SpriteDataContainer
{
    [SerializeField] public List<SpriteData> _listSprData = new List<SpriteData>();    
    [SerializeField] public float _Speed = 1f;       //배속
    [SerializeField] public float _Time = 0f;        //애니메이션 시간.
    [SerializeField] public float _TimeGap = 1f;   //시간간격
    
    public void AddData(Sprite spr)
    {
        SpriteData tempData = new SpriteData();
        tempData._Image = spr;
        tempData._Time_Frame = _TimeGap;
        tempData._Idx = _listSprData.Count;

        _listSprData.Add(tempData);

        _Time += _TimeGap;
    }

    public void Remove(int idx)
    {
        if (idx < 0 || _listSprData.Count <= idx)
            return;


        _listSprData.RemoveAt(idx);
        UpdateData_Idx();
        UpdateData_TotalTime();
    }

    public void UpdateData_Idx()
    {
        for (int idx = 0; idx < _listSprData.Count; ++idx)
        {
            _listSprData[idx]._Idx = idx;
        }
    }

    public void UpdateData_TotalTime()
    {
        _Time = 0;
        for (int idx = 0; idx < _listSprData.Count; ++idx)
        {
            _Time += _listSprData[idx]._Time_Frame ;
        }
    }

    public int FindIdx_ByTime(float time)
    {
        float aniTime = 0f;
        for (int idx = 0; idx < _listSprData.Count; ++idx)
        {
            if (aniTime <= time && time < aniTime + _listSprData[idx]._Time_Frame)
            {
                return idx;
            }
            aniTime += _listSprData[idx]._Time_Frame;
        }

        return -1;
    }

    public Sprite GetSprite_ByIdx(int idx)
    {
        if (idx < 0 || _listSprData.Count <= idx)
            return null;

        if (null != _listSprData[idx])
            return _listSprData[idx]._Image;


        return null;
    }
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

    public Dictionary<string, SpriteDataContainer> _dicSpriteContainer = new Dictionary<string, SpriteDataContainer>();


}
