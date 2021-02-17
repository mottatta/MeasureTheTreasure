using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestGroup : MonoBehaviour
{
    public Text resultTxt;
    public GameObject signsObject;

    public void SetResult(int res)
    {
        resultTxt.text = res.ToString();
    }

    public void ActivateSigns()
    {
        signsObject.SetActive(true);
    }

    public void DeactivateSigns()
    {
        signsObject.SetActive(false);
    }
}
