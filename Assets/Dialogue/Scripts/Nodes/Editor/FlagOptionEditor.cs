using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace DialogueEditor
{
    [CustomNodeEditor(typeof(Dialogue.FlagOption))]
    public class FlagOptionEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            serializedObject.Update();

            Dialogue.FlagOption node = target as Dialogue.FlagOption;

            if (NodeEditorWindow.current.zoom > 3)
            {
                serializedObject.ApplyModifiedProperties();
                return;
            }

            UnityEditor.EditorGUILayout.LabelField("Начать с первой выполненной комбинации флагов");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("StartFirstRised"));
            //NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            EditorGUILayout.Space();

            if (node.answers.Count == 0)
            {
                GUILayout.BeginHorizontal();
                NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
                NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
                GUILayout.EndHorizontal();
            }
            else
            {
                NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"));
            }

            GUILayout.Space(-30);


            NodeEditorGUILayout.PortField(target.GetOutputPort("defaultLine"));
            NodeEditorGUILayout.InstancePortList("answers", typeof(DialogueBaseNode), serializedObject,
                NodePort.IO.Output, Node.ConnectionType.Override);

            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth()
        {
            return 350;
        }
    }
}