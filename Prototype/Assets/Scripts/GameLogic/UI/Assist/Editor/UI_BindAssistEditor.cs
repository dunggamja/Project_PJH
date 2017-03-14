using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(UI_BindAssist))]
[CanEditMultipleObjects]
[SerializeField]
public class UI_BindAssistEditor : Editor
{
    public GameObject _objBind = null;
    public UI_BindAssist _bindAssist = null;




    /// <summary>
    /// 바인드 오브젝트들.
    /// </summary>
    [SerializeField]
    public List<CustomBindObject> _listBindObjects = new List<CustomBindObject>();
    //public Dictionary<string, GameObject> _dicBindObject = null;

    bool _showBindObjects = true;

    private void OnEnable()
    {
        _bindAssist = target as UI_BindAssist;
        _listBindObjects = _bindAssist._listBindObjects;
    }

    private void OnDisable()
    {
        HashSet<string> checkOverlapKey = new HashSet<string>();
        foreach (var item in _listBindObjects)
        {
            if (checkOverlapKey.Contains(item.key))
            {
                EditorUtility.DisplayDialog("에러", "동일한 이름의 키값이 존재합니다.", "확인");
                break;
            }
            checkOverlapKey.Add(item.key);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
            return;

        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        _objBind = EditorGUILayout.ObjectField("BindObject", _objBind, typeof(GameObject), true) as GameObject;
        EditorGUILayout.EndHorizontal();

        _showBindObjects = EditorGUILayout.Foldout(_showBindObjects, "BindObjects");
        if (_showBindObjects)
        {
            string removekey = string.Empty;

            foreach (var item in _listBindObjects)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("-"))
                {
                    removekey = item.key;
                }
                item.key = EditorGUILayout.TextField(item.key);
                item.obj = EditorGUILayout.ObjectField(item.obj, typeof(GameObject), true) as GameObject;
                EditorGUILayout.EndHorizontal();
            }

            if (false == string.IsNullOrEmpty(removekey))
                RemoveBindObject(removekey);
        }


        if (null != _objBind)
        {
            bool bContrain = false;
            foreach (var item in _listBindObjects)
            {
                if(item.key == _objBind.name)
                {
                    bContrain = true;
                    EditorUtility.DisplayDialog("에러", "동일한 이름의 키값이 존재합니다.", "확인");
                    break;
                }
            }

            if(false == bContrain)
                _listBindObjects.Add(new CustomBindObject(_objBind.name, _objBind));

            _objBind = null;
        }

        

        if (GUI.changed)
        {

            EditorUtility.SetDirty((UI_BindAssist)_bindAssist);
            serializedObject.ApplyModifiedProperties();
        }

    }

    private void RemoveBindObject(string key)
    {
        foreach (var item in _listBindObjects)
        {
            if (item.key == key)
            {
                _listBindObjects.Remove(item);
                break;
            }
        }
    }
}
