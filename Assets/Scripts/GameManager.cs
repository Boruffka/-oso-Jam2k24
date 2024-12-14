using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // data
    [SerializeField] private List<CharacterData> characterData;

    // dialogue
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI pageOneText;
    [SerializeField] private GameObject dialogueAnchor;

    // dialogue data and fields
    private Coroutine typingText;
    [SerializeField] private bool currentlyTyping;
    [SerializeField] private float textRevealSpeed;
    [SerializeField] private int chosenCharacter = 0;
    [SerializeField] private int dialogueStageNumber = 0;
    [SerializeField] private int dialogueLineNumber = 0;
    [SerializeField] private bool typedOnce = false;

    // buttons
    [SerializeField] private TextMeshProUGUI buttonOneText;
    [SerializeField] private TextMeshProUGUI buttonTwoText;
    [SerializeField] private GameObject questionButtonOne;
    [SerializeField] private GameObject questionButtonTwo;
    [SerializeField] private bool buttonOnePressed;
    [SerializeField] private bool buttonTwoPressed;

    // files (left side)
    [SerializeField] private GameObject pageZeroAnchor;
    [SerializeField] private GameObject pageOneAnchor;
    [SerializeField] private GameObject pageTwoAnchor;

    //timers
    [SerializeField] private float movementTimer;
    [SerializeField] private bool movementTimerStarted;

    [SerializeField] private bool firstCase;

    void Awake()
    {
        // dialogue
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        pageOneText = GameObject.Find("PageOneText").GetComponent<TextMeshProUGUI>();
        dialogueAnchor = GameObject.Find("DialogueBox");

        // buttons
        buttonOneText = GameObject.Find("ButtonOneText").GetComponent<TextMeshProUGUI>();
        buttonTwoText = GameObject.Find("ButtonTwoText").GetComponent<TextMeshProUGUI>();
        questionButtonOne = GameObject.Find("QuestionButtonOne");
        questionButtonTwo = GameObject.Find("QuestionButtonTwo");

        // files
        pageZeroAnchor = GameObject.Find("PageZero");
        pageOneAnchor = GameObject.Find("PageOne");
        pageTwoAnchor = GameObject.Find("PageTwo");

        //timers
        movementTimer = 0;

        firstCase = true;
    }

    void Start()
    {
        chosenCharacter = UnityEngine.Random.Range(0, characterData.Count);
        Debug.Log(chosenCharacter);
        chosenCharacter = 0; // RANDOMIZE IT LATER (0;charCount)
    }

    void Update()
    {
        movementTimer += Time.deltaTime;

        switch(dialogueStageNumber)
        {
            case 0:
                questionButtonOne.SetActive(false);
                questionButtonTwo.SetActive(false);
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
                if(!firstCase && !movementTimerStarted)
                {
                    movementTimerStarted = true;
                    movementTimer = 0;
                    StartCoroutine(MoveTo(pageZeroAnchor.transform, new Vector3(0, 0, 0), .5f));
                    StartCoroutine(MoveTo(pageOneAnchor.transform, new Vector3(0, 0, 0), .5f));
                    StartCoroutine(MoveTo(pageTwoAnchor.transform, new Vector3(0, 0, 0), .5f));
                }
                if((movementTimerStarted && movementTimer > .75f) || !movementTimerStarted)
                {
                    firstCase = false;
                    StartCoroutine(MoveTo(pageZeroAnchor.transform, new Vector3(630, 0, 0), .5f));
                    StartCoroutine(MoveTo(pageOneAnchor.transform, new Vector3(630, 0, 0), .5f));
                    StartCoroutine(MoveTo(pageTwoAnchor.transform, new Vector3(630, 0, 0), .5f));
                    StartCoroutine(MoveTo(dialogueAnchor.transform, new Vector3(242, 0, 0), .5f));
                    dialogueStageNumber = 2;
                    // spawn postaci
                    movementTimerStarted = false;
                }
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
                        SetTextField(pageOneText, characterData[chosenCharacter].characterFilesInfo);
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
                    questionButtonOne.SetActive(true);
                    questionButtonTwo.SetActive(true);
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
                questionButtonOne.SetActive(false);
                questionButtonTwo.SetActive(false);
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
                questionButtonOne.SetActive(false);
                questionButtonTwo.SetActive(false);
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
        //CleanUp();
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

    public void movePageZeroOnScreen()
    {
        StartCoroutine(MoveTo(pageZeroAnchor.transform, new Vector3(630, 0, 0), .5f));
    }

    public void movePageZeroAwayFromScreen()
    {
        StartCoroutine(MoveTo(pageZeroAnchor.transform, new Vector3(0, 0, 0), .5f));
    }
    public void movePageOneOnScreen()
    {
        StartCoroutine(MoveTo(pageOneAnchor.transform, new Vector3(630, 0, 0), .5f));
    }

    public void movePageOneAwayFromScreen()
    {
        StartCoroutine(MoveTo(pageOneAnchor.transform, new Vector3(0, 0, 0), .5f));
    }

    public void Test()
    {
        Debug.Log("test");
    }

    IEnumerator MoveTo(Transform fromPosition, Vector3 toPosition, float duration)
    {
        float counter = 0;

        Vector3 startPos = fromPosition.localPosition;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.localPosition = Vector3.Lerp(startPos, toPosition, counter / duration);
            yield return null;
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