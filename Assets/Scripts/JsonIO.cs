using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonIO
{
    private const string dataPath = "Assets/Resources/";

    public static void SaveStatusToJson(Dictionary<string, float> dic, string fileName)
    {
        IsExists(dataPath + fileName + ".json");
        StatusData data = new StatusData();
        data.DicToVar(dic);
        string jsonString = JsonUtility.ToJson(data, true);
        File.WriteAllText(dataPath + fileName + ".json", jsonString);
    }

    public static Dictionary<string, float> LoadStatusFromJson(string fileName)
    {
        IsExists(dataPath + fileName + ".json");
        string jsonString = File.ReadAllText(dataPath + fileName + ".json");
        StatusData data = JsonUtility.FromJson<StatusData>(jsonString);
        Dictionary<string, float> status = data.VarToDic();
        return status;
    }

    private static void IsExists(string path)
    {
        if (File.Exists(path)) return;
        else Create(path);
    }

    private static void Create(string path)
    {
        File.Create(path).Close();
    }
}
