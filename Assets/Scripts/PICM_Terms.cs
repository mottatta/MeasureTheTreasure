using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PICM_Terms : MonoBehaviour
{
    public Text factor1, factor2, product;
    void Start()
    {
        factor1.text = SharedState.LanguageDefs["factor"];
        factor2.text = SharedState.LanguageDefs["factor"];
        product.text = SharedState.LanguageDefs["product"];
    }
}
