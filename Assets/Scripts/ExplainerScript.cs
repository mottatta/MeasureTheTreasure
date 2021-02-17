using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using UnityEngine.UI;

public class ExplainerScript : MonoBehaviour
{
    [SerializeField] bool disableOnReachingIndex = false;
    [SerializeField] int level;
    [SerializeField] GameObject[] objectsToBeEnabled;
    [SerializeField] int[] speachIndexesToEnableObjects;
    [SerializeField] GameObject speachButton;
    [SerializeField] Text dialogText;
    [SerializeField] float delayBetweenLetters = 0.01f;
    [SerializeField] TransitionScript transitionScript;
    [SerializeField] GetAShareLevelScript levelScript;
    public GameObject explainAnimation;
    int currentSpeachIndex = 0;
    int currentObject = 0;

    void Start()
    {
        currentSpeachIndex = 1;
        DisplayText(currentSpeachIndex);
    }

    public void ContinueDialog()
    {
        gameObject.SetActive(true);
        DisplayText(currentSpeachIndex);
    }

    void DisplayText(int index)
    {
        //not calling if it's the getAShare level
        //objects are activated when speachButton is clicked and call GoToNextSpeach
        //this code is for explainer of the equations
        if (!disableOnReachingIndex)
        {
            //enable objects when the currentSpeachIndex == the object activate index 
            //which is maped in speachIndeexesToEnableObjects
            if (currentObject < objectsToBeEnabled.Length)
            {
                if (speachIndexesToEnableObjects[currentObject] == currentSpeachIndex)
                {
                    objectsToBeEnabled[currentObject].SetActive(true);
                    currentObject++;
                    //return;
                }
            }
        }

        if (currentSpeachIndex == 6) explainAnimation.SetActive(true);

        string textKey = "level_" + level.ToString() + "_explain_" + index.ToString();
        if (SharedState.LanguageDefs[textKey] != null)
        {
            LOLSDK.Instance.SpeakText(textKey);
            StartCoroutine(AnimateText(SharedState.LanguageDefs[textKey]));
        }
        else
        {
            transitionScript.GetComponent<TransitionScript>().DisplayTransitionAndGotoNextScene(false, true);
        }
        
    }

    IEnumerator AnimateText(string inputText)
    {
        int i = 0;
        while (i < inputText.Length)
        {
            dialogText.text += inputText[i];
            i++;
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        speachButton.SetActive(true);
    }

    public void GoToNextSpeach()
    {
        dialogText.text = "";
        currentSpeachIndex++;
        if (disableOnReachingIndex)
        {
            //enable objects when the currentSpeachIndex == the object activate index 
            //which is maped in speachIndeexesToEnableObjects
            if (currentObject < objectsToBeEnabled.Length)
            {
                if (speachIndexesToEnableObjects[currentObject] == currentSpeachIndex)
                {
                    objectsToBeEnabled[currentObject].SetActive(true);
                    gameObject.SetActive(false);
                    currentObject++;
                    return;
                }
            }
        }
        if(currentSpeachIndex == 7)
        {
            explainAnimation.SetActive(false);
            gameObject.SetActive(false);
            levelScript.CreateJewels();
            return;
        }
        DisplayText(currentSpeachIndex);
        speachButton.SetActive(false);
    }
}
