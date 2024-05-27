using System;
using System.IO;
using UnityEngine;

public static class JSONHelper
{
    public static readonly string TileDataDirectoryPath = "Assets/JsonData";
    public static readonly string TileDataFilePath = Path.Combine(TileDataDirectoryPath, "tileData.json");
    public static readonly string InventoryFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
    public static readonly string BoardIndexFilePath = Path.Combine(Application.persistentDataPath, "boardIndex.json");
    public static readonly string DiceAmountFilePath = Path.Combine(Application.persistentDataPath, "diceAmount.json");

    public static void SaveData<T>(string filePath, T data)
    {
        try
        {
            string jsonData = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Data saved to " + filePath + " , data : " + data);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save data to " + filePath + "\n" + e.Message);
        }
    }

    public static T LoadData<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                T data = JsonUtility.FromJson<T>(jsonData);
                Debug.Log("Data loaded from " + filePath + " , data : " + data);
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load data from " + filePath + "\n" + e.Message);
                return default;
            }
        }
        else
        {
            Debug.LogWarning("Save file not found at " + filePath);
            return default;
        }
    }

    public static void ResetData<T>(string filePath, T defaultData)
    {
        SaveData(filePath, defaultData);
        Debug.Log("Data reset for " + filePath);
    }
}
