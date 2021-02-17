using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBubbleSpot : MonoBehaviour
{
    public int index;
    public bool isFree = true;
    public DraggableBubble bubbleOnTop = null;

    public void PlaceBubble(DraggableBubble bubble)
    {
        UnplaceBooble();
        isFree = false;
        bubbleOnTop = bubble;
    }

    public void UnplaceBooble()
    {
        isFree = true;
        bubbleOnTop = null;
    }
}
