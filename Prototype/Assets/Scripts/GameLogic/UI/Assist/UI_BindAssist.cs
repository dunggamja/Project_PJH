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



    public GameObject GetBindObject(string key)
    {

        var findObj = _listBindObjects.Find((item) => { return item.key == key; });
        if (null != findObj)
            return findObj.obj;

        return null;
    }
}


