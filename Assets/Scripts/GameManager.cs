using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // GameObjects or TMPro references
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI characterFilesText;

    // dialogue data and fields
    private Coroutine typingText;
    [SerializeField] private bool currentlyTyping;

    [SerializeField] private List<CharacterData> characterData;

    [SerializeField] private int chosenCharacter = 0;
    [SerializeField] private int dialogueStageNumber= 0;
    [SerializeField] private int dialogueLineNumber = 0;

    [SerializeField] private GameObject buttonOne;
    [SerializeField] private GameObject buttonTwo;

    [SerializeField] private bool buttonOnePressed;
    [SerializeField] private bool buttonTwoPressed;

    void Awake()
    {
        // GameObjects or TMPro assignments
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        characterFilesText = GameObject.Find("CharacterFilesText").GetComponent<TextMeshProUGUI>();

        buttonOne = GameObject.Find("QuestionButtonOne");
        buttonTwo = GameObject.Find("QuestionButtonTwo");
    }

    void Start()
    {
        SetTextField(characterFilesText, characterData[0].characterFilesInfo);

        chosenCharacter = 0; // RANDOMIZE IT LATER (0;charCount)
    }

    void Update()
    {
        switch(dialogueStageNumber)
        {
            case 0:
                buttonOne.SetActive(false);
                buttonTwo.SetActive(false);
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
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].introDialogue[dialogueLineNumber++], 0.02f));
                    }
                }
                if ((dialogueLineNumber > characterData[chosenCharacter].introDialogue.Count - 1) && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 1;
                }
                break;
            case 1:
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
                        SetTextField(characterFilesText, characterData[chosenCharacter].characterFilesInfo);
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].entryDialogue[dialogueLineNumber++], 0.02f));
                    }
                }
                if ((dialogueLineNumber > characterData[chosenCharacter].entryDialogue.Count - 1) && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 2;
                }
                break;
            case 2:
                dialogueText.gameObject.SetActive(false);
                if (buttonOnePressed)
                    dialogueStageNumber = 3;
                if (buttonTwoPressed)
                    dialogueStageNumber = 4;
                break;
            case 3:
                dialogueText.gameObject.SetActive(true);
                buttonOne.SetActive(false);
                buttonTwo.SetActive(false);
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
                        SetTextField(characterFilesText, characterData[chosenCharacter].characterFilesInfo);
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].responseOneDialogue[dialogueLineNumber++], 0.02f));
                    }
                }
                if ((dialogueLineNumber > characterData[chosenCharacter].responseOneDialogue.Count - 1) && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 5;
                }
                break;
            case 4:
                dialogueText.gameObject.SetActive(true);
                buttonOne.SetActive(false);
                buttonTwo.SetActive(false);
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
                        SetTextField(characterFilesText, characterData[chosenCharacter].characterFilesInfo);
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].responseTwoDialogue[dialogueLineNumber++], 0.02f));
                    }
                }
                if ((dialogueLineNumber > characterData[chosenCharacter].responseTwoDialogue.Count - 1) && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 5;
                }
                break;
            case 5:
                Debug.Log("fin");
                dialogueStageNumber++;
                break;
            default:
                Debug.Log("ERROR!");
                break;
        }
        CleanUp();
    }

    void CleanUp()
    {
        buttonOnePressed = false;
        buttonTwoPressed = false;
    }

    public void ButtonOnePressed()
    {
        buttonOnePressed = true;
    }

    public void ButtonTwoPressed()
    {
        buttonTwoPressed = true;
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