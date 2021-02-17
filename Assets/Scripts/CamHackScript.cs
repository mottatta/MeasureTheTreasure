using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHackScript : MonoBehaviour
{
    public Camera diceCam;
    private void OnPreRender()
    {
        diceCam.Render();
    }
}
