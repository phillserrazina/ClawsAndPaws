using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(ButtonFX)), CanEditMultipleObjects]
public class ButtonFXEditor : Editor
{
    private ButtonFX buttonScript;

    private SerializedProperty sfxName;
    private SerializedProperty changeSize;
    private SerializedProperty newSize;
    private SerializedProperty keepSizeOnClick;
    private SerializedProperty changeSprite;
    private SerializedProperty keepSpriteOnClick;

    private void OnEnable() {
        buttonScript = target as ButtonFX;

        sfxName = serializedObject.FindProperty("sfxName");
        changeSize = serializedObject.FindProperty("changeSize");
        newSize = serializedObject.FindProperty("newSize");
        keepSizeOnClick = serializedObject.FindProperty("keepSizeOnClick");
        changeSprite = serializedObject.FindProperty("changeSprite");
        keepSpriteOnClick = serializedObject.FindProperty("keepSpriteOnClick");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        sfxName.stringValue = EditorGUILayout.TextField("SFX Name: ", buttonScript.sfxName);

        EditorGUILayout.BeginHorizontal();
        changeSize.boolValue = EditorGUILayout.Toggle("Change Size: ", buttonScript.changeSize);

        if (buttonScript.changeSize) {
            newSize.floatValue = EditorGUILayout.FloatField(buttonScript.newSize);
        }
        EditorGUILayout.EndHorizontal();

        if (buttonScript.changeSize) {
            keepSizeOnClick.boolValue = EditorGUILayout.Toggle("Keep Size On Click: ", buttonScript.keepSizeOnClick);
        }

        EditorGUILayout.BeginHorizontal();

        changeSprite.boolValue = EditorGUILayout.Toggle("Change Sprite: ", buttonScript.changeSprite);

        if (buttonScript.changeSprite) {
            buttonScript.newSprite = (Sprite)EditorGUILayout.ObjectField(buttonScript.newSprite, typeof(Sprite), false);
        }

        EditorGUILayout.EndHorizontal();

        if (buttonScript.changeSprite) {
            keepSpriteOnClick.boolValue = EditorGUILayout.Toggle("Keep Sprite On Click: ", buttonScript.keepSpriteOnClick);
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(buttonScript);
            EditorSceneManager.MarkSceneDirty(buttonScript.gameObject.scene);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
