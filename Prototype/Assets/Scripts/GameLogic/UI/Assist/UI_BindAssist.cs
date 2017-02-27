using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;


/// <summary>
/// 게임오브젝트의 바인딩을 도와주는 클래스.
/// </summary>
public class UI_BindAssist
{
    /// <summary>
    /// 해당 Transform 의 차일드 오브젝트에서 게임오브젝트를 검사합니다. 
    /// </summary>
    public Transform _transform = null;

    /// <summary>
    /// Bind된 게임 오브젝트들.
    /// </summary>
    Dictionary<int, GameObject> _dicBindObjects = new Dictionary<int, GameObject>();

    private bool _isBinded = false;

    public UI_BindAssist(Transform transform)
    {
        _transform = transform;
    }

    /// <summary>
    /// 게임 오브젝트 바인드. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Bind<T>() where T : struct, IConvertible
    {
        if (null == _transform)
        {
            Debug.Log("transform is null");
            return;
        }
               

        if (_isBinded)
            return;

        if (false == typeof(T).IsEnum)
            return;


        T[] arrayEnums = (T[])System.Enum.GetValues(typeof(T));

        Transform[] arrayChildTrans = _transform.GetComponentsInChildren<Transform>();

        for (int i = 0; i < arrayEnums.Length; ++i)
        {
            var item = arrayEnums[i];

            Transform childTrans = FindTransformInArray(arrayChildTrans, item.ToString());

            if (null == childTrans)
                continue;

            int key = item.ToInt32(NumberFormatInfo.InvariantInfo);
            if (false == _dicBindObjects.ContainsKey(key))
                _dicBindObjects.Add(key, childTrans.gameObject);
        }

        _isBinded = true;
    }

    public GameObject GetBindObject(int key)
    {
        if (_dicBindObjects.ContainsKey(key))
            return _dicBindObjects[key];

        return null;
    }

    public void ClearBindObjects()
    {
        _isBinded = false;
        _dicBindObjects.Clear();
    }

    private Transform FindTransformInArray(Transform[] arrayTrans, string name)
    {
        for (int i = 0; i < arrayTrans.Length; ++i)
        {
            if (string.Equals(arrayTrans[i].name, name))
                return arrayTrans[i];
        }
        return null;
    }
    

}


