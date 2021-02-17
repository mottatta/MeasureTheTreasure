using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInstructions : MonoBehaviour
{
    public Text desc1;
    public Text desc2;
    public bool isTreasureLevel = false;

    void Start()
    {
        if (isTreasureLevel)
        {
            if(SharedState.treasureLevelInstructionsShown == true)
            {
                gameObject.SetActive(false);
            }
            SharedState.treasureLevelInstructionsShown = true;
        }
        desc1.text = SharedState.LanguageDefs["buttons_instr_1"];
        desc2.text = SharedState.LanguageDefs["buttons_instr_2"];
    }
}
