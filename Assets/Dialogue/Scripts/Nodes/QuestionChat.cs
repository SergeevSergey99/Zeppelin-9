using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Dialogue
{
    
    [NodeTint("#4444FF")]
    public class QuestionChat: Chat
    {
        [Output(instancePortList = true)] public List<ShownObject> shownObjects = new List<ShownObject>();

        [Output] public DialogueBaseNode defaultForObject;
        [System.Serializable] public class ShownObject {
            public string clueName;
        }
        
       /* public bool IsShownObjectOutputExist(int index)
        {
            NodePort port = null;
            if (shownObjects.Count == 0) {
                port = GetOutputPort("output");
            } else {
                if (shownObjects.Count <= index) return false;
                port = GetOutputPort("shownObjects " + index);
            }

            if (port == null) return false;
            if (port.ConnectionCount == 0) return false;
            return true;
        }*/
       public void ShowClue(string clueName)
       {
           NodePort port = null;
           if (shownObjects.Count == 0) {
               port = GetOutputPort("defaultForObject");
           }
           else {
               int i = 0;
               foreach (var VARIABLE in shownObjects)
               {
                   
                   if (VARIABLE.clueName == clueName)
                   {
                       port = GetOutputPort("shownObjects " + i);
                       //Debug.Log(port.ConnectionCount);
                       break;
                   }
                   i++;
               }
               if (port == null) port = GetOutputPort("defaultForObject");
           }
           
           if (port == null) return;
           for (int i = 0; i < port.ConnectionCount; i++) {
               NodePort connection = port.GetConnection(i);
               (connection.node as DialogueBaseNode).Trigger();
           }
       } 
    }
}