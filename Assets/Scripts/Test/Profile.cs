using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct FlagVariable
{
    public string name;
    public bool isRaised;
}

[System.Serializable]
public struct FoundClue
{
    public string name;
    public string nameEN;
    [TextArea] public string description;
    [TextArea] public string descriptionEN;

    public Sprite sprite;
    public Color color;
}

[System.Serializable]
public struct CharacterReputation
{
    public string Character;
    public int Reputation;
}

[System.Serializable]
public struct Slot
{
    public string Scene;
    public int Reputation;
    public DateTime dateTime;
}

public class Profile : MonoBehaviour
{    
    public  Language language;

    public int reputation = 10;

    public List<FoundClue> Clues;
    public List<string> seenObjects;

    public List<FlagVariable> FlagVariables;
    public List<CharacterReputation> charactersReputations;

    public List<Slot> slots;

    private int start_from_clue = 1;
    private int clueCountPerPage = 4;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            LoadSlots();
        }
    }
   
    public void SaveProfile(int saveNumber)
    {
        SaveSystem.SaveProfile(this, saveNumber);
        LoadSlots();
    }
    public void DeleteProfile(int saveNumber)
    {
        SaveSystem.DeleteProfile(saveNumber);
        LoadSlots();
    }

    public void LoadProfile(int saveNumber)
    {
        Data data = SaveSystem.LoadProfile(saveNumber);

        reputation = data.reputation;
        Clues.Clear();
        if (data.Clues != null)
        {
            foreach (FoundClueSave clueSave in data.Clues)
            {
                FoundClue clue;
                clue.name = clueSave.name;
                clue.nameEN = clueSave.nameEN;
                clue.description = clueSave.description;
                clue.descriptionEN = clueSave.descriptionEN;
                clue.color.r = clueSave.colorR;
                clue.color.g = clueSave.colorG;
                clue.color.b = clueSave.colorB;
                clue.color.a = clueSave.colorA;
                clue.sprite = null;
                    
                 foreach(Sprite obj in Resources.LoadAll<Sprite>("Images"))
                {
                    if (clueSave.spritePath.Equals(obj.name)) 
                    {
                        clue.sprite = obj;
                    }
                }
                Clues.Add(clue);
            }
        }

        seenObjects = data.seenObjects;
        FlagVariables = data.FlagVariables;
        charactersReputations = data.charactersReputations;

        SceneManager.LoadScene(data.scene);
    }

    public void LoadSlots()
    {
        slots.Clear();
        for (int i = 1; i <= 10; i++)
        {
            Slot slot;

            Data data = SaveSystem.LoadProfile(i);
            if (data == null)
            {
                slot.Reputation = 0;
                slot.Scene = "";
                slot.dateTime = DateTime.Now;
            }
            else
            {
                slot.Reputation = data.reputation;
                slot.Scene = data.scene;
                slot.dateTime = data.dateTime;
            }

            slots.Add(slot);
        }
    }


    public void NextPageInNotebook()
    {
        start_from_clue += clueCountPerPage;
        UpdateNotebook();
    }

    public void PreviousPageInNotebook()
    {
        start_from_clue -= clueCountPerPage;
        UpdateNotebook();
    }

    public void setCLueCountPerPage(int c)
    {
        clueCountPerPage = c;
    }

    public void UpdateNotebook()
    {
        GameObject panel = null;
        GameObject canvas = GameObject.Find("Canvas").gameObject;

        if (canvas.transform.Find("Interface") != null)
        {
            panel = canvas.transform.Find("Interface").Find("RightButtons").Find("Notebook").Find("Panel")
                .gameObject;
        }

        if (panel != null)
        {
            panel.transform.Find("Line").Find("Arrow").localPosition = Vector3.right * reputation * 10f;
            panel.transform.Find("Next").gameObject.SetActive(false);
            panel.transform.Find("Previous").gameObject.SetActive(false);

            for (int j = 1; j <= clueCountPerPage; j++)
            {
                panel.transform.Find("Clue" + j).gameObject.SetActive(false);
            }

            int i = 1;

            foreach (var VARIABLE in Clues)
            {
                if (i < start_from_clue)
                {
                    i++;

                    panel.transform.Find("Previous").gameObject.SetActive(true);
                    continue;
                }


                if (i >= clueCountPerPage + start_from_clue)
                {
                    panel.transform.Find("Next").gameObject.SetActive(true);
                    break;
                }

                panel.transform.Find("Clue" + (1 + i - start_from_clue)).gameObject.SetActive(true);
                panel.transform.Find("Clue" + (1 + i - start_from_clue)).Find("Image").gameObject.GetComponent<Image>()
                        .sprite =
                    VARIABLE.sprite;
                panel.transform.Find("Clue" + (1 + i - start_from_clue)).Find("Image").gameObject.GetComponent<Image>()
                    .color = VARIABLE.color;
                //panel.transform.Find("Clue" + (1 + i - start_from_clue)).Find("Text").gameObject.GetComponent<Text>().text = VARIABLE.name;
                panel.transform.Find("Clue" + (1 + i - start_from_clue)).gameObject.GetComponent<Clue>().name = VARIABLE.name;

                i++;
            }
        }
    }


    public bool GreaterThen(int rep)
    {
        return reputation > rep;
    }

    public bool GreaterOrEqualThen(int rep)
    {
        return reputation >= rep;
    }

    public bool LessThen(int rep)
    {
        return reputation < rep;
    }

    public bool LessOrEqualThen(int rep)
    {
        return reputation <= rep;
    }

    public bool Between(int repMin, int repMax)
    {
        return reputation > repMin && reputation < repMax;
    }

    public bool BetweenOrEqual(int repMin, int repMax)
    {
        return reputation >= repMin && reputation <= repMax;
    }

    public void AddReputation(int rep)
    {
        reputation += rep;
        if (reputation >= 100)
        {
            reputation = 100;
        }

        UpdateNotebook();
    }

    public void RemoveReputation(int rep)
    {
        reputation -= rep;

        if (reputation <= -100)
        {
            reputation = -100;
        }

        UpdateNotebook();
    }

    public void SetReputation(int rep)
    {
        reputation = rep;
        UpdateNotebook();
    }
}