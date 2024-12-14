using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
public class TextManager : MonoBehaviour
{
    [SerializeField] private List<CharacterData> charactersLoaded = new List<CharacterData>();
    [SerializeField] GameObject gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Start()
    {
        // --- name the file "dialogueData.txt" ----
        LoadCharacters();
        gameManager.SendMessage("SetCharacterData", charactersLoaded);
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
        List<string> entry = new List<string>();
        List<string> r1 = new List<string>();
        List<string> r2 = new List<string>();
        string charName = null;
        string q1 = null;
        string q1f = null;
        string q2 = null;
        string q2f = null;
        string filesInfo = null;
        bool guilty = false;

        while((loadedLine = reader.ReadLine()) != null)
        {
            if (loadedLine.Length > 0 && loadedLine[0] == '#')
            {
                inputField++;
                if (inputField > 8)
                {
                    loadedCharacter.introDialogue = intro;
                    loadedCharacter.entryDialogue = entry;
                    loadedCharacter.responseOneDialogue = r1;
                    loadedCharacter.responseTwoDialogue = r2;
                    loadedCharacter.characterName = charName;
                    loadedCharacter.questionOne = q1;
                    loadedCharacter.fullQuestionOne = q1f;
                    loadedCharacter.questionTwo = q2;
                    loadedCharacter.fullQuestionTwo = q2f;
                    loadedCharacter.characterFilesInfo = filesInfo;
                    loadedCharacter.isGuilty = guilty;
                    // load mugshot sprite
                    // load regular sprite

                    charactersLoaded.Add(loadedCharacter);

                    loadedCharacter = new CharacterData();
                    intro = new List<string>();
                    entry = new List<string>();
                    r1 = new List<string>();
                    r2 = new List<string>();
                    charName = null;
                    q1 = null;
                    q1f = null;
                    q2 = null;
                    q2f = null;
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
                        entry.Add(loadedLine);
                        break;
                    case 2:
                        r1.Add(loadedLine);
                        break;
                    case 3:
                        r2.Add(loadedLine);
                        break;
                    case 4:
                        charName = loadedLine;
                        break;
                    case 5:
                        q1 = loadedLine;
                        break;
                    case 6:
                        q1f = loadedLine;
                        break;
                    case 7:
                        q2 = loadedLine;
                        break;
                    case 8:
                        q2f = loadedLine;
                        break;
                    case 9:
                        filesInfo = loadedLine;
                        break;
                    case 10:
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