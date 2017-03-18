using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;

[Serializable]
public class CustomBindObject
{
    [SerializeField]
    public string key;
    [SerializeField]
    public GameObject obj;

    public CustomBindObject(string key, GameObject obj)
    {
        this.key = key;
        this.obj = obj;
    }
}

/// <summary>
/// 게임오브젝트의 바인딩을 도와주는 클래스.
/// </summary>
public class UI_BindAssist : MonoBehaviour
{
    /// <summary>
    /// Bind된 게임 오브젝트들.
    /// </summary>      
    [SerializeField]
    public List<CustomBindObject> _listBindObjects = new List<CustomBindObject>();

    /// <summary>
    /// 바인딩 된 게임오브젝트 리스트.
    /// </summary>
    public List<CustomBindObject> BindObjectsList
    {
        get { return _listBindObjects; }
    }

    /// <summary>
    /// 바인딩된 게임오브젝트를 얻어옴.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject GetBindObject(string key)
    {

        var findObj = _listBindObjects.Find((item) => { return item.key == key; });
        if (null != findObj)
            return findObj.obj;

        return null;
    }

    /// <summary>
    /// 바인딩 된 게임 오브젝트에서 컴포넌트를 얻어옵니다. 
    /// </summary>
    /// <typeparam name="T">컴포넌트</typeparam>
    /// <param name="key">키 값</param>
    /// <returns></returns>
    public T GetControl<T>(string key) where T : Component
    {
        var obj = GetBindObject(key);
        if (null != obj)
        {
            T component = obj.GetComponent<T>();
            if (null != component)
                return component;
        }
        return null;
    }


}


