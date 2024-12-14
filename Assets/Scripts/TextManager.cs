using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
public class TextManager : MonoBehaviour
{
    [SerializeField] private List<CharacterData> charactersLoaded = new List<CharacterData>();

    void Start()
    {
        // name the file "dialogueData.txt"
        LoadCharacters();
        Debug.Log(charactersLoaded.Count);
        Debug.Log(charactersLoaded[0].introDialogue[0]);
    }

    // s - single, m - multiple lines
    // [0] intro (m)
    // [1] charName (s)
    // [2] entry (m)
    // [3] q1 (s)
    // [4] q2 (s)
    // [5] r1 (m)
    // [6] r2 (m)
    // [7] filesInfo (s)
    // [8] guilty (s) 0 or 1

    void LoadCharacters()
    {
        string path = "Assets/dialogueData.txt";
        StreamReader reader = new StreamReader(path);
        string loadedLine = "";
        int inputField = 0;

        CharacterData loadedCharacter = new CharacterData();
        List<string> intro = new List<string>();
        string charName = null;
        List<string> entry = new List<string>();
        string q1 = null;
        string q2 = null;
        List<string> r1 = new List<string>();
        List<string> r2 = new List<string>();
        string filesInfo = null;
        bool guilty = false;

        while((loadedLine = reader.ReadLine()) != null)
        {
            if (loadedLine == "#")
            {
                inputField++;
                if (inputField >= 8)
                {
                    loadedCharacter.introDialogue = intro;
                    loadedCharacter.characterName = charName;
                    loadedCharacter.entryDialogue = entry;
                    loadedCharacter.questionOne = q1;
                    loadedCharacter.questionTwo = q2;
                    loadedCharacter.responseOneDialogue = r1;
                    loadedCharacter.responseTwoDialogue = r2;
                    loadedCharacter.characterFilesInfo = filesInfo;
                    loadedCharacter.isGuilty = guilty;

                    charactersLoaded.Add(loadedCharacter);

                    loadedCharacter = new CharacterData();
                    intro = new List<string>();
                    charName = null;
                    entry = new List<string>();
                    q1 = null;
                    q2 = null;
                    r1 = new List<string>();
                    r2 = new List<string>();
                    filesInfo = null;
                    guilty = false;

                    inputField = 0;
                }
            }
            else
            {
                switch(inputField)
                {
                    case 0:
                        intro.Add(loadedLine);
                        break;
                    case 1:
                        charName = loadedLine;
                        break;
                    case 2:
                        entry.Add(loadedLine);
                        break;
                    case 3:
                        q1 = loadedLine;
                        break;
                    case 4:
                        q2 = loadedLine;
                        break;
                    case 5:
                        r1.Add(loadedLine);
                        break;
                    case 6:
                        r2.Add(loadedLine);
                        break;
                    case 7:
                        filesInfo = loadedLine;
                        break;
                    case 8:
                        if(loadedLine == "1")
                        {
                            guilty = true;
                        }
                        else
                        {
                            guilty = false;
                        }
                        break;
                    default:
                        Debug.Log("Something went horribly wrong during .txt input operations.");
                        break;
                }
            }
        }

        reader.Close();
    }
}