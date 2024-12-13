using UnityEngine;
using System.Collections.Generic;

public class CharacterData
{
    List<string> introDialogue;         // lines of intro dialogue (judge)

    string characterName;               // single line

    Sprite characterSprite;             // character sprite

    List<string> entryDialogue;         // lines of entry dialogue

    string questionOne;                 // 
    string questionTwo;                 // two questions, short

    List<string> responseOneDialogue;   // response to question one
    List<string> responseTwoDialogue;   // response to question two

    string filesInfo;                   // box of info +/- 14 lines

    bool isGuilty;                      // correct verdict (0/1)
}
