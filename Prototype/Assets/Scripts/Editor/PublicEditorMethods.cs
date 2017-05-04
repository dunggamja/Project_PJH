using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PublicEditorMethods
{
    public static void SpriteField(Rect rect, Sprite sprite, int id)
    {
        if (Event.current.type != EventType.Repaint)
            return;

        if (null == rect)
            return;


        var preTexture = AssetPreview.GetAssetPreview(sprite);
        //var preTexture = sprite.texture;

        if (preTexture)
        {
            EditorGUI.DrawPreviewTexture(rect, preTexture, null, ScaleMode.ScaleToFit);

        }
    }
}
