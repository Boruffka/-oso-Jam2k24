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
    [SerializeField] private TextMeshProUGUI buttonOneText;
    [SerializeField] private TextMeshProUGUI buttonTwoText;

    // dialogue data and fields
    private Coroutine typingText;
    [SerializeField] private bool currentlyTyping;

    [SerializeField] private List<CharacterData> characterData;

    [SerializeField] private float textRevealSpeed;

    [SerializeField] private int chosenCharacter = 0;
    [SerializeField] private int dialogueStageNumber= 0;
    [SerializeField] private int dialogueLineNumber = 0;
    [SerializeField] private bool typedOnce = false;

    [SerializeField] private GameObject buttonOne;
    [SerializeField] private GameObject buttonTwo;

    [SerializeField] private bool buttonOnePressed;
    [SerializeField] private bool buttonTwoPressed;

    void Awake()
    {
        // GameObjects or TMPro assignments
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        characterFilesText = GameObject.Find("CharacterFilesText").GetComponent<TextMeshProUGUI>();
        buttonOneText = GameObject.Find("ButtonOneText").GetComponent<TextMeshProUGUI>();
        buttonTwoText = GameObject.Find("ButtonTwoText").GetComponent<TextMeshProUGUI>();

        buttonOne = GameObject.Find("QuestionButtonOne");
        buttonTwo = GameObject.Find("QuestionButtonTwo");
    }

    void Start()
    {
        chosenCharacter = UnityEngine.Random.Range(0, characterData.Count);
        Debug.Log(chosenCharacter);
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
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].introDialogue[dialogueLineNumber++], textRevealSpeed));
                    }
                }
                if ((dialogueLineNumber > characterData[chosenCharacter].introDialogue.Count - 1) && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 1;
                }
                break;
            case 1:
                // slide in
                dialogueStageNumber = 2;
                break;
            case 2:
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
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].entryDialogue[dialogueLineNumber++], textRevealSpeed));
                    }
                }
                if ((dialogueLineNumber > characterData[chosenCharacter].entryDialogue.Count - 1) && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 3;
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    dialogueText.gameObject.SetActive(false);
                    buttonOne.SetActive(true);
                    buttonTwo.SetActive(true);
                    SetTextField(buttonOneText, characterData[chosenCharacter].questionOne);
                    SetTextField(buttonTwoText, characterData[chosenCharacter].questionTwo);
                }
                if (buttonOnePressed)
                {
                    dialogueStageNumber = 4;
                }
                if (buttonTwoPressed)
                {
                    dialogueStageNumber = 5;
                }
                break;
            case 4:
                typedOnce = false;
                dialogueText.gameObject.SetActive(true);
                buttonOne.SetActive(false);
                buttonTwo.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space) || !typedOnce)
                {
                    typedOnce = true;
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
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].fullQuestionOne, textRevealSpeed));
                    }
                }
                if (typedOnce && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 6;
                }
                break;
            case 5:
                typedOnce = false;
                dialogueText.gameObject.SetActive(true);
                SetTextField(dialogueText, "");
                buttonOne.SetActive(false);
                buttonTwo.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space) || !typedOnce)
                {
                    typedOnce = true;
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
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].fullQuestionTwo, textRevealSpeed));
                    }
                }
                if (typedOnce && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 7;
                }
                break;
            case 6:
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
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].responseOneDialogue[dialogueLineNumber++], textRevealSpeed));
                    }
                }
                if ((dialogueLineNumber > characterData[chosenCharacter].responseOneDialogue.Count - 1) && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 8;
                }
                break;
            case 7:
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
                        typingText = StartCoroutine(UpdateTextField(dialogueText, characterData[chosenCharacter].responseTwoDialogue[dialogueLineNumber++], textRevealSpeed));
                    }
                }
                if ((dialogueLineNumber > characterData[chosenCharacter].responseTwoDialogue.Count - 1) && currentlyTyping == false)
                {
                    dialogueLineNumber = 0;
                    dialogueStageNumber = 8;
                }
                break;
            case 8:
                Debug.Log("Reached verdict stage!");
                dialogueStageNumber = 0;
                chosenCharacter++;
                break;
            default:

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