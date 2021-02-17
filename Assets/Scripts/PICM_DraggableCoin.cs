using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PICM_DraggableCoin : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    Vector3 originalPosition;
    bool isDragged = false;
    public int val;
    public Text txt;


    private void Start()
    {
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        isDragged = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        if (isDragged)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    public void SetVal(int n)
    {
        val = n;
        txt.text = val.ToString();
    }

    public void ReturnToOriginalPosition()
    {
        isDragged = false;
        transform.position = originalPosition;
    }
}
