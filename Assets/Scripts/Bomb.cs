using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public AudioClip sfxExplosion;
    LevelManagerDivide levelManager;

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManagerDivide>();
    }

    private void OnMouseDown()
    {
        CreateExplosion();
        levelManager.OnBombClicked();
        Destroy(gameObject);
    }

    void CreateExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.GetComponent<ParticleSystem>().Play();
        explosion.transform.position = transform.position;
        SoundManager.GetInstance().PlaySFX(sfxExplosion);
    }
}
