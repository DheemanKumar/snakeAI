using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class save_load : MonoBehaviour
{
    // File path to save/load the binary data
    private string filePath;

    private void Awake()
    {
        // Set the file path
        filePath = Application.dataPath + "/data.dat";

    }

    // Save data to a binary file
    public void SaveData(object data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(filePath);

        formatter.Serialize(fileStream, data);
        fileStream.Close();

        ////Debug.Log("Data saved to: " + filePath);
    }

    // Load data from a binary file
    public object LoadData()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);

            object data = formatter.Deserialize(fileStream);
            fileStream.Close();

            ////Debug.Log("Data loaded from: " + filePath);
            return data;
        }
        else
        {
            ////Debug.Log("File does not exist: " + filePath);
            return null;
        }
    }
}
