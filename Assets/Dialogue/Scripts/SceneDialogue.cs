using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;


namespace Dialogue
{
    public class SceneDialogue : SceneGraph
    {
        public Button AnswerButton;

        private DialogueGraph dgraph;
        private GameObject dialogueObject;
        private GameObject dialogueAnswers;
        private GameObject dialogueName;
        private GameObject dialogueImg;
        private GameObject dialogueTxt;
        private int charIndex = 0;
        private string PrintingText = "";
        private double timer = 0;
        private GameObject DialogueArea = null;

        private GameObject InterfaceArea = null;
        //private GameObject dialogueCharacter = null;


        public void SwapPanels()
        {
            dialogueObject.SetActive(false);
            dialogueTxt.GetComponent<Text>().text = "";
            if (dialogueObject.name == "Panel")
            {
                InterfaceArea.SetActive(true);
                InterfaceArea.transform.Find("RightButtons").gameObject.GetComponent<NotebookScript>().ShowNotebook();
                foreach (Transform VARIABLE in InterfaceArea.transform.Find("RightButtons").Find("Notebook")
                    .Find("Panel"))
                {
                    if (VARIABLE.name.StartsWith("Clue"))
                    {
                        VARIABLE.GetComponent<DragHandler>().enabled = true;
                    }
                }

                InterfaceArea.transform.Find("RightButtons").Find("Pause").gameObject.SetActive(false);
//                InterfaceArea.transform.Find("RightButtons").Find("Search").gameObject.SetActive(false);
                InterfaceArea.transform.Find("RightButtons").Find("Notebook").Find("Button").gameObject
                    .SetActive(false);

                dialogueObject = DialogueArea.transform.Find("CrossPanel").gameObject;
                dialogueAnswers.SetActive(false);
                dialogueImg = dialogueObject.transform.Find("Profile").gameObject;
                dialogueName = dialogueObject.transform.Find("Name").gameObject;
                dialogueTxt = dialogueObject.transform.Find("Image").gameObject.transform.Find("Text").gameObject;
            }
            else
            {
                InterfaceArea.transform.Find("RightButtons").gameObject.GetComponent<NotebookScript>().ShowNotebook();
                //InterfaceArea.transform.Find("RightButtons").Find("Pause").gameObject.SetActive(true);
                /*               InterfaceArea.transform.Find("RightButtons").Find("Search").gameObject.SetActive(true);
                               InterfaceArea.transform.Find("RightButtons").Find("Notebook").Find("Button").gameObject.SetActive(true);
               */
                //InterfaceArea.SetActive(false);
                foreach (Transform VARIABLE in InterfaceArea.transform.Find("RightButtons").Find("Notebook")
                    .Find("Panel"))
                {
                    if (VARIABLE.name.StartsWith("Clue"))
                    {
                        VARIABLE.GetComponent<DragHandler>().enabled = false;
                    }
                }

                dialogueObject = DialogueArea.transform.Find("Panel").gameObject;
                dialogueAnswers.SetActive(false);
                dialogueImg = dialogueObject.transform.Find("Profile").gameObject;
                dialogueName = dialogueObject.transform.Find("Name").gameObject;
                dialogueTxt = dialogueObject.transform.Find("Image").gameObject.transform.Find("Text").gameObject;
            }

            dialogueObject.SetActive(true);
        }

        // Start is called before the first frame update
        void Awake()
        {
            //Debug.Log(gameObject.name);
            /*
            foreach (var VARIABLE in gameObject.scene.GetRootGameObjects())
            {
                if (VARIABLE.name == "Canvas")
                {*/
            DialogueArea = GameObject.Find("Canvas").transform.Find("Dialog").gameObject;
            InterfaceArea = GameObject.Find("Canvas").transform.Find("Interface").gameObject;
            /*     }
             }*/

            dgraph = (DialogueGraph) graph;
            dialogueObject = DialogueArea.transform.Find("Panel").gameObject;
            dialogueAnswers = DialogueArea.transform.Find("AnswersPanel").gameObject;
            dialogueAnswers.SetActive(false);
            dialogueImg = dialogueObject.transform.Find("Profile").gameObject;
            dialogueName = dialogueObject.transform.Find("Name").gameObject;
            dialogueTxt = dialogueObject.transform.Find("Image").gameObject.transform.Find("Text").gameObject;

            //StartDialogue();
        }

        public void SetDialogue()
        {
            color = false;
            dialogueObject.SetActive(true);

            HideButton();
            if (dgraph.current == null)
            {
                End();
                return;
            }

            if (dgraph.current.GetType() == typeof(QuestionChat) && dialogueObject.name == "Panel")
            {
                CharacterToLeft(((Chat) dgraph.current).character.name, -1000);
                SwapPanels();
                StartWait(1);
            }
            else if (dgraph.current.GetType() == typeof(Chat) && dialogueObject.name != "Panel")
            {
                CharacterToLeft(dialogueName.GetComponent<Text>().text, 0);
                SwapPanels();
                StartWait(1);
            }

            if (DialogueArea.GetComponent<DialogueController>().controller.name ==
                ((Chat) dgraph.current).character.name)
            {
                DialogueArea.GetComponent<DialogueController>().controller.GetComponent<Animator>()
                    .SetBool("Idle", false);
//                    dialogueCharacter = 
            }

            dialogueName.GetComponent<Text>().text = ((Chat) dgraph.current).character.name;
            //dialogueTxt.GetComponent<Text>().text = ((Chat) dgraph.current).text;
            PrintingText = (GameObject.Find("Profile").GetComponent<Profile>().language == Language.RU)
                ? ((Chat) dgraph.current).text
                : ((Chat) dgraph.current).textEN;
            // dialogueImg.GetComponent<Image>().sprite = ((Chat) dgraph.current).character.sprite;

            foreach (Transform child in dialogueImg.transform)
                Destroy(child.gameObject);
            if (dgraph.current.isPortretShown)
            {
                if (((Chat) dgraph.current).character.portret != null)
                {
                    GameObject portret = Instantiate(((Chat) dgraph.current).character.portret);
                    portret.transform.SetParent(dialogueImg.transform, false);
                    portret.transform.localPosition = Vector3.back;
                }

                dialogueImg.SetActive(true);
            }
            else
            {
                dialogueImg.SetActive(false);
            }

            charIndex = 0;
            if (PrintingText.Equals(""))
            {
                dialogueTxt.GetComponent<Text>().text = PrintingText;
                Next();
                dialogueObject.SetActive(false);
            }
        }

        public void StartDialogue()
        {
            GetComponent<Button>().interactable = false;
            DialogueArea.GetComponent<DialogueController>().controller = gameObject;
            dgraph.Restart();
            SetDialogue();
            DialogueArea.SetActive(true);

            InterfaceArea.SetActive(false);
        }

        public void End()
        {
            if (dialogueObject.name != "Panel")
            {
                CharacterToLeft(dialogueName.GetComponent<Text>().text, 0);
                SwapPanels();
                StartWait(1);
            }

/*            InterfaceArea.transform.Find("RightButtons").Find("Search").gameObject.SetActive(true);
            if (!InterfaceArea.transform.Find("RightButtons").Find("Search").Find("Button").gameObject
                .GetComponent<SearchingButton>().GetSearching())
            {*/
            InterfaceArea.transform.Find("RightButtons").Find("Pause").gameObject.SetActive(true);
            InterfaceArea.transform.Find("RightButtons").Find("Notebook").Find("Button").gameObject.SetActive(true);
/*            }*/

            GetComponent<Button>().interactable = true;
            dialogueTxt.GetComponent<Text>().text = "";
            DialogueArea.SetActive(false);
            InterfaceArea.SetActive(true);
        }

        public void ShowButton()
        {
            if (DialogueArea.GetComponent<DialogueController>().controller.GetComponent<Animator>() != null)
            {
                DialogueArea.GetComponent<DialogueController>().controller.GetComponent<Animator>()
                    .SetBool("Idle", true);
            }

            dialogueObject.transform.Find("Image").Find("Button").gameObject.SetActive(true);
        }

        public void HideButton()
        {
            dialogueObject.transform.Find("Image").Find("Button").gameObject.SetActive(false);
        }

        public void NextClue(string clueName)
        {
            if (dgraph.current.GetType() == typeof(QuestionChat))
            {
                dgraph.ShowClue(clueName);
                SetDialogue();
            }
        }

        private void CharacterToLeft(string name, float x)
        {
            GameObject SceneCharacter = GameObject.Find("Canvas").transform.Find("Background").Find("Characters")
                .Find(name).gameObject;


            float center_of_back = 0;


            float newX = x;
            foreach (Transform VARIABLE in GameObject.Find("Canvas").transform.Find("Background")
                .Find("Backgrounds"))
            {
                if (VARIABLE.localPosition.x - VARIABLE.GetComponent<RectTransform>().rect.width / 2
                    < SceneCharacter.transform.localPosition.x
                    && VARIABLE.localPosition.x + VARIABLE.GetComponent<RectTransform>().rect.width / 2
                    > SceneCharacter.transform.localPosition.x)
                {
                    center_of_back = VARIABLE.localPosition.x;
                }
            }

            SceneCharacter.transform.localPosition = new Vector3(
                center_of_back + newX,
                SceneCharacter.transform.localPosition.y,
                SceneCharacter.transform.localPosition.z);
        }

        public void Next()
        {
            HideButton();
            if (charIndex < PrintingText.Length)
            {
                charIndex = PrintingText.Length;
                dialogueTxt.GetComponent<Text>().text = PrintingText;
                AudioStop();
                ShowButton();
                return;
            }

            if (dgraph.isLast())
            {
                End();
                return;
            }

            if (dgraph.current.answers.Count == 0)
            {
                dgraph.AnswerQuestion(0);
                SetDialogue();
            }
            else
            {
                dialogueAnswers.SetActive(true);
                float koef = AnswerButton.GetComponent<RectTransform>().rect.height * 2;
                float start = (dgraph.current.answers.Count - 1) * 0.5f * koef;
                int i = 0;
                foreach (var answer in dgraph.current.answers)
                {
                    Button s1Button = Instantiate(AnswerButton);

                    //Sets "ChoiceButtonHolder" as the new parent of the s1Button.
                    s1Button.transform.SetParent(dialogueAnswers.transform);
                    s1Button.transform.localPosition = Vector3.up * start;
                    s1Button.transform.localScale = Vector3.one;


                    s1Button.transform.Find("Text").GetComponent<Text>().text =
                        (GameObject.Find("Profile").GetComponent<Profile>().language == Language.RU)
                            ? answer.text
                            : answer.textEN;
                    s1Button.GetComponent<DialogueButton>().i = i;
                    i++;
                    start -= koef;
                }
            }
        }

        public void Next(int i)
        {
            dialogueAnswers.SetActive(false);
            HideButton();
            foreach (Transform child in dialogueAnswers.transform)
                Destroy(child.gameObject);

            //Debug.Log(dgraph.IsAnswerOutputExist(i));
            if (!dgraph.IsAnswerOutputExist(i))
            {
                End();
                return;
            }


            dgraph.AnswerQuestion(i);
            SetDialogue();
        }


        private int t;
        bool isGameStopped = false;

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(t);

            isGameStopped = false;
            DialogueArea.SetActive(true);
        }

        public void StartWait(int _t)
        {
            t = _t;
            isGameStopped = true;

            //Debug.Log(dialogueObject.name);
            DialogueArea.SetActive(false);
            dialogueTxt.GetComponent<Text>().text = "";
            StartCoroutine("Wait");
        }

        void AudioStop()
        {
            if (gameObject.name.Equals(dgraph.current.character.name))
            {
                if (gameObject.GetComponent<AudioSource>() != null)
                {
                    gameObject.GetComponent<AudioSource>().Stop();
                }
            }
            else
            {
                if (dgraph.current.isPortretShown)
                {
                    if (dialogueImg.transform.GetChild(0).gameObject != null)
                        if (dialogueImg.transform.GetChild(0).gameObject.GetComponent<AudioSource>() != null)
                        {
                            dialogueImg.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                        }
                }
                else
                {
                    Transform OtherChart = gameObject.transform.parent.Find(dgraph.current.character.name);
                    if (OtherChart != null)
                    {
                        GameObject OtherChar = OtherChart.gameObject;
                        if (OtherChar != null)
                        {
                            if (OtherChar.GetComponent<AudioSource>() != null)
                            {
                                OtherChar.GetComponent<AudioSource>().Stop();
                            }
                        }
                    }
                    else
                    {
                        if (DialogueArea.GetComponent<AudioSource>() != null)
                        {
                            DialogueArea.GetComponent<AudioSource>().Stop();
                        }
                    }
                }
            }
        }

        void AudioPlay()
        {
            if (gameObject.name.Equals(dgraph.current.character.name))
            {
                if (gameObject.GetComponent<AudioSource>() != null)
                {
                    if (!gameObject.GetComponent<AudioSource>().isPlaying)
                        gameObject.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                if (dgraph.current.isPortretShown)
                {
                    if (dialogueImg.transform.GetChild(0).gameObject != null)
                        if (dialogueImg.transform.GetChild(0).gameObject.GetComponent<AudioSource>() != null)
                        {
                            if (!dialogueImg.transform.GetChild(0).gameObject.GetComponent<AudioSource>().isPlaying)
                                dialogueImg.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                        }
                }
                else
                {
                    Transform OtherChart = gameObject.transform.parent.Find(dgraph.current.character.name);
                    if (OtherChart != null)
                    {
                        GameObject OtherChar = OtherChart.gameObject;
                        if (OtherChar != null)
                        {
                            if (OtherChar.GetComponent<AudioSource>() != null)
                            {
                                if (!OtherChar.GetComponent<AudioSource>().isPlaying)
                                    OtherChar.GetComponent<AudioSource>().Play();
                            }
                        }
                    }
                    else
                    {
                        if (DialogueArea.GetComponent<AudioSource>() != null)
                        {
                            if (!DialogueArea.GetComponent<AudioSource>().isPlaying)
                                DialogueArea.GetComponent<AudioSource>().Play();
                        }
                    }
                }
            }
        }

        bool color = false;

        // Update is called once per frame
        void Update()
        {
            if (isGameStopped && dialogueObject.activeSelf)
            {
                return;
            }

            if (charIndex < PrintingText.Length)
            {
                AudioPlay();

                timer -= Time.deltaTime;
                while (timer <= 0f)
                {
                    timer += 0.1 / ((Chat) dgraph.current).SpeechSpeed;
                    if (PrintingText.Substring(charIndex).StartsWith("<"))
                    {
                        if (PrintingText.Substring(charIndex).StartsWith("<color=#"))
                        {
                            charIndex += "<color=#000000>".Length;
                            color = true;
                        }
                        else if (PrintingText.Substring(charIndex).StartsWith("</color>"))
                        {
                            charIndex += "</color>".Length;
                            color = false;
                        }
                    }

                    charIndex++;

                    if (PrintingText.Substring(charIndex).StartsWith("<"))
                    {
                        if (PrintingText.Substring(charIndex).StartsWith("<color=#"))
                        {
                            charIndex += "<color=#000000>".Length;
                            color = true;
                        }
                        else if (PrintingText.Substring(charIndex).StartsWith("</color>"))
                        {
                            charIndex += "</color>".Length;
                            color = false;
                        }
                    }
                    dialogueTxt.GetComponent<Text>().text = PrintingText.Substring(0, charIndex);
                    if (color)
                    {
                        dialogueTxt.GetComponent<Text>().text += "</color>";
                    }

                    dialogueTxt.GetComponent<Text>().text +=
                        "<color=#00000000>" + PrintingText.Substring(charIndex).Replace("</color>","") + "</color>";
                    if (charIndex >= PrintingText.Length)
                    {
                        ShowButton();
                        AudioStop();
                        return;
                    }
                }
            }
        }
    }
}