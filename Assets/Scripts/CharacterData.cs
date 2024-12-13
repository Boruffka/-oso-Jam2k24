using UnityEngine;
using System.Collections.Generic;

public class CharacterData
{
    public List<string> introDialogue;         // [0] lines of intro dialogue (judge)

    public string characterName;               // [1] single line

    public Sprite characterSprite;             // character sprite

    public List<string> entryDialogue;         // [2] lines of entry dialogue

    public string questionOne;                 // [3]
    public string questionTwo;                 // [4] two questions, short

    public List<string> responseOneDialogue;   // [5] response to question one
    public List<string> responseTwoDialogue;   // [6] response to question two

    public string characterFilesInfo;          // box of info +/- 14 lines

    public bool isGuilty;                      // correct verdict (0/1)
}
