using UnityEngine;
using System.Collections;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;

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
            StopAllCoroutines();
            StartCoroutine(UpdateTextField(dialogueText, "That's dialogue text for you. That's dialogue text for you. That's dialogue text for you.", 0.1f));
        }
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
    }
}
