using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UWT_LevelManager : MonoBehaviour
{
    public CoinSlot[] coinSlots;
    public Text resultTxt;
    public Text targetTxt;
    public Text[] signsTxt;
    public int solution;
    public Color wrongColor;
    public Color correctColor;
    public WrongCoinsAmountMenu wrongCoinsAmountMenu;
    public WrongCoinsAmountMenu levelCompleteMenu;
    public GameObject rotator;
    public GameObject rays;
    public AudioClip sfxSuccess;
    public AudioClip sfxFail;
    int result;
    bool isLevelComplete = false;

    void Start()
    {
        SharedState.SubmitProgress();
        UpdateResult();
    }

    void Update()
    {
        
    }

    CoinSlot GetFirstEmptyCoinSlot()
    {
        foreach(CoinSlot coin in coinSlots)
        {
            if (coin.val == 0) return coin;
        }
        return null;
    }

    public void OnCoinCollected(int val)
    {
        CoinSlot slot = GetFirstEmptyCoinSlot();
        if(slot != null)
        {
            slot.SetVal(val);
        }
        UpdateResult();
        if(!isLevelComplete && result == solution)
        {
            OnLevelComplete();
        }
        else if(!isLevelComplete && result > solution)
        {
            rotator.SetActive(false);
            isLevelComplete = true;
            ShowWrongMenu();
        }
    }

    void ShowWrongMenu()
    {
        wrongCoinsAmountMenu.Show();
        SoundManager.GetInstance().PlaySFX(sfxFail);
    }

    void OnLevelComplete()
    {
        SoundManager.GetInstance().PlaySFX(sfxSuccess);
        isLevelComplete = true;
        rotator.SetActive(false);
        levelCompleteMenu.Show();
        rays.SetActive(true);
        ChangeEquationColor(correctColor);
        rays.GetComponent<Animator>().Play("RaysRotateConstantly");
    }

    public void UpdateResult()
    {
        result = 0;
        foreach (CoinSlot coin in coinSlots)
        {
            result += coin.val;
        }
        resultTxt.text = result.ToString();
        if(SharedState.LanguageDefs != null) targetTxt.text = SharedState.LanguageDefs["coins_left"] + ": " + (solution - result).ToString();
        if (solution - result < 0)
        {
            targetTxt.text = "";
            ChangeEquationColor(wrongColor);
        }
    }

    void ChangeEquationColor(Color color)
    {
        resultTxt.color = color;
        foreach (CoinSlot slot in coinSlots)
        {
            slot.txt.color = color;
        }
        foreach(Text txt in signsTxt)
        {
            txt.color = color;
        }
    }
}
