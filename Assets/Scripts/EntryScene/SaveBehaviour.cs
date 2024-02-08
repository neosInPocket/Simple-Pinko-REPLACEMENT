using System.IO;
using UnityEngine;

public class SaveBehaviour : MonoBehaviour
{
    [SerializeField] private bool clearPreviousSaves;
    private static string dataFilePath => Application.persistentDataPath + "/SavesData.json";
    public static SavesData DataFile { get; set; }

    private void Awake()
    {
        if (clearPreviousSaves)
        {
            DataFile = new SavesData();
            SetValue();
        }
        else
        {
            GetValues();
        }
    }

    public static void SetValue()
    {
        if (!File.Exists(dataFilePath))
        {
            NewValues();
        }
        else
        {
            WriteValues();
        }
    }

    public static void GetValues()
    {
        if (!File.Exists(dataFilePath))
        {
            NewValues();
        }
        else
        {
            string text = File.ReadAllText(dataFilePath);
            DataFile = JsonUtility.FromJson<SavesData>(text);
        }
    }

    private static void NewValues()
    {
        DataFile = new SavesData(); ;
        File.WriteAllText(dataFilePath, JsonUtility.ToJson(DataFile));
    }

    private static void WriteValues()
    {
        File.WriteAllText(dataFilePath, JsonUtility.ToJson(DataFile));
    }
}
