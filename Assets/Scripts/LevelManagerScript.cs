using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerScript : MonoBehaviour
{
    private int _levelCoins;//coins available to be taken
    [SerializeField] int collctedCoins = 0;//how much the player has already collected
    [SerializeField] TransitionScript transitionScript;
    Text coinsDisplay;

    public GameObject picm;
    public PutInChestMenu picm_script;
    int lastCollectedVal;
    int chestsCount;

    void Awake()
    {
        _levelCoins = 0;
        GameObject cD = GameObject.FindGameObjectWithTag("coinDisplay");
        coinsDisplay = cD.GetComponentInChildren<Text>();
        coinsDisplay.text = collctedCoins.ToString();
    }

    public void AllCoinsCollected()
    {
        transitionScript.DisplayTransitionAndGotoNextScene();
    }

    public bool AreAllCoinsCollected()
    {
        return _levelCoins <= 0;
    }

    public void OnCoinCollected(int val, int _chestsCount)
    {
        lastCollectedVal = val;
        chestsCount = _chestsCount;
        Invoke("ActivatePICMMenu", 0.8f);
        SharedState.SubmitProgress();

        collctedCoins += val;
        coinsDisplay.text = collctedCoins.ToString();
        _levelCoins--;
        //if (_levelCoins <= 0) AllCoinsCollected();
    }

    private void ActivatePICMMenu()
    {
        picm_script.ActivateMenu(lastCollectedVal, chestsCount);
    }

    public int levelCoins
    {
        get {
            return _levelCoins;
        }
        set {
            _levelCoins = value;
        }
    }
}
