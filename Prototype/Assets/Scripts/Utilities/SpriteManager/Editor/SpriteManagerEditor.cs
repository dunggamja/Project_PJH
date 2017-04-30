using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SpriteManager))]
[CanEditMultipleObjects]
[SerializeField]
public class SpriteManagerEditor : Editor
{
    [SerializeField]
    public class SpriteManager_DicInfo
    {
        public int _key = 0;
        public SpriteDataContainer _value = null;
    }

    // 에디터에 표시될 스프라이트 목록들 
    [SerializeField]
    public List<SpriteManager_DicInfo> _listSpriteDicInfo = new List<SpriteManager_DicInfo>();


    public SpriteManager _spriteManager = null;

    private void OnEnable()
    {
        _spriteManager = target as SpriteManager;
        ConvertSpriteManagerData_ToList();
    }

    private void OnDisable()
    {
        
    }


    /// <summary>
    /// SpriteManager 클래스에 Dictionary에 있는 데이터들을 
    /// List에 답습니다. 
    /// </summary>
    private void ConvertSpriteManagerData_ToList()
    {
        if (null == _spriteManager)
        {
            Debug.Log("SpriteManager is null");
            return;
        }

        _listSpriteDicInfo.Clear();

        var iter = _spriteManager._dicSpriteContainer.GetEnumerator();

        while (iter.MoveNext())
        {
            var key = iter.Current.Key;
            var value = iter.Current.Value;

            if (null == value)
                continue;

            SpriteManager_DicInfo tempInfo = new SpriteManager_DicInfo();
            tempInfo._key = key;
            tempInfo._value = value;
            _listSpriteDicInfo.Add(tempInfo);
        }
    }


    /// <summary>
    /// SpriteManagerEditor 클래스에 List에 있는 데이터들을 
    /// SpriteManger 클래스의 Dictionary 컨테이너에 답습니다. 
    /// </summary>
    private void ConvertListData_ToSpriteManagerData()
    {
        if (null == _spriteManager)
        {
            Debug.Log("SpriteManager is null");
            return;
        }

        _spriteManager._dicSpriteContainer.Clear();

        for (int idx = 0; idx < _listSpriteDicInfo.Count; ++idx)
        {
            var info = _listSpriteDicInfo[idx];
            if (null == info)
                continue;

            _spriteManager._dicSpriteContainer.Add(info._key, info._value);
        }
    }






}
