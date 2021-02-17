using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LoLSDK;

public class DialogScreenScript : MonoBehaviour
{
    [SerializeField] int level;
    private int currentSpeachIndex;
    [SerializeField] private GameObject speachButton;
    [SerializeField] private Text dialogText;
    [SerializeField] TransitionScript transitionScript;
    [SerializeField] bool gotoNextSceneOnDialogEnd = true;
    [SerializeField] GameObject[] objectsToEnable;

    public bool isFinalScene = false;
    float delayBetweenLetters = 0.05f;

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
        if (SharedState.LanguageDefs != null)
        {
            if (SharedState.LanguageDefs[textKey] != null)
            {
                LOLSDK.Instance.SpeakText(textKey);
                StartCoroutine(AnimateText(SharedState.LanguageDefs[textKey]));
            }
            else
            {
                if (gotoNextSceneOnDialogEnd)
                {
                    transitionScript.DisplayTransitionAndGotoNextScene(false, false, isFinalScene); 
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
        DisplayText(currentSpeachIndex);
        speachButton.SetActive(false);
    }
}
