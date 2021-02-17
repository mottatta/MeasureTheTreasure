using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectableObjectScript : MonoBehaviour
{
    public Vector3 offset;

    public void MoveAccordingTo(Vector3 targetPos)
    {
        transform.position = targetPos - offset;
    }
}
