using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

[System.Serializable]
public enum ScenePosition
{
    НЕ_МЕНЯТЬ,
    СЛЕВА,
    ПО_ЦЕНТРУ,
    СПРАВА
}

namespace Dialogue
{
    [NodeTint("#FFFFAA")]
    public class SetPose : DialogueBaseNode
    {
        public CharacterInfo character;
        [Output] public DialogueBaseNode pass;
        public string pose;
        public ScenePosition place;
        public float distance = 1000;
        public int scenePlace = 0;

        public override void Trigger()
        {
            GameObject SceneCharacter = GameObject.Find("Canvas").transform.Find("Background").Find("Characters")
                .Find(character.name).gameObject;
            if (SceneCharacter != null)
            {
                if (place != ScenePosition.НЕ_МЕНЯТЬ)
                {
                    float center_of_back = 0;
                    

                    float newX = 0;
                    if (scenePlace != 0)
                    {
                        foreach (Transform VARIABLE in GameObject.Find("Canvas").transform.Find("Background")
                            .Find("Backgrounds"))
                        {
                            if (scenePlace == 1)
                            {
                                center_of_back += VARIABLE.localPosition.x;
                                break;
                            }
                            else
                            {
                                scenePlace--;
                            }
                        }
                    }
                    else
                    {
                        foreach (Transform VARIABLE in GameObject.Find("Canvas").transform.Find("Background")
                            .Find("Backgrounds"))
                        {
                            if (VARIABLE.localPosition.x - VARIABLE.GetComponent<RectTransform>().rect.width / 2 
                                < SceneCharacter.transform.localPosition.x
                                && VARIABLE.localPosition.x + VARIABLE.GetComponent<RectTransform>().rect.width / 2 
                                > SceneCharacter.transform.localPosition.x)
                            {
                                center_of_back = VARIABLE.localPosition.x;
                                break;
                            }
                        }
                    }

                    switch (place)
                    {
                        case ScenePosition.СЛЕВА:
                            newX += -distance;
                            break;
                        case ScenePosition.СПРАВА:
                            newX += distance;
                            break;
                        case ScenePosition.ПО_ЦЕНТРУ:
                            newX += 0;
                            break;
                    }

                    SceneCharacter.transform.localPosition = new Vector3(
                        center_of_back + newX,
                        SceneCharacter.transform.localPosition.y,
                        SceneCharacter.transform.localPosition.z);
                }

                foreach (var VARIABLE in character.poses)
                {
                    if (VARIABLE.poseName == pose)
                    {
                        SceneCharacter.GetComponent<Image>().sprite = VARIABLE.sprite;
                        break;
                    }
                }
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