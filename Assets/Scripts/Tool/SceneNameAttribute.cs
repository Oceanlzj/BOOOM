using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneNameAttribute : PropertyAttribute
{
    public string[] NameList => AllSceneNames();

    public static string[] AllSceneNames()
    {
        List<string> list = new List<string>();
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (EditorBuildSettingsScene editorBuildSettingsScene in scenes)
        {
            if (editorBuildSettingsScene.enabled)
            {
                string text = editorBuildSettingsScene.path.Substring(editorBuildSettingsScene.path.LastIndexOf('/') + 1);
                text = text.Substring(0, text.Length - 6);
                list.Add(text);
            }
        }

        return list.ToArray();
    }
}

[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] nameList = (base.attribute as SceneNameAttribute).NameList;
        if (property.propertyType == SerializedPropertyType.String)
        {
            int num = Mathf.Max(0, Array.IndexOf<string>(nameList, property.stringValue));
            num = EditorGUI.Popup(position, property.displayName, num, nameList);
            property.stringValue = nameList[num];
            return;
        }
        if (property.propertyType == SerializedPropertyType.Generic)
        {
            property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, nameList);
            return;
        }
        base.OnGUI(position, property, label);
    }
}
