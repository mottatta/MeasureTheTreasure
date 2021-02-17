using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DivideCoin : MonoBehaviour
{
    public int val;
    public Text txt;
    public GameObject halfCoin;
    public GameObject bombPrefab;
    public int targetVal;
    public GameObject particles;
    public AudioClip selectClip;
    public AudioClip collectedClip;
    public Transform[] newCoinSpots;
    public int maxBombsCount = 5;
    void Start()
    {
        txt.text = val.ToString();
    }

    void SetVal(int v)
    {
        val = v;
        txt.text = v.ToString();
    }

    void OnMouseDown()
    {
        if (val == targetVal)
        {
            if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(collectedClip);
            Collect();
        }
        else
        {
            if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(selectClip);
            int newVal = val / 2;
            Vector3 t = transform.right * 3f;
            if (Random.Range(0.0f, 1.0f) > 0.5f) t = transform.right * -3f;
            if (val % 2 == 0)
            {
                CreateHalfCoin(transform.right * 2,newVal, newCoinSpots[0]);
                CreateHalfCoin(transform.right * -2, newVal, newCoinSpots[1]);
                if (Random.Range(0.0f, 1.0f) > 0.5f) CreateBomb(t, newCoinSpots[2]);
                Destroy(gameObject);
            }
            else{
                newVal = val / 3;
                CreateHalfCoin(transform.right * 2, newVal, newCoinSpots[0]);
                CreateHalfCoin(transform.right * -2, newVal, newCoinSpots[1]);
                CreateHalfCoin(new Vector3(0, 0, 0), newVal, newCoinSpots[2]);
                if(Random.Range(0.0f, 1.0f) > 0.5f) CreateBomb(t, newCoinSpots[3]);
                Destroy(gameObject);
            }
        }
    }

    void Collect()
    {
        GameObject p = Instantiate(particles);
        p.transform.position = transform.position;
        GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManagerDivide>().OnCoinCollected();
        Destroy(gameObject);
    }

    void CreateHalfCoin(Vector3 pos, int newVal, Transform startPoint)
    {
        float speed = 4f;
        float upSpeed = 8f;
        GameObject newCoin = Instantiate(halfCoin);
        DivideCoin coinScript = newCoin.GetComponent<DivideCoin>();
        coinScript.targetVal = targetVal;
        coinScript.SetVal(newVal);
        newCoin.transform.position = startPoint.position;
        newCoin.GetComponent<Rigidbody2D>().AddForce(transform.up * upSpeed, ForceMode2D.Impulse);
        newCoin.GetComponent<Rigidbody2D>().AddForce(pos * speed, ForceMode2D.Impulse);
    }

    void CreateBomb(Vector3 pos, Transform startPoint)
    {
        Bomb[] bombs = GameObject.FindObjectsOfType<Bomb>();
        if (bombs.Length > maxBombsCount) return;
        float speed = 4f;
        float upSpeed = 8f;
        GameObject bomb = Instantiate(bombPrefab);
        bomb.transform.position = startPoint.position;
        bomb.GetComponent<Rigidbody2D>().AddForce(transform.up * upSpeed, ForceMode2D.Impulse);
        bomb.GetComponent<Rigidbody2D>().AddForce(pos * speed, ForceMode2D.Impulse);
    }
}
