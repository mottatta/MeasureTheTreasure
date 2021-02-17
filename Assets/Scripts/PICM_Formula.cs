using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PICM_Formula : MonoBehaviour
{
    public Text factor1;
    public Text factor2;
    public Text product;

    public void EnableFormula()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableFormula()
    {
        this.gameObject.SetActive(false);
    }

    public void SetFormula(int f1, int f2, int pro)
    {
        factor1.text = f1.ToString();
        factor2.text = f2.ToString();
        product.text = pro.ToString();
    }
}
