using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject creditsAnchor;
    [SerializeField] private bool creditsShown;

   
    void Awake()
    {
        creditsAnchor = GameObject.Find("CreditsAnchor");
        creditsShown = false;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Court", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        Debug.Log("Cr!");
        if(!creditsShown)
        {
            StartCoroutine(MoveTo(creditsAnchor.transform, new Vector3(0, 0, 0), 2f));
            creditsShown = true;
        }
        else
        {
            StartCoroutine(MoveTo(creditsAnchor.transform, new Vector3(1000, 0, 0), 2f));
            creditsShown = false;
        }
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
}
