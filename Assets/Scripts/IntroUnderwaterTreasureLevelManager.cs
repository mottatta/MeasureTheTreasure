using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUnderwaterTreasureLevelManager : MonoBehaviour
{
    string prefix = "intro_uwt_level_";
    int currentDialog = 1;

    public PICM_Talker talker;
    public TransitionScript transition;

    void Start()
    {
        SoundManager.GetInstance().PlayMusic(SoundManager.GetInstance().levelMusic);
        TalkDialog();
    }

    void Update()
    {
        
    }

    public void OnSpeachButtonPressed()
    {
        currentDialog++;
        TalkDialog();
    }

    void TalkDialog()
    {
        string dialogStr = prefix + currentDialog.ToString();
        if (SharedState.LanguageDefs != null)
        {
            if (SharedState.LanguageDefs[dialogStr])
            {
                talker.DisplayText(dialogStr, true);
            }
            else transition.DisplayTransitionAndGotoNextScene(false, false);
        }
    }
}
