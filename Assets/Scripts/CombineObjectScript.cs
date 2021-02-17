using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineObjectScript : MonoBehaviour
{
    public bool isSelected;
    [SerializeField] int val;
    [SerializeField] int combinersCount;
    [SerializeField] GameObject newObject;
    Animator animator;
    GameObject combiner;
    CombinerScript combinerScript;
    GameObject levelManager;
    public GameObject hint;

    private float moveSpeed = 20f;
    private GameObject target = null;

    public AudioClip selectClip;

    void Awake()
    {
        animator = GetComponent<Animator>();
        combiner = GameObject.FindGameObjectWithTag("combiner");
        combinerScript = combiner.GetComponent<CombinerScript>();
        levelManager = GameObject.FindGameObjectWithTag("levelManager");
        HideHint();
    }

    public void ShowHint()
    {
        hint.SetActive(true);
    }

    public void HideHint()
    {
        hint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void AddNewObject()
    {
        GameObject obj = Instantiate(newObject);
        obj.transform.position = transform.position;
        obj.transform.parent = transform.parent;
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    private void OnMouseDown()
    {
        int nextLevelVal;
        //if the object consist of more objects that the level target is do not mark
        if (levelManager.GetComponent<LevelManagerCombinerScript>())
        {
            nextLevelVal = levelManager.GetComponent<LevelManagerCombinerScript>().GetNextLevelVal();
        }
        else nextLevelVal = levelManager.GetComponent<GetAShareLevelScript>().GetNextLevelVal();
        if (combinersCount > nextLevelVal || val == nextLevelVal) return;
        if (!combinerScript.IsSelectingAllowed()) return;
        if (!isSelected)
        {
            Select();
        }
        else if(isSelected == true)
        {
            Unselect();
        }
    }

    public void Select()
    {
        if(SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(selectClip);
        isSelected = true;
        //animator.Play("CO_CoinSelected");
        animator.SetBool("isSelected", true);
        combinerScript.OnObjectSelected(gameObject);
    }

    public void Unselect()
    {
        isSelected = false;
        //animator.Play("CO_CoinNotSelected");
        animator.SetBool("isSelected", false);
        combinerScript.OnObjectUnselected(gameObject);
    }

    public int GetVal()
    {
        return val;
    }

    public int GetCombinersCount()
    {
        return combinersCount;
    }
}
