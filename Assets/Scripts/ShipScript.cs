using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public MastScript[] mastScripts;
    public Color[] colors;
    [SerializeField] GameObject buildMenu;
    [SerializeField] DialogScreenShipLevel talkerScript;
    [SerializeField] GameObject[] platna;
    [SerializeField] BuoyancyEffector2D buoyancy;
    int currentMast = 0;

    public int[] results;

    public AudioClip clickClip;
    public AudioClip success;
    public AudioClip fail;
    public AudioClip completeClip;

    void Start()
    {
        SetMastsColors();
    }

    public void OnBuildButtonPressed(string action)
    {
        switch (action)
        {
            case "up":
                BuildUp();
                break;
            case "down":
                BuildDown();
                break;
            case "done":
                BuildDone();
                break;
        }
    }

    private void BuildUp()
    {
        SoundManager.GetInstance().PlaySFX(clickClip);
        mastScripts[currentMast].SetHeight(mastScripts[currentMast].val + 1);
    }

    private void BuildDown()
    {
        SoundManager.GetInstance().PlaySFX(clickClip);
        mastScripts[currentMast].SetHeight(mastScripts[currentMast].val - 1);
    }

    private void BuildDone()
    {
        if (mastScripts[currentMast].val == results[currentMast])
        {
            buildMenu.SetActive(false);
            mastScripts[currentMast].ShowParticles();
            //success
            if (currentMast + 1 < mastScripts.Length)
            {
                SoundManager.GetInstance().PlaySFX(success);
                talkerScript.GoToNextSpeach();
            }
            else
            {
                //All tasks complete
                Debug.Log("All tasks complete!");
                SoundManager.GetInstance().PlaySFX(completeClip);
                Invoke("ShipSailAway", 0.70f);
                talkerScript.GoToNextSpeach();//when talker say his final level speach will call transition fade in and it will go to next scene
            }
        }
        else
        {
            //buildin failed as player didn't made the mast == to results[currentMast]
            SoundManager.GetInstance().PlaySFX(fail);
            talkerScript.SpeakSameSpeach();
        }
    }

    private void ShipSailAway()
    {
        EnablePlatna();
        MakeWaterFlow();
    }

    private void EnablePlatna()
    {
        foreach(GameObject obj in platna)
        {
            obj.SetActive(true);
        }
    }

    private void MakeWaterFlow()
    {
        buoyancy.flowVariation = 0.70f;
    }

    public void EnableNextTask()
    {
        SharedState.SubmitProgress();
        currentMast++;
        buildMenu.SetActive(true);
    }

    private void SetMastsColors()
    {
        for(int i = 0; i < mastScripts.Length; i++)
        {
            mastScripts[i].ChangeColor(colors[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
