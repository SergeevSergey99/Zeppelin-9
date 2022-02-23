using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct FoundClueSave
{
    public string name;
    public string nameEN;
    [TextArea] public string description;
    [TextArea] public string descriptionEN;

    public string spritePath;
    public float colorR;
    public float colorG;
    public float colorB;
    public float colorA;
}

[Serializable]
public class Data
{
    public string scene;
    public int reputation;
    
    public DateTime dateTime;
    
    public List<FoundClueSave> Clues;
    public List<string> seenObjects;
    
    public List<FlagVariable> FlagVariables;
    public List<CharacterReputation> charactersReputations;

    public Data(Profile profile)
    {
        scene = SceneManager.GetActiveScene().name;
        reputation = profile.reputation;
        dateTime = DateTime.Now;

        List<FoundClueSave> MyClues = new List<FoundClueSave>(0);
        foreach (FoundClue clue in profile.Clues)
        {
            FoundClueSave clueSave;
            clueSave.name = clue.name;
            clueSave.nameEN = clue.nameEN;
            clueSave.description = clue.description;
            clueSave.descriptionEN = clue.descriptionEN;
            clueSave.colorR = clue.color.r;
            clueSave.colorG = clue.color.g;
            clueSave.colorB = clue.color.b;
            clueSave.colorA = clue.color.a;

            clueSave.spritePath = clue.sprite.name;
//            Debug.Log(clue.sprite.name);
            
            
            MyClues.Add(clueSave);
        }

        Clues = MyClues;
        seenObjects = profile.seenObjects;

        FlagVariables = profile.FlagVariables;
        charactersReputations = profile.charactersReputations;
    }

}
