using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PICM_Chest : MonoBehaviour
{
    [SerializeField] public GameObject[] coins;
    public int coinsCount;
    int currentCoin = 0;
    public Text txt;
    int val;
    GameObject picm;
    PutInChestMenu picm_script;

    public Animator raysAnimator;

    void Start()
    {
        SetVal(0);
        picm = GameObject.FindGameObjectWithTag("PICM");
        picm_script = picm.GetComponent<PutInChestMenu>();
    }

    public void NullChest()
    {
        SetVal(0);
    }

    void SetVal(int v)
    {
        HideAllCoins();
        val = v;
        if(v > 0)
        {
            for (int i = 0; i < coins.Length; i++) if(v > i) coins[i].SetActive(true);
        }
        txt.text = v.ToString();
    }

    public int GetVal()
    {
        return val;
    }

    void HideAllCoins()
    {
        foreach (GameObject coin in coins)
        {
            coin.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (picm.activeInHierarchy)
        {
            SetVal(val + 1);
            picm_script.OnCoinPutInChest();
            raysAnimator.Play("RaysRotate");
        }
        Destroy(collision.gameObject);
    }
}
