using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Dialogue;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ProfileManager : MonoBehaviour
{
    public GameObject profile = null;
    private GameObject CanvasArea;

    private void Awake()
    {
        foreach (var VARIABLE in gameObject.scene.GetRootGameObjects())
        {
            if (VARIABLE.name == "Canvas")
            {
                CanvasArea = VARIABLE.gameObject;
            }
        }

        profile = GameObject.FindGameObjectsWithTag("Player")[0];
        int cnt = 0;
        foreach (Transform VARIABLE in CanvasArea.transform.Find("Interface").Find("RightButtons").Find("Notebook").Find("Panel"))
        {
            if (VARIABLE.gameObject.name.StartsWith("Clue"))
            {
                cnt++;
            }
        }
        profile.GetComponent<Profile>().setCLueCountPerPage(cnt);

        if (CanvasArea.transform.Find("Background").Find("Clues") != null) {
            foreach(Transform VARIABLE in CanvasArea.transform.Find("Background").Find("Clues"))
        {
            if(isClueHaveBeenFound(VARIABLE.gameObject.GetComponent<Clue>().name))
            {
                VARIABLE.gameObject.GetComponent<Clue>().isSeen = true;

                var colors = VARIABLE.gameObject.GetComponent<Image>().color;
                colors = new Color(
                colors.r,
                colors.g,
                colors.b,
                0.5f);

                VARIABLE.gameObject.GetComponent<Image>().color = colors;
            }
        }
        }


        profile.GetComponent<Profile>().UpdateNotebook();
        buttonCheck();
    }

    public void buttonCheck()
    {
        if(CanvasArea.transform.Find("Background").Find("Backgrounds").childCount <= 1)
            return;
        CanvasArea.transform.Find("Interface").Find("RightButton").gameObject.SetActive(true);
        CanvasArea.transform.Find("Interface").Find("LeftButton").gameObject.SetActive(true);

        bool a = false;
        foreach (Transform VARIABLE in CanvasArea.transform.Find("Background").Find("Backgrounds"))
        {
            
            if (VARIABLE.gameObject.GetComponent<RectTransform>().localPosition.x < -CanvasArea.transform
                .Find("Background").gameObject
                .GetComponent<RectTransform>().localPosition.x)
            {
                a = true;
            }
        }
        if (!a)CanvasArea.transform.Find("Interface").Find("LeftButton").gameObject.SetActive(false);
        a = false;
        foreach (Transform VARIABLE in CanvasArea.transform.Find("Background").Find("Backgrounds"))
        {
            
            if (VARIABLE.gameObject.GetComponent<RectTransform>().localPosition.x > -CanvasArea.transform
                .Find("Background").gameObject
                .GetComponent<RectTransform>().localPosition.x)
            {
                a = true;
            }
        }
        if (!a)CanvasArea.transform.Find("Interface").Find("RightButton").gameObject.SetActive(false);
    }
    public void Load_Level(string level)
    {
        SceneManager.LoadScene(level);
    }

   
    public void MoveRight(bool animation = false)
    {
        CanvasArea.transform.Find("Interface").Find("RightButton").gameObject.SetActive(false);
        CanvasArea.transform.Find("Interface").Find("LeftButton").gameObject.SetActive(false);
        
        bool move = false;
        Vector3 newPos = CanvasArea.transform.Find("Background").gameObject.GetComponent<RectTransform>().localPosition;
        

        foreach (Transform VARIABLE in CanvasArea.transform.Find("Background").Find("Backgrounds"))
        {
            
            if (VARIABLE.gameObject.GetComponent<RectTransform>().localPosition.x > -CanvasArea.transform
                .Find("Background").gameObject
                .GetComponent<RectTransform>().localPosition.x)
            {
                move = true;
                newPos -= Vector3.right * CanvasArea.transform.Find("Background").gameObject.GetComponent<RectTransform>()
                        .rect.width;
                
                break;
            }
        }

        if (move)
        {
            if (animation)
            {
                CanvasArea.transform.Find("Background").gameObject.GetComponent<Mover>().setMover(newPos.x);
                return;
            }
            
            CanvasArea.transform.Find("Background").gameObject.GetComponent<RectTransform>().localPosition = newPos;
            
        }
        buttonCheck();
    }

    public void MoveLeft(bool animation = false)
    {

        CanvasArea.transform.Find("Interface").Find("RightButton").gameObject.SetActive(false);
        CanvasArea.transform.Find("Interface").Find("LeftButton").gameObject.SetActive(false);

        
        bool move = false;
        Vector3 newPos = CanvasArea.transform.Find("Background").gameObject.GetComponent<RectTransform>().localPosition;

        foreach (Transform VARIABLE in CanvasArea.transform.Find("Background").Find("Backgrounds"))
        {
            if (VARIABLE.gameObject.GetComponent<RectTransform>().localPosition.x < -CanvasArea.transform
                .Find("Background").gameObject
                .GetComponent<RectTransform>().localPosition.x)
            {
                move = true;
                newPos += Vector3.right * CanvasArea.transform.Find("Background").gameObject.GetComponent<RectTransform>()
                    .rect.width;
                
                break;
            }
        } 
        
        if (move)
        {
            if (animation)
            {
                CanvasArea.transform.Find("Background").gameObject.GetComponent<Mover>().setMover(newPos.x);
                return;
            }
            
            CanvasArea.transform.Find("Background").gameObject.GetComponent<RectTransform>().localPosition = newPos;
            
        }
        buttonCheck();
    }

    public void StartCrossExamination(bool nothing)
    {
            CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<SceneDialogue>().SwapPanels();
    }

    public void NextNotebookPage()
    {
        profile.GetComponent<Profile>().NextPageInNotebook();
    }
    public void PreviousNotebookPage()
    {
        profile.GetComponent<Profile>().PreviousPageInNotebook();
    }
    public void ShowClue(string clueName)
    {
        CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<SceneDialogue>().NextClue(clueName);
    }

    public void Wait(int seconds = 1)
    {
        CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<SceneDialogue>().StartWait(seconds);
    }
    public void HideCharacter(string name)
    {
        GameObject c = CanvasArea.transform.Find("Background").Find("Characters").Find(name).gameObject;
        if (c != null)
        {
            c.SetActive(false);
        }
    }
    public void ShowCharacter(string name)
    {
        GameObject c = CanvasArea.transform.Find("Background").Find("Characters").Find(name).gameObject;
        if (c != null)
        {
            c.SetActive(true);
        }
    }
    public void Zoom(bool zoom = true)
    {
        CanvasArea.transform.Find("ZoomPanel").gameObject.SetActive(zoom);
        if(CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Clue>().AlternativeImage == null)
        {
            CanvasArea.transform.Find("ZoomPanel").Find("Image").gameObject.GetComponent<Image>().sprite =
            CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Image>()
                .sprite;
            CanvasArea.transform.Find("ZoomPanel").Find("Image").gameObject.GetComponent<Image>().color =
                CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Image>()
                    .color;
        }
        else
        {
            CanvasArea.transform.Find("ZoomPanel").Find("Image").gameObject.GetComponent<Image>().sprite =
                CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Clue>()
                    .AlternativeImage;
        }
    }

    public void removeAllClues(bool nothing)
    {
        profile.GetComponent<Profile>().Clues.Clear();
        profile.GetComponent<Profile>().UpdateNotebook();
    }

    public void AddToCluesList(bool setNewAlpha = true)
    {
        CanvasArea.transform.Find("ZoomPanel").gameObject.SetActive(false);
        if (CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Clue>()
            .isSeen)
        {
            return;
        }

        var VARIABLE = profile;

        FoundClue findedClue;
        findedClue.name = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller
            .GetComponent<Clue>().name;
        findedClue.nameEN = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller
            .GetComponent<Clue>().nameEN;
        
        findedClue.description = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>()
            .controller.GetComponent<Clue>().description;
        findedClue.descriptionEN = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>()
            .controller.GetComponent<Clue>().descriptionEN;
        findedClue.sprite = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller
            .GetComponent<Image>().sprite;
        findedClue.color = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller
            .GetComponent<Image>().color;

        if (CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Clue>().AlternativeImage != null)
        {
            findedClue.sprite = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller
                .GetComponent<Clue>().AlternativeImage;
            findedClue.color = Color.white;
        }

        VARIABLE.GetComponent<Profile>().Clues.Add(findedClue);
        VARIABLE.GetComponent<Profile>().UpdateNotebook();
        VARIABLE.GetComponent<Profile>().seenObjects.Add(gameObject.scene.name + " " +
                                                         CanvasArea.transform.Find("Dialog")
                                                             .GetComponent<DialogueController>().controller
                                                             .name + " " +
                                                         CanvasArea.transform.Find("Dialog")
                                                             .GetComponent<DialogueController>().controller
                                                             .GetComponent<Clue>().name);
        if (setNewAlpha)
        {
            var colors = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller
                .GetComponent<Image>().color;
            colors = new Color(
                colors.r,
                colors.g,
                colors.b,
                0.5f);

            CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller
                .GetComponent<Image>().color = colors;
        }

        CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Clue>()
            .isSeen = true;
    }

    public void AddToSeenList(bool nothing = true)
    {
        CanvasArea.transform.Find("ZoomPanel").gameObject.SetActive(false);
        if (CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Clue>()
            .isSeen)
        {
            return;
        }

        foreach (var VARIABLE in gameObject.scene.GetRootGameObjects())
        {
            if (VARIABLE.name == "Profile")
            {
                VARIABLE.GetComponent<Profile>().seenObjects.Add(gameObject.scene.name + " " +
                                                                 CanvasArea.transform.Find("Dialog")
                                                                     .GetComponent<DialogueController>().controller
                                                                     .name + " " +
                                                                 CanvasArea.transform.Find("Dialog")
                                                                     .GetComponent<DialogueController>().controller
                                                                     .GetComponent<Clue>().name);

                var colors = CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller
                    .GetComponent<Image>().color;
                colors = new Color(
                    colors.r,
                    colors.g,
                    colors.b,
                    0.5f);
                GetComponent<Image>().color = colors;
                CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<Clue>()
                    .isSeen = true;
            }
        }
    }

    public void ShowClueEvent(string clue)
    {
        foreach (FoundClue VARIABLE in profile.GetComponent<Profile>().Clues)
        {
            if (VARIABLE.name == clue)
            {

                CanvasArea.transform.Find("EventWindow").gameObject.SetActive(true);
                CanvasArea.transform.Find("EventWindow").Find("ClueInfo").gameObject.SetActive(true);
                
                CanvasArea.transform.Find("EventWindow").Find("ClueInfo").Find("Name").gameObject.GetComponent<Text>().text = 
                    (GameObject.Find("Profile").GetComponent<Profile>().language == Language.RU)? VARIABLE.name: VARIABLE.nameEN;
                CanvasArea.transform.Find("EventWindow").Find("ClueInfo").Find("Text").gameObject.GetComponent<Text>().text =
                    (GameObject.Find("Profile").GetComponent<Profile>().language == Language.RU)? VARIABLE.description: VARIABLE.descriptionEN;;
                
                
                CanvasArea.transform.Find("EventWindow").Find("ClueInfo").Find("Image").gameObject.GetComponent<Image>().sprite = VARIABLE.sprite;
                CanvasArea.transform.Find("EventWindow").Find("ClueInfo").gameObject.GetComponent<Animator>()
                    .Play("EventMoveToCenter");
                break;
            }
        }

    }
    public void ShowInfoEvent(string Name, string Describtion)
    {
        
        CanvasArea.transform.Find("EventWindow").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("ShowInfo").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("ShowInfo").Find("Name").gameObject.GetComponent<Text>().text = Name;
        CanvasArea.transform.Find("EventWindow").Find("ShowInfo").Find("Text").gameObject.GetComponent<Text>().text = Describtion;
        CanvasArea.transform.Find("EventWindow").Find("ShowInfo").gameObject.GetComponent<Animator>()
            .Play("EventMoveToCenter");
    }

    public void UpdateClueName(string ClueName, string NewName)
    {
        int index = -1;

        index = profile.GetComponent<Profile>().Clues.FindIndex(x => x.name == ClueName);

        if (index == -1)
            return;

        FoundClue f = profile.GetComponent<Profile>().Clues[index];
        f.name = NewName;
        profile.GetComponent<Profile>().Clues[index] = f;
        
        CanvasArea.transform.Find("EventWindow").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("UpdateClue").gameObject.SetActive(true);

        if (GameObject.Find("Profile").GetComponent<Profile>().language == Language.RU)
        {
            CanvasArea.transform.Find("EventWindow").Find("UpdateClue").Find("Text").gameObject.GetComponent<Text>()
                .text = "Улика \"" + ClueName + "\" теперь имеет новое название \"" + NewName + "\"";
        }
        else if (GameObject.Find("Profile").GetComponent<Profile>().language == Language.EN)
        {
            CanvasArea.transform.Find("EventWindow").Find("UpdateClue").Find("Text").gameObject.GetComponent<Text>()
                .text = "Clue \"" + ClueName + "\" now have new name \"" + NewName + "\"";
            
        }

        CanvasArea.transform.Find("EventWindow").Find("UpdateClue").gameObject.GetComponent<Animator>()
            .Play("EventMoveToCenter");

        profile.GetComponent<Profile>().UpdateNotebook();

    }
    public void UpdateClueNameEN(string ClueName, string NewName)
    {
        int index = -1;

        index = profile.GetComponent<Profile>().Clues.FindIndex(x => x.name == ClueName);

        if (index == -1)
            return;

        FoundClue f = profile.GetComponent<Profile>().Clues[index];
        f.nameEN = NewName;
        profile.GetComponent<Profile>().Clues[index] = f;
        
        CanvasArea.transform.Find("EventWindow").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("UpdateClue").gameObject.SetActive(true);
        
        
        

        profile.GetComponent<Profile>().UpdateNotebook();

    }

    public void UpdateClueDescription(string ClueName, string NewDescription)
    {
        int index = -1;

        index = profile.GetComponent<Profile>().Clues.FindIndex(x => x.name == ClueName);

        if (index == -1)
            return;

        FoundClue f = profile.GetComponent<Profile>().Clues[index];
        f.description = NewDescription;
        profile.GetComponent<Profile>().Clues[index] = f;
        
        CanvasArea.transform.Find("EventWindow").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("UpdateClue").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("UpdateClue").Find("Text").gameObject.GetComponent<Text>()
            .text = "Улика \"" + ClueName + "\" теперь имеет описание \"" +NewDescription +"\"";
        CanvasArea.transform.Find("EventWindow").Find("UpdateClue").gameObject.GetComponent<Animator>()
            .Play("EventMoveToCenter");

        profile.GetComponent<Profile>().UpdateNotebook();

    }

    public void UpdateClueSprite(string ClueName, Sprite NewSprite)
    {
        int index = -1;

        index = profile.GetComponent<Profile>().Clues.FindIndex(x => x.name == ClueName);

        if (index == -1)
            return;

        FoundClue f = profile.GetComponent<Profile>().Clues[index];
        f.sprite = NewSprite;
        profile.GetComponent<Profile>().Clues[index] = f;
    }

    public bool isClueHaveBeenFound(string ClueName)
    {
        foreach (var VARIABLE in profile.GetComponent<Profile>().Clues)
        {
            if (VARIABLE.name == ClueName)
            {
                return true;
            }
        }

        return false;
    }
    public bool isObjectHaveBeenSeen(string ObjectName)
    {
        foreach (var VARIABLE in profile.GetComponent<Profile>().seenObjects)
        {
            if (VARIABLE == ObjectName)
            {
                return true;
            }
        }

        return false;
    }

    public bool isFlagExist(string Name)
    {
        foreach (FlagVariable VARIABLE in profile.GetComponent<Profile>().FlagVariables)
        {
            if (VARIABLE.name == Name)
            {
                return true;
            }
        }

        return false;
    }
    public bool IsFlagRaised(string Name)
    {
        foreach (FlagVariable VARIABLE in profile.GetComponent<Profile>().FlagVariables)
        {
            if (VARIABLE.name == Name)
            {
                return VARIABLE.isRaised;
            }
        }

        return false;
    }

    public void CreateFlag(string Name)
    {
        if (isFlagExist(Name))
            return;
        FlagVariable f;
        f.name = Name;
        f.isRaised = false;
        profile.GetComponent<Profile>().FlagVariables.Add(f);
    }

    public void RaiseFlag(string Name)
    {
        int index = -1;


        index = profile.GetComponent<Profile>().FlagVariables.FindIndex(x => x.name == Name);

        if (index == -1)
            return;

        FlagVariable f;
        f.name = Name;
        f.isRaised = true;
        profile.GetComponent<Profile>().FlagVariables[index] = f;
    }

    public void CheckShouldCharacterDisapiar(bool nothing)
    {
        CanvasArea.transform.Find("Dialog").GetComponent<DialogueController>().controller.GetComponent<IsActive>().ActiveSetter();
    }

    public void LowerFlag(string Name)
    {
        int index = -1;


        index = profile.GetComponent<Profile>().FlagVariables.FindIndex(x => x.name == Name);

        if (index == -1)
            return;

        FlagVariable f;
        f.name = Name;
        f.isRaised = false;
        profile.GetComponent<Profile>().FlagVariables[index] = f;
    }

    public void ToggleFlag(string Name)
    {
        int index = -1;


        index = profile.GetComponent<Profile>().FlagVariables.FindIndex(x => x.name == Name);

        if (index == -1)
            return;

        FlagVariable f;
        f.name = Name;
        f.isRaised = !profile.GetComponent<Profile>().FlagVariables[index].isRaised;
        profile.GetComponent<Profile>().FlagVariables[index] = f;
    }
    
    public bool isCharRepExist(string Name)
    {
        foreach (CharacterReputation VARIABLE in profile.GetComponent<Profile>().charactersReputations)
        {
            if (VARIABLE.Character == Name)
            {
                return true;
            }
        }

        return false;
    }
    public bool isCharRepGreaterThen(string Name, int Rep)
    {
        int index = -1;
        index = profile.GetComponent<Profile>().charactersReputations.FindIndex(x => x.Character == Name);
        if (index == -1)
            return false;

        return profile.GetComponent<Profile>().charactersReputations[index].Reputation > Rep;
    }
    public bool isCharRepLessThen(string Name, int Rep)
    {
        int index = -1;
        index = profile.GetComponent<Profile>().charactersReputations.FindIndex(x => x.Character == Name);
        if (index == -1)
            return false;

        return profile.GetComponent<Profile>().charactersReputations[index].Reputation < Rep;
    }
    public void CreateCharRep(string Name)
    {
        if (isCharRepExist(Name))
            return;
        CharacterReputation f;
        f.Character = Name;
        f.Reputation = 0;
        profile.GetComponent<Profile>().charactersReputations.Add(f);
    }
    public void SetCharRep(string Name, int Rep)
    {
        int index = -1;


        index = profile.GetComponent<Profile>().charactersReputations.FindIndex(x => x.Character == Name);


        CharacterReputation f;
        f.Character = Name;
        f.Reputation = Rep;
        
        if (index == -1)
        {
            profile.GetComponent<Profile>().charactersReputations.Add(f);
        };
        
        profile.GetComponent<Profile>().charactersReputations[index] = f;
    }
    public void AddCharRep(string Name, int Add)
    {
        int index = -1;


        index = profile.GetComponent<Profile>().charactersReputations.FindIndex(x => x.Character == Name);

        if (index == -1)
            return;

        CharacterReputation f;
        f.Character = Name;
        f.Reputation = profile.GetComponent<Profile>().charactersReputations[index].Reputation + Add;
        profile.GetComponent<Profile>().charactersReputations[index] = f;
    }
    public void RemoveCharRep(string Name, int Remove)
    {
        int index = -1;


        index = profile.GetComponent<Profile>().charactersReputations.FindIndex(x => x.Character == Name);

        if (index == -1)
            return;

        CharacterReputation f;
        f.Character = Name;
        f.Reputation = profile.GetComponent<Profile>().charactersReputations[index].Reputation - Remove;
        profile.GetComponent<Profile>().charactersReputations[index] = f;
    }

    public bool GreaterThen(int rep)
    {
        return profile.GetComponent<Profile>().GreaterThen(rep);
    }

    public bool GreaterOrEqualThen(int rep)
    {
        return profile.GetComponent<Profile>().GreaterOrEqualThen(rep);
    }

    public bool LessThen(int rep)
    {
        return profile.GetComponent<Profile>().LessThen(rep);
    }

    public bool LessOrEqualThen(int rep)
    {
        return profile.GetComponent<Profile>().LessOrEqualThen(rep);
    }

    public bool Between(int repMin, int repMax)
    {
        return profile.GetComponent<Profile>().Between(repMin, repMax);
    }

    public bool BetweenOrEqual(int repMin, int repMax)
    {
        return profile.GetComponent<Profile>().BetweenOrEqual(repMin, repMax);
    }

    public void AddReputation(int rep)
    {
        profile.GetComponent<Profile>().AddReputation(rep);
        CanvasArea.transform.Find("EventWindow").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("AddRating").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("AddRating").Find("Text").gameObject.GetComponent<Text>().text = "Вы получили " + rep + " очков";
        CanvasArea.transform.Find("EventWindow").Find("AddRating").gameObject.GetComponent<Animator>()
            .Play("EventMoveToCenter");
    }

    public void RemoveReputation(int rep)
    {
        profile.GetComponent<Profile>().RemoveReputation(rep);
        CanvasArea.transform.Find("EventWindow").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("RemoveRating").gameObject.SetActive(true);
        CanvasArea.transform.Find("EventWindow").Find("RemoveRating").Find("Text").gameObject.GetComponent<Text>().text = "Вы потеряли " + rep + " очков";

        CanvasArea.transform.Find("EventWindow").Find("RemoveRating").gameObject.GetComponent<Animator>()
            .Play("EventMoveToCenter");
    }

    public void SetReputation(int rep)
    {
        profile.GetComponent<Profile>().SetReputation(rep);
    }
}