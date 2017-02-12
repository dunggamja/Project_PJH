using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 턴을 관리하는 매니저.
/// ( 현재는 기본적인 기능 만을 가지고 있지만,
///   나중에는 턴 순서 조절 등의 기능 추가도 염두에 두자....)
/// </summary>
public class TurnManager
{

    /// <summary>
    /// 현재 상태.
    /// </summary>
    public enum STATE
    {
        NONE,           
        TURN_ANNOUNCE, //턴 소개
        USER_TURN,     //유저 행동
        USER_TURN_END, //턴 종료 후 처리.
        BATTLE_END,     // 전투 종료. 
        SHOW_RESULT,    // 승패 표시
    }


    /// <summary>
    /// 턴 관련 데이터.
    /// </summary>
    public class TurnUserData
    {
        /// <summary>
        /// 유저 ID
        /// </summary>
        public Int64 _userID = 0;
                
    }

    static TurnManager _instance = null;

    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    static public TurnManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new TurnManager();                
            }
            return _instance;
        }
    }
    
    /// <summary>
    /// 턴 관리 대상 리스트.
    /// </summary>
    private List<TurnUserData> _listTurnData = new List<TurnUserData>();

    /// <summary>
    /// 현재 누구의 턴인지 표시.
    /// </summary>
    private int _CurIdx_listTurnUser = 0;

    /// <summary>
    /// 현재 몇 턴인지 표시하기 위한 변수.
    /// </summary>
    private int _CurTurn = 0;
    /// <summary>
    /// 현재 몇 턴인지 표시하기 위한 변수.
    /// </summary>
    public int CurTurn { get { return _CurTurn; } }


    /// <summary>
    /// 현재 상태. 
    /// </summary>
    private STATE _eState = STATE.NONE;
    /// <summary>
    /// 현재 상태. 
    /// </summary>
    public STATE State { get { return _eState; } }


    /// <summary>
    /// 배틀에 참가할 유저데이터 추가.
    /// </summary>
    /// <param name="userData"></param>
    public void AddTurnUserData(TurnUserData userData)
    {
        _listTurnData.Add(userData);
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        
        Clear();
    }


    /// <summary>
    /// 각종 데이터 삭제.
    /// </summary>
    public void Clear()
    { 
        _listTurnData.Clear(); //유저리스트 클리어.
        _eState = STATE.NONE; //스테이트 초기화.
        _CurIdx_listTurnUser = 0;
        _CurTurn = 0;
    }


    /// <summary>
    /// 사용자의 턴이 끝나고 다음턴으로 넘김.
    /// </summary>
    public void NextTurn()
    {
        ++_CurIdx_listTurnUser;

        if (_listTurnData.Count == _CurIdx_listTurnUser)
        {

            _CurIdx_listTurnUser = 0;

            //턴 증가
            ++_CurTurn;
        }
    }


    /// <summary>
    /// 현재 턴인 유저 ID 반환.
    /// </summary>
    /// <returns></returns>
    public Int64 Get_CurrentTurn_UserID()
    {
        if(_listTurnData.Count <= _CurIdx_listTurnUser)
            return -1;

        return _CurIdx_listTurnUser;
    }

}
