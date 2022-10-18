using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// This class reads and writes saveData in save files
public class SaveFileHandler
{
    public void SaveDataToFile(SaveData saveData, string saveGameFilePath)
    {
        if (saveData != null)
        {
            string filePath = Application.persistentDataPath + saveGameFilePath;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create(filePath);
            binaryFormatter.Serialize(file, saveData);
            file.Close();
            MessageLog.SendMessage(this + ": Save file created @ " + filePath, MessagePrecedence.ROUTINE, false);
            Debug.Log(this + ": Save file created @ " + filePath);
        }
        else
        {
            Debug.LogError(this + ".SaveDataToFile(): saveData is null.");
        }
    }

    public SaveData LoadDataFromFile(string saveGameFilePath)
    {
        string filePath = Application.persistentDataPath + saveGameFilePath;

        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();

            Debug.Log(this + ": saveData loaded from file " + filePath);
            return saveData;
        }
        else
        {
            Debug.LogError(this + ".LoadDataFromFile() cannot load save game from " + saveGameFilePath);
            return null;
        }
    }
}

