using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WrongCoinsAmountMenu : MonoBehaviour
{
    public PICM_Talker talker;
    public bool isLevelComplete = false;
    public TransitionScript transition;
    public bool isDevideLevelWrongTalker = false;
    public bool gotoMapAfterThatLevel = true;

    public void Show()
    {
        gameObject.SetActive(true);
        string dialogString = (isLevelComplete) ? "uwt_level_success" : "uwt_level_fail";
        if (!isDevideLevelWrongTalker) talker.DisplayText(dialogString, true);
        else talker.DisplayText("devide_level_bomb", true);
    }

    public void OnPlayButtonClicked()
    {
        if (isDevideLevelWrongTalker)
        {
            GameObject.FindObjectOfType<LevelManagerDivide>().AfterBombClickedTalker();
        }
        else
        {
            if (!isLevelComplete)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                transition.DisplayTransitionAndGotoNextScene(false, gotoMapAfterThatLevel);
            }
        }
    }
}
