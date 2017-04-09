using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;



public class SpriteData
{
    public int _Idx = 0;
    public int _Frame = 0;
    public float _Time = 0f;
    public Image _Image = null;
}

public class SpriteDataContainer
{
    List<SpriteData> _listSprData = new List<SpriteData>();
    bool _repeat = false;
    float _fSpeed = 1f;
}

/// <summary>
/// 스프라이트 편집 클래스
/// 1. 스프라이트 팩커에 들어있는 스프라이트들을 편집해서 재생하자. 
/// 2. 재생이 컴포넌트상에서 확인 가능해야 함. 
/// 3. 프레임 재생 속도 조절 가능
/// 4. 각 프레임마다 보여지는 시간 컨트롤 가능
/// 5. 일괄적인 재생 배속의 변화는 가능해야 한다. 
/// 6. 60FPS 기본으로 놓고 계산을 한다. 
/// </summary>
public class SpriteManager : MonoBehaviour
{
    Dictionary<string, SpriteDataContainer> _dicSpriteContainer = new Dictionary<string, SpriteDataContainer>();


}
