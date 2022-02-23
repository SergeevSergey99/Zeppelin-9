using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{

    public static void DeleteProfile(int saveNumber) 
    {
        string path = Application.persistentDataPath + "/Saves";
        path += "/SaveNumber" + saveNumber + ".saveData";
        if (File.Exists(path)) 
        {
            File.Delete(path);
        }

    }
    public static void SaveProfile(Profile profile, int saveNumber)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Saves";
        Directory.CreateDirectory(path);
        path += "/SaveNumber" + saveNumber + ".saveData";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(profile);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data LoadProfile(int saveNumber)
    {
        string path = Application.persistentDataPath + "/Saves/SaveNumber" + saveNumber + ".saveData";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream strem = new FileStream(path, FileMode.Open);
            Debug.Log(path);
            
            Data data = formatter.Deserialize(strem) as Data;
            strem.Close();
            
            return data;
        }

        //Debug.Log("Save file not found in " + path);
        return null;
    }
        
}