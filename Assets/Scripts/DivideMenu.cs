using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivideMenu : MonoBehaviour
{
    public GameObject[] coins;
    public Text txt;
    int val1, val2, res;
    string unsolvedEquation;
    public int collectedCoins;
    public GameObject rays;
    public Animator raysAnimator;
    public GameObject terms;
    void Start()
    {
        ShowCoins(0, 0);
        HideRays();
        HideTerms();
        HideEquation();
    }

    public void HideEquation()
    {
        txt.text = "";
    }

    public void ShowRays()
    {
        rays.SetActive(true);
        raysAnimator.Play("RaysRotateConstantly");
    }

    public void HideRays()
    {
        rays.SetActive(false);
    }

    public void ShowTerms()
    {
        terms.SetActive(true);
    }

    public void HideTerms()
    {
        terms.SetActive(false);
    }

    public void SetEquation(int val1, int val2, bool solved)
    {
        unsolvedEquation = val1.ToString() + " / " + val2.ToString() + " = ";
        txt.text = unsolvedEquation;
        res = val1 / val2;
        if (solved)
        {
            txt.text += (val1 / val2).ToString();
        }
        else
        {
            txt.text += "?";
        }
    }

    public void ShowResult()
    {
        txt.text = unsolvedEquation + res.ToString();
    }

    public void ShowCoins(int count, int val)
    {
        for(int i=0;i<coins.Length;i++)
        {
            coins[i].GetComponentInChildren<Text>().text = val.ToString();
            if (i < count)
            {
                coins[i].SetActive(true);
            }
            else
            {
                coins[i].SetActive(false);
            }
        }
    }

    public void OnCoinCollected()
    {
        collectedCoins++;
        for (int i = 0; i < coins.Length; i++)
        {
            if (i < collectedCoins)
            {
                coins[i].SetActive(true);
            }
            else
            {
                coins[i].SetActive(false);
            }
        }
    }
}
