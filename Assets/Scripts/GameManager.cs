using UnityEngine;
using System;
using System.Collections;
using TMPro;
public class GameManager : MonoBehaviour
{
    // GameObjects or TMPro references
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Coroutine typingText;
    [SerializeField] private bool currentlyTyping;


    // GameObjects or TMPro assignments
    void Awake()
    {
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
                typingText = StartCoroutine(UpdateTextField(dialogueText, "That's dialogue text for you. That's dialogue text for you. That's dialogue text for you.", 0.1f));
            }
        }
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