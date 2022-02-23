using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
    [NodeTint("#FFFFAA")]
    public class Event : DialogueBaseNode
    {
        public SerializableEvent[]
            trigger = new SerializableEvent[1];// Could use UnityEvent here, but UnityEvent has a bug that prevents it from serializing correctly on custom EditorWindows. So i implemented my own.
       
        [Output] public DialogueBaseNode pass;

        /*Event()
        {
            trigger[0].target = GameObject.Find("ProfileManager").GetComponent<ProfileManager>();

        }*/
        
        /*private static SerializableEvent Result()
        {
            SerializableEvent tr = new SerializableEvent();
            tr.target = GameObject.Find("ProfileManager").GetComponent<ProfileManager>();

            return tr;
        }*/

        public override void Trigger()
        {
            for (int i = 0; i < trigger.Length; i++)
            {
                trigger[i].Invoke();
            }

            //Trigger next nodes
            NodePort port;
            port = GetOutputPort("pass");
            if (port == null)
            {
                return;
            }

            if (port.ConnectionCount == 0)
            {
                (graph as DialogueGraph).current = null;
            }
            
            for (int i = 0; i < port.ConnectionCount; i++)
            {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }
        }
    }
}