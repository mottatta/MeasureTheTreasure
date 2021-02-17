using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkButtonScript : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToBeDisabled;
    [SerializeField] GameObject[] objectsToBeEnabled;

    private void OnMouseDown()
    {
        Invoke("DisableObjects", 0.2f);
        EnableObjects();
        gameObject.SetActive(false);
    }

    void EnableObjects()
    {
        if (objectsToBeEnabled.Length > 0)
        {
            foreach (GameObject obj in objectsToBeEnabled)
            {
                obj.SetActive(true);
            }
        }
    }

    void DisableObjects()
    {
        if (objectsToBeDisabled.Length > 0)
        {
            foreach (GameObject obj in objectsToBeDisabled)
            {
                obj.SetActive(false);
            }
        }
    }
}
