using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MainControl))]
public class MainControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MainControl script = (MainControl)target;

        // Condition Navigation Section
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Condition Navigation", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Previous"))
        {
            script.PreviousCondition();
        }

        if (GUILayout.Button("Next"))
        {
            script.NextCondition();
        }
        EditorGUILayout.EndHorizontal();

        // Object Control Section
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Object Control", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Show All"))
        {
            script.AppearAllObjectsInCurrentCondition();
        }

        if (GUILayout.Button("Hide All"))
        {
            script.HideAllObjectsInCurrentCondition();
        }
        if (GUILayout.Button("Reset All"))
        {
            script.ResetAllObjectsToDefault();
        }
        EditorGUILayout.EndHorizontal();

        // Individual Object Control with Reset Button
        GUILayout.Space(10);
        if (script.Conditions != null && script.Conditions.Count > 0 && script.Conditions[script.currentConditionIndex].Objects != null)
        {
            EditorGUILayout.LabelField("Individual Objects:", EditorStyles.boldLabel);
            foreach (var objState in script.Conditions[script.currentConditionIndex].Objects)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(objState.Object, typeof(GameObject), true);

                if (GUILayout.Button("Show"))
                {
                    script.ToggleObjectVisibility(objState.Object, true);
                }

                if (GUILayout.Button("Hide"))
                {
                    script.ToggleObjectVisibility(objState.Object, false);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
