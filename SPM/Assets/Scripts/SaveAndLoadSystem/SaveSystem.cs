using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(GameObject player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath + "SaveFile.dat");
        FileStream fileStream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(player);
        formatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath + "SaveFile.dat");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(fileStream) as GameData;
            fileStream.Close();
            return data;
           
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
