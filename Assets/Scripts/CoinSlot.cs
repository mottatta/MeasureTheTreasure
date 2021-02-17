using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSlot : MonoBehaviour
{
    public Text txt;
    public int val;
    public GameObject body;
    public GameObject sparksPrefab;

    void Start()
    {
        SetVal(0);                        
    }

    public void Show()
    {
        GameObject sparks = Instantiate(sparksPrefab);
        sparks.transform.position = transform.position;
        body.SetActive(true);
    }

    public void SetVal(int v)
    {
        val = v;
        if (val == 0)
        {
            txt.text = "";
            body.SetActive(false);
        }
        else
        {
            txt.text = val.ToString();
            Show();
        }
    }
}
