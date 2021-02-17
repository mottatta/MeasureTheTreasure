using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButtonScript : MonoBehaviour
{
    public ShipScript schipScript;
    public BuildMenuScript buildMenuScript;
    public string action;
    void OnMouseDown()
    {
        schipScript.OnBuildButtonPressed(action);
        buildMenuScript.OnActivity();
    }
}
