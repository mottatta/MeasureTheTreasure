using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollHint : MonoBehaviour
{
    private void Awake()
    {
        string hint = SharedState.LanguageDefs["rollHint"];
        GetComponentInChildren<Text>().text = hint;
    }
}
