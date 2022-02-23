using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(Dialogue.SetPose))]
    public class SetPoseEditor : NodeEditor {

        public override void OnBodyGUI() {
            serializedObject.Update();

            Dialogue.SetPose node = target as Dialogue.SetPose;
            if (NodeEditorWindow.current.zoom > 3)
            {
                serializedObject.ApplyModifiedProperties();
                return;
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("character"), GUIContent.none);
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("pose"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("place"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("distance"));
            
            NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            EditorGUILayout.Space();
            NodeEditorGUILayout.PortField(target.GetOutputPort("pass"));
            
            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth() {
            return 250;
        }
    }
}