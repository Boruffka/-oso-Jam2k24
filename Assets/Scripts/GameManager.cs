using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class GameManager : MonoBehaviour
{
    // GameObjects or TMPro references
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI characterFilesText;

    private Coroutine typingText;
    [SerializeField] private bool currentlyTyping;

    [SerializeField] private List<CharacterData> characterData;

    [SerializeField] private int dialogueStageNumber= 0;
    [SerializeField] private int dialogueFieldNumber = 0;
    [SerializeField] private int dialogueLineNumber = 0;

    // GameObjects or TMPro assignments
    void Awake()
    {
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        characterNameText = GameObject.Find("CharacterName  Text").GetComponent<TextMeshProUGUI>();
        characterFilesText = GameObject.Find("CharacterFilesText").GetComponent<TextMeshProUGUI>();

    }

    void Start()
    {
        SetTextField(characterFilesText, characterData[0].characterFilesInfo);
        SetTextField(characterNameText, characterData[0].characterName);
    }

    void Update()
    {
        switch(dialogueStageNumber)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (typingText != null)
                    {
                        StopCoroutine(typingText);
                    }
                    if (currentlyTyping)
                    {
                        dialogueText.maxVisibleCharacters = Int32.MaxValue;
                        currentlyTyping = false;
                    }
                    else
                    {
                        currentlyTyping = true;
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[dialogueFieldNumber].introDialogue[dialogueLineNumber++], 0f));
                        if (dialogueLineNumber > characterData[dialogueFieldNumber].introDialogue.Count)
                        {
                            dialogueLineNumber = 0;
                            dialogueFieldNumber = 0;
                        }
                    }
                }
                break;
        }
        
    }


    void SetCharacterData(List<CharacterData> setCharacterData)
    {
        characterData = setCharacterData;
        Debug.Log("Loaded!" + characterData.Count);
    }
    void SetTextField(TextMeshProUGUI textField, string updatedText)
    {
        textField.text = updatedText;
        dialogueText.maxVisibleCharacters = Int32.MaxValue;
    }

    IEnumerator UpdateTextField(TextMeshProUGUI textField, string updatedText, float letterDelay)
    {
        textField.text = updatedText;
        textField.maxVisibleCharacters = 0;
        for (int i = 0; i < updatedText.Length; i++)
        {
            textField.maxVisibleCharacters++;
            yield return new WaitForSeconds(letterDelay);
        }
        currentlyTyping = false;
        yield return null;
    }
}