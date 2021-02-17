using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerDivide : MonoBehaviour
{
    public TransitionScript transition;
    public GameObject[] coins;
    public DivideMenu menu;
    public PICM_Talker talker;
    public MakeEquationMenu equationMenu;

    int currentSpeach = 1;
    int currentCoin = 0;
    public int[] vals;
    public int[] targetVals;
    public GameObject coinPrefab;
    int result;
    int coinsCollected;
    public AudioClip completeClip;
    public WrongCoinsAmountMenu wrongMenu;
    public GameObject levelTalker;

    void Start()
    {
        LoadTalkerSpeach(true);
        currentSpeach++;
    }

    public void OnSpeachButtonPress()
    {
        bool _activateSpeachButton = true;
        switch (currentSpeach)
        {
            case 4:
                ActivateEquationMenu();
                return;
            case 8:
            case 12:
                ActivateEquationMenu();
                return;
            case 6:
            case 10:
                menu.ShowTerms();
                break;
            case 16:
                transition.DisplayTransitionAndGotoNextScene();
                break;
        }
        LoadTalkerSpeach(_activateSpeachButton);
        currentSpeach++;
    }

    void LoadTalkerSpeach(bool _activateSpeachButton)
    {
        string key = "divide_level_" + currentSpeach.ToString();
        talker.DisplayText(key, _activateSpeachButton);
    }

    void ActivateEquationMenu()
    {
        equationMenu.EnableMenu();
    }

    public void ActivateCurrentCoin()
    {
        menu.HideRays();
        menu.HideTerms();
        menu.HideEquation();
        LoadTalkerSpeach(false);
        currentSpeach++;
        SharedState.SubmitProgress();
        coinsCollected = 0;

        GameObject coinObject = Instantiate(coinPrefab);
        //coins[i].SetActive(true);
        DivideCoin coin = coinObject.GetComponent<DivideCoin>();
        coin.val = vals[currentCoin];
        coin.targetVal = targetVals[currentCoin];
        coin.transform.localScale = coins[0].transform.localScale;
        menu.SetEquation(coin.val, coin.targetVal, false);
        menu.ShowCoins(0, coin.targetVal);
        menu.collectedCoins = 0;
        result = coin.val / coin.targetVal;
    }

    public void OnCoinCollected()
    {
        menu.OnCoinCollected();
        coinsCollected++;
        if (coinsCollected == result)
        {
            if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(completeClip);
            DestroyAllBombs();
            menu.ShowResult();
            menu.ShowRays();
            LoadTalkerSpeach(true);
            currentSpeach++;
            currentCoin++;
        }
    }

    public void OnBombClicked()
    {
        DestroyAllCoins();
        DestroyAllBombs();
        wrongMenu.Show();
        levelTalker.SetActive(false);
    }

    public void AfterBombClickedTalker()
    {
        levelTalker.SetActive(true);
        wrongMenu.gameObject.SetActive(false);
        currentSpeach--;
        ActivateCurrentCoin();
    }

    public void DestroyAllCoins()
    {
        DivideCoin[] divideCoins = GameObject.FindObjectsOfType<DivideCoin>();
        foreach(DivideCoin c in divideCoins)
        {
            Destroy(c.gameObject);
        }
    }

    public void DestroyAllBombs()
    {
        Bomb[] bombs = GameObject.FindObjectsOfType<Bomb>();
        foreach(Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }
}
