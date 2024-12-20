using UnityEngine;
using System.Collections.Generic;

public class CharacterData
{
    public List<string> introDialogue;         // [0] lines of intro dialogue (judge)
    public List<string> entryDialogue;         // [1] lines of entry dialogue
    public List<string> responseOneDialogue;   // [2] response to question one
    public List<string> responseTwoDialogue;   // [3] response to question two

    public string characterName;               // [4] single line
    public string questionOne;                 // [5] brief of question one
    public string fullQuestionOne;             // [6] full question one
    public string questionTwo;                 // [7] brief of question two
    public string fullQuestionTwo;             // [8] full question two
    public string characterFilesSummary;       // [9] box of info 
    public string characterFilesTraits;        // [10] box of info 
    public string characterFilesIndictment;    // [11] box of info 
    public string characterFilesProof;         // [12] box of info 
    public bool isGuilty;                      // [13] correct verdict (0/1)

    public Sprite characterSprite;             // character sprite
    public Sprite characterFilesSprite;        // character second sprite
}
