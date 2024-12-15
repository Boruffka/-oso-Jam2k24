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
        string cfs = null;
        string cft = null;
        string cfi = null;
        string cfp = null;
        bool guilty = false;

        while((loadedLine = reader.ReadLine()) != null)
        {
            if (loadedLine[0] != '/')
            {
                if (loadedLine.Length > 0 && loadedLine[0] == '#')
                {
                    inputField++;
                    if (inputField > 13)
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
                        loadedCharacter.characterFilesSummary = cfs;
                        loadedCharacter.characterFilesTraits = cft;
                        loadedCharacter.characterFilesIndictment = cfi;
                        loadedCharacter.characterFilesProof = cfp;
                        loadedCharacter.isGuilty = guilty;
                        // load regular sprite
                        // load mugshot sprite

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
                        cfs = null;
                        cft = null;
                        cfi = null;
                        cfp = null;
                        guilty = false;

                        inputField = 0;
                    }
                }
                else
                {
                    switch (inputField)
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
                            cfs = loadedLine;
                            break;
                        case 10:
                            cft = loadedLine;
                            break;
                        case 11:
                            cfi = loadedLine;
                            break;
                        case 12:
                            cfp = loadedLine;
                            break;
                        case 13:
                            if (loadedLine == "1")
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
        }

        reader.Close();
    }
}