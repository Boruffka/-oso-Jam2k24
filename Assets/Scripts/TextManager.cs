using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
public class TextManager : MonoBehaviour
{
    private Dictionary<int, CharacterData> charactersLoaded;

    void Start()
    {
        // name the file "dialogueData.txt"
        ReadFile();
    }

    void ReadFile()
    {
        string path = "Assets/dialogueData.txt";
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}