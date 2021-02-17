using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] float secondsToReminder = 5f;
    
    void Start()
    {
        DisablePointer();
    }



    public void EnablePointer()
    {
        pointer.SetActive(true);
    }

    public void DisablePointer()
    {

        Invoke("EnablePointer", secondsToReminder);
        pointer.SetActive(false);
    }

    public void OnActivity()
    {
        CancelInvoke("EnablePointer");
        DisablePointer();
    }
}
