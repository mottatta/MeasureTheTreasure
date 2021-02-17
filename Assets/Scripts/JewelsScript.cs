using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelsScript : MonoBehaviour
{
    public float delayToHint = 1.20f;
    CombinerScript combinerScript;
    GameObject levelManager;
    GetAShareLevelScript levelManagerScript;

    void Start()
    {
        combinerScript = GameObject.FindGameObjectWithTag("combiner").GetComponent<CombinerScript>();
        levelManager = GameObject.FindGameObjectWithTag("levelManager");
        levelManagerScript = levelManager.GetComponent<GetAShareLevelScript>();
    }

   public void EnableChildsByCount(int count)
    {
        for(int i = 0;i < transform.childCount; i++)
        {
            if (i < count) transform.GetChild(i).gameObject.SetActive(true);
            else transform.GetChild(i).gameObject.SetActive(false);
        }
        HideAllHints();
        Invoke("ShowTwoHints", 0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideAllHints();
            CancelInvoke("ShowTwoHints");
            Invoke("ShowTwoHints", delayToHint);
        }
    }

    public void HideAllHints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).gameObject.GetComponent<CombineObjectScript>().HideHint();
            }
        }
    }

    void ShowTwoHints()
    {
        if (combinerScript.levelIsOver) return;
        int index1 = -1;
        int index2 = -1;
        int val = -1;
        while(index1 == -1 || index2 == -1)
        {
            index1 = -1;
            index2 = -1;
            val = 100;
            //set val
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    int newVal = transform.GetChild(i).gameObject.GetComponent<CombineObjectScript>().GetVal();
                    if(newVal != levelManagerScript.GetNextLevelVal())
                    {
                        if(newVal < val)
                        {
                            val = newVal;
                            index1 = i;
                        }
                    }
                }
            }

            for (int j = 0; j < transform.childCount; j++)
            {
                if (transform.GetChild(j).gameObject.activeInHierarchy)
                {
                    int newVal = transform.GetChild(j).gameObject.GetComponent<CombineObjectScript>().GetVal();
                    if (val == newVal && j != index1)
                    {
                         index2 = j;
                    }
                }
            }
        }
        if(index1 != -1 && index2 != -1)
        {
            transform.GetChild(index1).GetComponent<CombineObjectScript>().ShowHint();
            transform.GetChild(index2).GetComponent<CombineObjectScript>().ShowHint();
        }
    }
}
