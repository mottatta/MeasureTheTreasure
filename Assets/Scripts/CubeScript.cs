using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CubeScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GetAShareLevelScript levelScript;
    [SerializeField] GameObject clickToRollCanvas;
    [SerializeField] GameObject number1Canvas;
    [SerializeField] int index;
    [SerializeField] GameObject diceToBeEnabled;
    [SerializeField] float minThrowForce = 10f;
    [SerializeField] float maxThrowForce = 15f;
    [SerializeField] float minRollForce = 300f;
    [SerializeField] float maxRollForce = 500f;

    bool isThrown;
    bool endSent;
    private int _upSide;
    Quaternion originalRotation;
    Vector3 originalPosition = Vector3.zero;

    public AudioClip rollClip;
    public AudioClip collisionClip;
    public GameObject diceCamera;

    // Start is called before the first frame update
    void Awake()
    {
        ShowClickToRoll();
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.30f;
        originalRotation = transform.rotation;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isThrown)
        {
            if(rb.IsSleeping() && transform.rotation != originalRotation)
            {
                if (!endSent)
                {
                    endSent = true;
                    OnEndRoll();
                }        
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(diceCamera.activeSelf && SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(collisionClip);
    }

    private void OnEndRoll()
    {
        int val = _upSide;
        switch (_upSide)
        {
            case 1:
            case 6:
                val = 2;
                break;
            case 2:
            case 5:
                val = 4;
                break;
            case 3:
            case 4:
                if (index == 1) val = 8;
                else val = 6;
                break;

        }
        switch (index)
        {
            case 1:
                levelScript.SetNextLevelVal(val);
                diceToBeEnabled.SetActive(true);
                break;
            case 2:
                levelScript.SetNumberOfGroupsToCompleteTheTask(val);
                Invoke("OnSecondDiceStop", 2f);
                break;
            case 3:

                break;
        }
    }

    private void OnSecondDiceStop()
    {
        levelScript.OnSecondDiceStop();
    }

    public void ResetDice()
    {
        gameObject.SetActive(true);
        isThrown = false;
        endSent = false;
        ShowClickToRoll();
        transform.rotation = originalRotation;
        transform.position = originalPosition;
    }

    private void OnMouseDown()
    {
        Throw();
    }

    void Throw()
    {
        if (isThrown) return;
        if(index == 1) SharedState.SubmitProgress();
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(rollClip);
        float force = Random.Range(minThrowForce, maxThrowForce);
        rb.AddForce(transform.up * force, ForceMode.Impulse);
        Invoke("Roll", Random.Range(0.1f, 0.2f));
        isThrown = true;
    }

    void Roll()
    {
        ShowNumber1();
        Vector3 rot = new Vector3(Random.Range(minRollForce, maxRollForce), Random.Range(minRollForce, maxRollForce), Random.Range(minRollForce, maxRollForce));
        rb.AddTorque(rot,ForceMode.Impulse);
    }

    void ShowClickToRoll()
    {
        clickToRollCanvas.SetActive(true);
        number1Canvas.SetActive(false);
    }

    void ShowNumber1()
    {
        clickToRollCanvas.SetActive(false);
        number1Canvas.SetActive(true);
    }

    public int upSide
    {
        get
        {
            return _upSide;
        }
        set
        {
            _upSide = value;
        }
    }
}
