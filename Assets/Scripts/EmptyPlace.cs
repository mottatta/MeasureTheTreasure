using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyPlace : MonoBehaviour
{
    public SpriteRenderer coinRenderer;
    public Text txt;
    public Color noAlphaColor;
    public Color alphaColor;
    public MakeEquationMenu menu;
    int val;
    public AudioClip success;
    public AudioClip failure;
    void Start()
    {
        
    }

    public void SetVal(int n)
    {
        val = n;
        txt.text = "";
        MakeAlphaColor();
    }

    public void ShowVal()
    {
        txt.text = val.ToString();
        MakeCoinNoAlpha();
    }

    void MakeAlphaColor()
    {
        coinRenderer.color = alphaColor;
    }

    void MakeCoinNoAlpha()
    {
        coinRenderer.color = noAlphaColor; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PICM_DraggableCoin coin = collision.gameObject.GetComponent<PICM_DraggableCoin>();
        if(val == coin.val)
        {
            //SUCCESS
            if(SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(success);
            Destroy(coin.gameObject);
            ShowVal();
            menu.OnSuccess();
        }
        else
        {
            //FAILURE
            if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(failure);
            coin.ReturnToOriginalPosition();
        }
    }
}
