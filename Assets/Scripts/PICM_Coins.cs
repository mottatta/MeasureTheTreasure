using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PICM_Coins : MonoBehaviour
{
    public GameObject[] coins;
    public int coinsCount;

    public void EnableCoins(int count)
    {
        DisableAllCoins();
        coinsCount = count;
        if (count <= 0) return;
        for(int i = 0; i < coins.Length; i++)
        {
            if(count > i) coins[i].SetActive(true);
        }
    }

    void DisableAllCoins()
    {
        foreach(GameObject coin in coins)
        {
            coin.SetActive(false);
        }
    }
}
