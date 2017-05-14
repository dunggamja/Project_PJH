using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteEditorWindow : EditorWindow {

    static SpriteEditorWindow _editorWindow = null;
    public static void Open()
    {
        if(null == _editorWindow) _editorWindow = CreateInstance<SpriteEditorWindow>();

        _editorWindow.ShowUtility();
    }

    private void OnGUI()
    {
        if (Event.current.keyCode == KeyCode.Escape)
            _editorWindow.Close();

        Process_DragDropObjects();

        Draw_SpriteList();
    }

    private Vector2 _scrollPos = Vector2.zero;

    private void Draw_SpriteList()
    {
        Vector2 startPos = Vector2.zero;

        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, true, false, GUILayout.MinWidth(500), GUILayout.MaxHeight(250), GUILayout.ExpandHeight(true));
        GUILayout.BeginHorizontal();
        if (null != _sprDataContainer)
        {
            GUILayout.Button("");
            for (int i = 0; i < _sprDataContainer._listSprData.Count; ++i)
            {
                GUILayout.Button("button : " + i.ToString(), GUILayout.Width(190), GUILayout.Height(190));
                //var lastRect = GUILayoutUtility.GetLastRect();
                //PublicEditorMethods.SpriteField(lastRect, _sprDataContainer.GetSprite_ByIdx(i), 0);
            }
        }
        
        GUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }


    private void Process_DragDropObjects()
    {
        switch (Event.current.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (0 < DragAndDrop.objectReferences.Length)
                    DragAndDrop.AcceptDrag();

                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;

                break;
            case EventType.DragExited:
                if (0 < DragAndDrop.objectReferences.Length)
                {
                    
                    foreach (Object obj in DragAndDrop.objectReferences)
                    {
                        Sprite spr = obj as Sprite;
                        if (null == spr)
                            continue;

                        _sprDataContainer.AddData(spr);
                    }
                }
                break;
        }
    }

    private SpriteDataContainer _sprDataContainer = new SpriteDataContainer();

    public void GetSpriteData_From_SpriteManager(string key)
    {
        if (SpriteManager.Instance._dicSpriteContainer.ContainsKey(key))
        {
            _sprDataContainer = SpriteManager.Instance._dicSpriteContainer[key];
        }
        else
        {
            _sprDataContainer = null;
        }
    }





}
