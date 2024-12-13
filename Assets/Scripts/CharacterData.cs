using UnityEngine;
using System.Collections.Generic;

public class CharacterData
{
    List<string> introDialogue;         // [1] lines of intro dialogue (judge)

    string characterName;               // [2] single line

    Sprite characterSprite;             // character sprite

    List<string> entryDialogue;         // [3] lines of entry dialogue

    string questionOne;                 // [4]
    string questionTwo;                 // [5] two questions, short

    List<string> responseOneDialogue;   // [6] response to question one
    List<string> responseTwoDialogue;   // [7] response to question two

    string filesInfo;                   // box of info +/- 14 lines

    bool isGuilty;                      // correct verdict (0/1)
}
