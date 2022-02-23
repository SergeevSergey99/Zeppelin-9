using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor
{
    [CustomNodeEditor(typeof(Dialogue.Event))]
    public class EventEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            serializedObject.Update();

            Dialogue.Event node = target as Dialogue.Event;
            if (NodeEditorWindow.current.zoom > 3)
            {
                serializedObject.ApplyModifiedProperties();
                return;
            }

            NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            EditorGUILayout.Space();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("trigger"));
            NodeEditorGUILayout.PortField(target.GetOutputPort("pass"));

            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth()
        {
            return 336;
        }
    }
}