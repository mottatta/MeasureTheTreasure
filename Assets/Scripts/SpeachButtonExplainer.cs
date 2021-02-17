using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeachButtonExplainer : MonoBehaviour
{
    [SerializeField] ExplainerScript script;
    public AudioClip clip;
    private void OnMouseDown()
    {
        if(SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(clip);
        if(script != null) script.GoToNextSpeach();
        gameObject.SetActive(false);
    }
}
