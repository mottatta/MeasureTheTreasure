using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableBubble: MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public Vector3 originalPosition;
    public bool isDragged = false;
    public string val;
    public Text txt;
    public Collider2D colliderObject = null;
    public int index;
    public int alternativeIndex;
    public bool isDraggable = true;
    public bool isEquationReady = false;
    public ME_LevelManager levelManager;
    public GameObject pointerReminder;

    private void Start()
    {
        SetVal(val);
    }

    void OnMouseDown()
    {
       
        if (isEquationReady)
        {
            if (val == "?")
            {
                levelManager.ShowEquationSolutions();
                pointerReminder.SetActive(false);
            }
        }
        else
        {
            if (!isDraggable) return;
            if (isDragged == false) levelManager.PlaySFX(levelManager.sfxSelect);
            isDragged = true;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    private void OnMouseUp()
    {
        if (!isDraggable) return;
        if (colliderObject != null)
        {
            if(transform.parent != colliderObject.transform.parent) transform.parent = colliderObject.transform.parent;
            transform.position = colliderObject.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition.z);
            //transform.position.Set(colliderObject.transform.position.x, colliderObject.transform.position.y, 0);

            StaticBubbleSpot spot = colliderObject.GetComponent<StaticBubbleSpot>();

            if (!spot.isFree)
            {
                spot.bubbleOnTop.ReturnToOriginalPosition();
            }
            levelManager.PlaySFX(levelManager.sfxPlaceBubble);
            spot.PlaceBubble(this);
            isDragged = false;
            Debug.Log(index + " " + spot.index);
        }
        else
        {
            levelManager.PlaySFX(levelManager.sfxShowSolutions);//make woosh sound
            transform.parent = Camera.main.transform;
            transform.position = originalPosition;
            isDragged = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "staticBubble")
        {
            StaticBubbleSpot spot = collision.GetComponent<StaticBubbleSpot>();
            colliderObject = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "staticBubble")
        {
            if (colliderObject != null)
            {
                StaticBubbleSpot spot = colliderObject.GetComponent<StaticBubbleSpot>();
                if(spot.bubbleOnTop == this) spot.UnplaceBooble();
                colliderObject = null;
            }
        }
    }

    void OnMouseDrag()
    {
        if (!isDraggable) return;
        if (isDragged)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    public void SetVal(string n)
    {
        val = n;
        txt.text = val.ToString();
    }

    public void ReturnToOriginalPosition()
    {
        colliderObject = null;
        isDragged = false;
        transform.position = originalPosition;
        transform.parent = Camera.main.transform;
    }
}
