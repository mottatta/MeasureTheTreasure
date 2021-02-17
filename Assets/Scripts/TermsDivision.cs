using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermsDivision : MonoBehaviour
{
    public Text val1, val2, val3;    
    void Start()
    {
        val1.text = SharedState.LanguageDefs["dividend"];
        val2.text = SharedState.LanguageDefs["divisor"];
        val3.text = SharedState.LanguageDefs["quotient"];
    }
}
