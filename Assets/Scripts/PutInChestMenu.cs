using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInChestMenu : MonoBehaviour
{
    public LevelManagerScript levelManager;
    public bool isClickingAllowed = false;
    int coinsCount, coinsCountOriginal;
    public GameObject picmCoins;
    public Transform coinsPos;
    GameObject picm_coins;
    PICM_Chest[] chest_scripts;
    public GameObject[] chestGroups;
    int chestGroupsIndex;
    public PICM_Talker talker;
    public PICM_Formula formula;
    public GameObject terms;
    public AudioClip picm_sfx;
    public AudioClip picm_tadaaa;

    bool miniGameComplete;
    int dialogIndex;

    void Start()
    {
        transform.position = new Vector3(50, 0, 0);
        Invoke("DeactivateMenu", 0.5f);
    }

    public void OnCoinPutInChest()
    {
        coinsCount--;
        SoundManager.GetInstance().PlaySFX(picm_sfx);
        if(coinsCount <= 0)
        {
            //TODO show explain animation
            if (AllChestsAreEqual())
            {
                if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(picm_tadaaa);
                miniGameComplete = true;
                chestGroups[chestGroupsIndex].GetComponent<ChestGroup>().ActivateSigns();
                string key = "picm_" + coinsCountOriginal.ToString() + "_" + dialogIndex.ToString();
                talker.DisplayText(key, true);
            }
            else
            {
                talker.DisplayText("picm_err", true);
            }
        }
    }

    bool AllChestsAreEqual()
    {
        int val = chest_scripts[0].GetVal();
        for(int i = 1; i < chest_scripts.Length; i++)
        {
            if (val != chest_scripts[i].GetVal()) return false;
        }
        return true;
    }

    public void OnSpeachButtonPress()
    {
        if (!miniGameComplete)
        {
            RestartMiniGame();
        }
        else
        {
            dialogIndex++;
            switch (dialogIndex)
            {
                case 2:
                    formula.EnableFormula();
                    break;
                case 3:
                    terms.SetActive(true);
                    break;
                case 4:
                    if (levelManager.AreAllCoinsCollected())
                    {
                        levelManager.AllCoinsCollected();
                        return;
                    }
                    break;
                case 5:
                    Invoke("DeactivateMenu", 0.02f);
                    break;
            }
            string key = "picm_" + coinsCountOriginal.ToString() + "_" + dialogIndex.ToString();
            if (dialogIndex == 4) key = "picm_continue";
            talker.DisplayText(key, true);
        }
    }

    public void DeactivateMenu()
    {
        transform.position = new Vector3(50, 0, 0);
        if (picm_coins)
        {
            Destroy(picm_coins);
            picm_coins = null;
        }
        isClickingAllowed = true;
    }

    public void ActivateMenu(int _coinsCount = 0, int _chestsCount = 2)
    {
        dialogIndex = 1;
        miniGameComplete = false;
        coinsCount = coinsCountOriginal = _coinsCount;
        SetFormula(_chestsCount);
        formula.DisableFormula();
        terms.SetActive(false);
        chestGroupsIndex = _chestsCount - 2;
        DisableAllChestGroupsButOne(chestGroupsIndex);
        chestGroups[chestGroupsIndex].GetComponent<ChestGroup>().SetResult(coinsCount);
        chestGroups[chestGroupsIndex].GetComponent<ChestGroup>().DeactivateSigns();
        chest_scripts = GetComponentsInChildren<PICM_Chest>();
        transform.position = new Vector3(0, 0, 0);
        picm_coins = Instantiate(picmCoins);
        picm_coins.transform.parent = this.transform;
        picm_coins.transform.position = coinsPos.position;
        picm_coins.GetComponent<PICM_Coins>().EnableCoins(coinsCount);
        isClickingAllowed = true;
        NullAllChests();
        talker.DisplayText("picm_1", false);
    }

    void RestartMiniGame()
    {
        dialogIndex = 1;
        coinsCount = coinsCountOriginal;
        NullAllChests();
        Destroy(picm_coins);
        picm_coins = Instantiate(picmCoins);
        picm_coins.transform.parent = this.transform;
        picm_coins.transform.position = coinsPos.position;
        picm_coins.GetComponent<PICM_Coins>().EnableCoins(coinsCountOriginal);
        isClickingAllowed = true;
        talker.DisplayText("picm_1", false);
    }

    void SetFormula(int chestsCount)
    {
        formula.SetFormula(chestsCount, coinsCountOriginal / chestsCount, coinsCountOriginal);
    }

    void NullAllChests()
    {
        foreach(PICM_Chest chest in chest_scripts)
        {
            chest.NullChest();
        }
    }

    void DisableAllChestGroupsButOne(int index)
    {
        for(int i = 0; i < chestGroups.Length; i++)
        {
            if (i == index) chestGroups[i].SetActive(true);
            else chestGroups[i].SetActive(false);
        }
    }
}
