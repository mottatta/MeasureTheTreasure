using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeachButtonScript : MonoBehaviour
{
    [SerializeField] LevelManagerDivide divideManager;
    [SerializeField] PutInChestMenu picm_script;
    [SerializeField] private DialogScreenScript dialogScreenScript;
    [SerializeField] private DialogScreenShipLevel shipScript;
    public ME_DialogScript me_dialogScript;
    public WrongCoinsAmountMenu wrongCoinsAmountMenu;
    public IntroUnderwaterTreasureLevelManager introUnderwaterTreasureLevelManager;
    [SerializeField] GameObject pointer;
    public AudioClip clickClip;
    float pointerDelay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ShowPointer()
    {
        pointer.SetActive(true);
    }

    void OnEnable()
    {
        pointer.SetActive(false);
        Invoke("ShowPointer", pointerDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        gameObject.SetActive(false);
        pointer.SetActive(false);
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(clickClip);
        if (dialogScreenScript) dialogScreenScript.GoToNextSpeach();
        else if (shipScript) shipScript.GoToNextSpeach();
        else if (picm_script) picm_script.OnSpeachButtonPress();
        else if (divideManager) divideManager.OnSpeachButtonPress();
        else if (me_dialogScript) me_dialogScript.GoToNextSpeach();
        else if (wrongCoinsAmountMenu) wrongCoinsAmountMenu.OnPlayButtonClicked();
        else if (introUnderwaterTreasureLevelManager) introUnderwaterTreasureLevelManager.OnSpeachButtonPressed();
    }
}
