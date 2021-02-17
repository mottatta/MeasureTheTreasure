using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class ClawScript : MonoBehaviour
{
    [SerializeField] GameObject origin;
    public float speed = 4f;
    public Transform stickPoint;
    [SerializeField] GunScript gun;

    private bool isMoving;
    private Vector3 target;
    private GameObject childObject;
    [SerializeField] private LineRenderer lineRenderer;
    private bool hitJewel;
    private bool retracting;
    
    public AudioClip sfxCoin;
    public AudioClip sfxRope;
    public AudioClip sfxAnchor;
    public AudioClip sfxRock;

    public GameObject picm;

    // Start is called before the first frame update
    void Awake()
    {
        isMoving = false;
        lineRenderer = GetComponent<LineRenderer>();
        target = Vector3.zero;
        childObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (picm != null)
        {
            if (picm.transform.position.x == 0) return;
        }
        if (target != Vector3.zero)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        else
        {
            Debug.Log("zeroooo");
        }
        
        lineRenderer.SetPosition(0, origin.transform.position);
        lineRenderer.SetPosition(1, transform.position);
        if (childObject != null)
        {
            childObject.GetComponent<CollectableObjectScript>().MoveAccordingTo(stickPoint.position);
        }
        bool areEqual = V3Equal(transform.position, origin.transform.position);
        //Debug.Log(areEqual);
        if (V3Equal(transform.position, origin.transform.position) && retracting)
        {
            if(childObject != null)
            {
                RotatingCoinScript coinScript = childObject.GetComponent<RotatingCoinScript>();
                if (coinScript)
                {
                    if(SoundManager.GetInstance() != null) SoundManager.GetInstance().PlaySFX(sfxCoin);
                    coinScript.OnCollected();
                }
                else {
                    if (SoundManager.GetInstance() != null) SoundManager.GetInstance().PlaySFX(sfxRock);
                }
            }
            OnClawBack();
        }

    }

    public bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.10;
    }

    private void OnClawBack()
    {
        gun.CollectObject();
        if (hitJewel)
        {
            hitJewel = true;
        }
        Destroy(childObject);
        childObject = null;
        gameObject.SetActive(false);
    }

    public void SetIsMoving(bool val)
    {
        isMoving = val;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void SetTarget(Vector3 pos)
    {
        target = pos;
        if (SoundManager.GetInstance() != null) SoundManager.GetInstance().PlaySFX(sfxRope);
    }

    void OnTriggerEnter(Collider other)
    {
        if (childObject != null) return;
        retracting = true;
        target = origin.transform.position;
        if (other.gameObject.CompareTag("jewel"))
        {
            hitJewel = true;
        }
        if (other.gameObject.tag != "limiter" && other.gameObject.GetComponent<CollectableObjectScript>() != null)
        {
            childObject = other.gameObject;
            Vector3 offset = stickPoint.position - childObject.transform.position;
            childObject.GetComponent<CollectableObjectScript>().offset = offset;
            if (SoundManager.GetInstance() != null) SoundManager.GetInstance().PlaySFX(sfxAnchor);
            int val = 0;
            if (childObject.GetComponent<RotatingCoinScript>())
            {
                val = childObject.GetComponent<RotatingCoinScript>().GetVal();
            }
        }
    }

    public GameObject GetChildObject()
    {
        return childObject;
    }
}
