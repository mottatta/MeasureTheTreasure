using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMenuOKButton : MonoBehaviour
{
    [SerializeField] GetAShareLevelScript levelScript;
    void OnMouseDown()
    {
        gameObject.SetActive(false);
        levelScript.ContinueDialog();
    }
}
