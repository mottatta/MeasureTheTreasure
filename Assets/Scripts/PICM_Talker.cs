using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class PICM_Talker : MonoBehaviour
{
    private int currentSpeachIndex;
    [SerializeField] private GameObject speachButton;
    [SerializeField] private Text dialogText;
    public float delayBetweenLetters = 0.01f;

    bool activateSpeachButtonOnDisplayEnd;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    public void DisplayText(string index, bool _activateSpeachButton)
    {
        string textKey = index;
        speachButton.SetActive(false);
        dialogText.text = "";
        activateSpeachButtonOnDisplayEnd = _activateSpeachButton;
        
            if (SharedState.LanguageDefs[textKey] != null)
            {
                Debug.Log(SharedState.LanguageDefs[textKey]);
                LOLSDK.Instance.SpeakText(textKey);
                StartCoroutine(AnimateText(SharedState.LanguageDefs[textKey]));
            }
            else
            {
                Debug.Log("No such languageDefs key: " + index);
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
        if(activateSpeachButtonOnDisplayEnd) speachButton.SetActive(true);
    }
}
