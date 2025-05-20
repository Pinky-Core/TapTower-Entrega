using System;
using System.IO;
using System.Text;
using UnityEngine;

public abstract class BaseScriptableVariable : ScriptableObject
{
    [SerializeField] bool redeableFile = false;
    [SerializeField] protected string fileExtention = ".save";

    public string folderPath => $"{Application.persistentDataPath}/";
    public string completePath => $"{folderPath}{name.GetHashCode()}{fileExtention}";
    public string finderPath => File.Exists(completePath) ? completePath : folderPath;

    /// <summary>
    /// Salva datos en un archivo
    /// </summary>
    public abstract void SaveData();

    /// <summary>
    /// Cargar datos de un archivo
    /// </summary>
    public abstract void LoadData();

    /// <summary>
    /// Funcion generica para guardar datos.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    protected void SaveData<T>(T value)
    {
        string jsonData = redeableFile ? JsonUtility.ToJson(value) : Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonUtility.ToJson(value)));
        File.WriteAllText(completePath, jsonData);

        Debug.LogWarning($"<color=green>{completePath}</color> file saved.");
    }

    /// <summary>
    /// Funcion generica para cargar datos.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T LoadData<T>()
    {
        if (File.Exists(completePath))
        {
            Debug.LogWarning($"<color=green>{completePath}</color> file loaded.");
            return JsonUtility.FromJson<T>(redeableFile ? File.ReadAllText(completePath) : Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(completePath))));
        }
        else
        {
            Debug.LogWarning($"File not found: <color=red>{completePath}</color>");
            return default(T);
        }
    }

    /// <summary>
    /// Borra el archivo de salvado si existiera.
    /// </summary>
    public virtual void EraseSaveFile()
    {
        if (File.Exists(completePath))
        {
            File.Delete(completePath);
            Debug.Log($"<color=yellow>{completePath}</color> deleted");
        }
        else
        {
            Debug.LogWarning($"<color=red>{completePath}</color> not found");
        }
    }
}
