using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using XNode;

namespace Dialogue
{
    [NodeTint("#CC77FF")]
    public class FlagOption : DialogueBaseNode
    {
        public bool StartFirstRised = true;

        [Output] public DialogueBaseNode defaultLine;

        //public FlagVariable[] conditions;
        [Output(instancePortList = true)] public List<FlagList> answers = new List<FlagList>();

        [System.Serializable] public class FlagList {
            public List<FlagVariable> flags;
        }

        public override void Trigger()
        {
            // Perform condition
            GameObject pm = GameObject.Find("ProfileManager");

            int risedIndex = -1;
            if (StartFirstRised)
            {
                for (int i = 0; i < answers.Count; i++)
                {
                    bool isAllFlags = true;
                    foreach (var flag in answers[i].flags)
                    {
                        if (!(pm.GetComponent<ProfileManager>().isFlagExist(flag.name) && 
                            pm.GetComponent<ProfileManager>().IsFlagRaised(flag.name) == flag.isRaised))
                        
                        {
                            isAllFlags = false;
                        }
                        
                    }

                    if (isAllFlags)
                    {
                        risedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = answers.Count - 1; i >= 0; i--)
                {
                    bool isAllFlags = true;
                    foreach (var flag in answers[i].flags)
                    {
                        if (!(pm.GetComponent<ProfileManager>().isFlagExist(flag.name) && 
                              pm.GetComponent<ProfileManager>().IsFlagRaised(flag.name) == flag.isRaised))
                        
                        {
                            isAllFlags = false;
                        }
                        
                    }

                    if (isAllFlags)
                    {
                        risedIndex = i;
                        break;
                    }
                }
            }

            NodePort port = null;
            if (risedIndex == -1)
            {
                port = GetOutputPort("defaultLine");
            }
            else
            {
                if (answers.Count <= risedIndex) return;
                port = GetOutputPort("answers " + risedIndex);
            }

            if (port == null) return;
            for (int i = 0; i < port.ConnectionCount; i++)
            {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }

            //Trigger next nodes
            /*
            NodePort port;
            if (success) port = GetOutputPort("pass");
            else port = GetOutputPort("fail");
            if (port == null) return;
            for (int i = 0; i < port.ConnectionCount; i++) {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }*/
        }
    }
}