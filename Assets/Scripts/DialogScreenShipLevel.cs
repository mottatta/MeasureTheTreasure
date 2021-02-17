using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LoLSDK;

public class DialogScreenShipLevel : MonoBehaviour
{
    [SerializeField] int level;
    private int currentSpeachIndex;
    [SerializeField] private GameObject speachButton;
    [SerializeField] private Text dialogText;
    [SerializeField] TransitionScript transitionScript;
    [SerializeField] bool gotoNextSceneOnDialogEnd = true;

    [SerializeField] int[] indexesToEnableTask;
    [SerializeField] ShipScript shipScript;

    private bool isDialogBeingTyped;
    private bool isRepeatingSameSpeach;//if repeating same speach don't enable new task in ship script. Repeating is used when task wasn't complete, just to say it's wrong
    public bool gotoMapAfterThatLevel = false;
    float delayBetweenLetters = 0.05f;

    Coroutine typingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.GetInstance().PlayMusic(SoundManager.GetInstance().levelMusic);
        currentSpeachIndex = 1;
        DisplayText(currentSpeachIndex);
    }

    void DisplayText(int index)
    {
        string textKey = "level_" + level.ToString() + "_" + index.ToString();
        if (SharedState.LanguageDefs[textKey] != null)
        {
            LOLSDK.Instance.SpeakText(textKey);
            isDialogBeingTyped = true;
            typingCoroutine = StartCoroutine(AnimateText(SharedState.LanguageDefs[textKey]));
        }
        else
        {
            if (gotoNextSceneOnDialogEnd)
            {
                transitionScript.DisplayTransitionAndGotoNextScene(false, gotoMapAfterThatLevel);
            }
            else
            {
                //gameObject.SetActive(false);
            }
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
        isDialogBeingTyped = false;
        if(!IsSpeachEnabelingTask()) speachButton.SetActive(true);
        else {
            if (!isRepeatingSameSpeach) shipScript.EnableNextTask();
        }
    }

    private bool IsSpeachEnabelingTask()
    {
        for (int i = 0; i < indexesToEnableTask.Length; i++)
        {
            if (currentSpeachIndex == indexesToEnableTask[i]) return true;
        }
        return false;
    }

    public void GoToNextSpeach()
    {
        if (isDialogBeingTyped)
        {
            StopCoroutine(typingCoroutine);
        }
        isRepeatingSameSpeach = false;
        dialogText.text = "";
        currentSpeachIndex++;
        DisplayText(currentSpeachIndex);
        speachButton.SetActive(false);
    }

    public void SpeakSameSpeach()
    {
        if (isDialogBeingTyped) return;
        isRepeatingSameSpeach = true;
        dialogText.text = "";
        DisplayText(currentSpeachIndex);
        speachButton.SetActive(false);
    }
}
