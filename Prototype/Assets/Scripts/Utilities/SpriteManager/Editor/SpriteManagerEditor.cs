using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(SpriteManager))]
[CanEditMultipleObjects]
[SerializeField]
public class SpriteManagerEditor : Editor
{
    [SerializeField]
    public class SpriteManager_DicInfo
    {
        public string _key = string.Empty;
        public SpriteDataContainer _value = null;
    }

    // 에디터에 표시될 스프라이트 목록들 
    [SerializeField]
    public List<SpriteManager_DicInfo> _listSpriteDicInfo = new List<SpriteManager_DicInfo>();

    [SerializeField]
    public SpriteDataContainer _listSprite = new SpriteDataContainer();

    public SpriteManager _spriteManager = null;


    private bool _IsShowSpriteEdit = true;
    private bool _IsShowPreview = true;
    private bool _IsShowSpriteList = true;

    private Sprite _sprPreview = null;
    private Texture2D _texPreview = null;

    private void OnEnable()
    {
        _spriteManager = target as SpriteManager;
        //ConvertSpriteManagerData_ToList();

        EditorApplication.update += Update;
        _previewTime_StartUp = EditorApplication.timeSinceStartup;
    }

    private void OnDisable()
    {
        EditorApplication.update -= Update;
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
    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying) return;

        serializedObject.Update();

        if (GUILayout.Button("Editor"))
        {
            SpriteEditorWindow.Open();
        }
        
        //스프라이트 편집창 표시 ON /OFF
        _IsShowSpriteEdit = EditorGUILayout.Foldout(_IsShowSpriteEdit, "Sprite Edit");
        //스프라이트 편집창 표시 ON /OFF
        if (_IsShowSpriteEdit)
        {
            ShowInspector_SpriteList();
        }

        _IsShowPreview = EditorGUILayout.Foldout(_IsShowPreview, "Preview");
        if (_IsShowPreview)
        {
            ShowInspector_Preview();
        }



        if (GUI.changed)
        {
            EditorUtility.SetDirty(_spriteManager);
            serializedObject.ApplyModifiedProperties();
        }


    }


    private void ShowInspector_SpriteList()
    {
                //EditorGUILayout.PropertyField(serializedObject.FindProperty("objects"));
        _sprPreview = EditorGUILayout.ObjectField(_sprPreview, typeof(Sprite), true) as Sprite;

        if (null != _sprPreview)
        {
            bool bContain = false;
            foreach (var item in _listSprite._listSprData)
            {
                //동일한 스프라이트가 있을 경우. 
                if (item._Image.name == _sprPreview.name)
                {
                    bContain = true;
                    EditorUtility.DisplayDialog("에러", "동일한 이름의 키값이 존재합니다.", "확인");
                    break;
                }
            }

            if (false == bContain)
            {
                _listSprite.AddData(_sprPreview);
            }
            

            _sprPreview = null;
        }

        _IsShowSpriteList = EditorGUILayout.Foldout(_IsShowSpriteList, "Sprite List");

        if (_IsShowSpriteList)
        {
            for (int idx = 0; idx < _listSprite._listSprData.Count; ++idx)
            {
                if (null == _listSprite._listSprData[idx])
                    continue;

                EditorGUILayout.BeginHorizontal();
                _listSprite._listSprData[idx]._Image = EditorGUILayout.ObjectField(_listSprite._listSprData[idx]._Image, typeof(Sprite), true) as Sprite;
                EditorGUILayout.EndHorizontal();
            }
        }
        
    }



    private Rect _rectPreview = new Rect();

    private void ShowInspector_Preview()
    {
        GUILayoutOption[] previewOptions = new GUILayoutOption[]
        {
            GUILayout.MaxHeight(200),
            GUILayout.MaxWidth(200)
        };

        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("", previewOptions);
        var lastRect = GUILayoutUtility.GetLastRect();
        _rectPreview = new Rect(0, lastRect.yMin + 5, 180, 180);
        Draw_Preview();
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical();
        //애니메이션 배속
        EditorGUILayout.BeginHorizontal();
        _listSprite._Speed = EditorGUILayout.FloatField("Speed : ", _listSprite._Speed);
        
        EditorGUILayout.EndHorizontal();


        //총 재생시간
        EditorGUILayout.BeginHorizontal();
        _listSprite._Time = EditorGUILayout.FloatField("Play Time : ", _listSprite._Time);
        
        EditorGUILayout.EndHorizontal();
        
        //프레임당 시간(평균)
        EditorGUILayout.BeginHorizontal();
        _listSprite._TimeGap = EditorGUILayout.FloatField("TimeGap : ", _listSprite._TimeGap);
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("R"))
        { }
        if (GUILayout.Button("R"))
        { }
        if (GUILayout.Button("R"))
        { }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    private void Draw_Preview()
    {
        double time = EditorApplication.timeSinceStartup - _previewTime_StartUp;
        int idx = _listSprite.FindIdx_ByTime((float)time);

        if (idx < 0)
        {
            _previewTime_StartUp = EditorApplication.timeSinceStartup;
            idx = 0;
        }

        var previewSprite = _listSprite.GetSprite_ByIdx(idx);

        if (null == previewSprite)
            return;

        int id = GUIUtility.GetControlID(FocusType.Passive, _rectPreview);
        PublicEditorMethods.SpriteField(_rectPreview, previewSprite, id);
    }

    private double _previewTime_StartUp = 0f;

    private void Update()
    {
        if (_IsShowPreview)
        {
            double time = EditorApplication.timeSinceStartup - _previewTime_StartUp;
            Repaint();
        }

    }



}
