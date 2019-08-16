using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CanEditMultipleObjects]
[CustomEditor(typeof(ButtonFX))]
public class ButtonFXEditor : Editor
{
    private ButtonFX buttonScript;

    private void OnEnable() {
        buttonScript = target as ButtonFX;
    }

    public override void OnInspectorGUI() {
        buttonScript.sfxName = EditorGUILayout.TextField("SFX Name: ", buttonScript.sfxName);

        EditorGUILayout.BeginHorizontal();
        buttonScript.changeSize = EditorGUILayout.Toggle("Change Size: ", buttonScript.changeSize);

        if (buttonScript.changeSize) {
            buttonScript.newSize = EditorGUILayout.FloatField(buttonScript.newSize);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        buttonScript.changeSprite = EditorGUILayout.Toggle("Change Sprite: ", buttonScript.changeSprite);

        if (buttonScript.changeSprite) {
            buttonScript.newSprite = (Sprite)EditorGUILayout.ObjectField(buttonScript.newSprite, typeof(Sprite), false);
        }

        EditorGUILayout.EndHorizontal();

        if (GUI.changed) {
            EditorUtility.SetDirty(buttonScript);
            EditorSceneManager.MarkSceneDirty(buttonScript.gameObject.scene);
        }
    }
}
