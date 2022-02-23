using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/Graph", order = 0)]
    public class DialogueGraph : NodeGraph
    {
        [HideInInspector] public Chat current;

        //public bool active = true;
        public void Restart()
        {
            //Find the first DialogueNode without any inputs. This is the starting node.
            if (nodes.Find(x => x is DialogueBaseNode && x.Inputs.All(y => !y.IsConnected)) is Chat)
                current = nodes.Find(x => x is DialogueBaseNode && x.Inputs.All(y => !y.IsConnected)) as Chat;
            /*if (nodes.Find(x => x is DialogueBaseNode && x.Inputs.All(y => !y.IsConnected)) is FlagOption)
            {
                (nodes.Find(x => x is DialogueBaseNode && x.Inputs.All(y => !y.IsConnected)) as FlagOption)?.Trigger();
            }*/
            else
            {
                (nodes.Find(x => x is DialogueBaseNode && x.Inputs.All(y => !y.IsConnected)) as DialogueBaseNode)
                    ?.Trigger();
            }
        }

        public bool isLast()
        {
            return current.Outputs.All(y => !y.IsConnected);
        }

        public bool IsAnswerOutputExist(int i)
        {
            return current.IsAnswerOutputExist(i);
        }

        public Chat ShowClue(string clueName)
        {
            if (current.GetType() == typeof(QuestionChat))
            {
                ((QuestionChat)current).ShowClue(clueName);
            }

            return current;
        }

        public Chat AnswerQuestion(int i)
        {
            //        Chat tmp = current;
            //Debug.Log(current);
            current.AnswerQuestion(i);
//            if (current == tmp)
            //              active = false;
            return current;
        }
    }
}