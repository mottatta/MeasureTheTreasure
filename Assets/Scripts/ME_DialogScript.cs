using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LoLSDK;

public class ME_DialogScript : MonoBehaviour
{
    [SerializeField] int level;
    private int currentSpeachIndex;
    [SerializeField] private GameObject speachButton;
    [SerializeField] private Text dialogText;
    [SerializeField] TransitionScript transitionScript;
    [SerializeField] bool gotoNextSceneOnDialogEnd = true;
    [SerializeField] GameObject[] objectsToEnable;
    public ME_LevelManager levelManager;

    public float delayBetweenLetters = 0.05f;
    Coroutine c;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeachIndex = 1;
        DisplayText(currentSpeachIndex, true);
    }

    void DisplayText(int index, bool showSpeachButtonAtTheEnd, bool gotoMapLevel = false)
    {
        dialogText.text = "";
        if(c != null) StopCoroutine(c);
        string textKey = "equation_level_" + level.ToString() + "_" + index.ToString();
        if (SharedState.LanguageDefs[textKey] != null)
        {
            LOLSDK.Instance.SpeakText(textKey);
            c = StartCoroutine(AnimateText(SharedState.LanguageDefs[textKey], showSpeachButtonAtTheEnd));
        }
        else
        {
            if (gotoNextSceneOnDialogEnd)
            {
                transitionScript.DisplayTransitionAndGotoNextScene(false, gotoMapLevel);
            }
            else
            {
                gameObject.SetActive(false);
                foreach (GameObject obj in objectsToEnable)
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    IEnumerator AnimateText(string inputText, bool showSpeachButtonAtTheEnd)
    {
        int i = 0;
        while (i < inputText.Length)
        {
            dialogText.text += inputText[i];
            i++;
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        if(showSpeachButtonAtTheEnd) speachButton.SetActive(true);
        else speachButton.SetActive(false);
    }

    public void GoToNextSpeach()
    {
        dialogText.text = "";
        currentSpeachIndex++;
        if(currentSpeachIndex == 2)
        {
            levelManager.EnableGameplayObjects();
            DisplayText(currentSpeachIndex, false);
        }
        else if(currentSpeachIndex == 3)
        {
            DisplayText(currentSpeachIndex, false);
        }
        else
        {
            DisplayText(currentSpeachIndex, true, true);
        }
        speachButton.SetActive(false);
    }
}
