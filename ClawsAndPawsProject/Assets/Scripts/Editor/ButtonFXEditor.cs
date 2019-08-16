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
    private SerializedProperty changeSprite;

    private void OnEnable() {
        buttonScript = target as ButtonFX;

        sfxName = serializedObject.FindProperty("sfxName");
        changeSize = serializedObject.FindProperty("changeSize");
        newSize = serializedObject.FindProperty("newSize");
        changeSprite = serializedObject.FindProperty("changeSprite");
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

        EditorGUILayout.BeginHorizontal();

        changeSprite.boolValue = EditorGUILayout.Toggle("Change Sprite: ", buttonScript.changeSprite);

        if (buttonScript.changeSprite) {
            buttonScript.newSprite = (Sprite)EditorGUILayout.ObjectField(buttonScript.newSprite, typeof(Sprite), false);
        }

        EditorGUILayout.EndHorizontal();

        if (GUI.changed) {
            EditorUtility.SetDirty(buttonScript);
            EditorSceneManager.MarkSceneDirty(buttonScript.gameObject.scene);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
