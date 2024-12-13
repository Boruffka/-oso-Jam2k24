using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
public class TextManager : MonoBehaviour
{
    [SerializeField] private List<CharacterData> charactersLoaded;

    void Start()
    {
        // name the file "dialogueData.txt"
        LoadCharacters();
    }

    // s - single, m - multiple lines
    // [0] intro (m)
    // [1] charName (s)
    // [2] entry (m)
    // [3] question1 (s)
    // [4] question2 (s)
    // [5] response1 (m)
    // [6] response2 (m)
    // [7] filesInfo (s)
    // [8] guilty (s) 0 or 1

    void LoadCharacters()
    {
        string path = "Assets/dialogueData.txt";
        StreamReader reader = new StreamReader(path);
        string loadedData = reader.ReadToEnd();
        string loadedLine = "";
        int inputField = 0;

        List<string> intro = new List<string>();
        string charName = null;
        List<string> entry = new List<string>();
        string q1 = null;
        string q2 = null;
        List<string> response1 = new List<string>();
        List<string> response2 = new List<string>();
        string filesInfo = null;
        bool guilty = false;

        Debug.Log(loadedData);

        while((loadedLine = reader.ReadLine()) != null)
        {
            if (loadedLine[loadedLine.Length-1]=='#')
            {
                inputField++;
                if (inputField == 9) inputField = 8;
            }
            Debug.Log(inputField + " " + loadedLine);
        }

        reader.Close();
    }
}