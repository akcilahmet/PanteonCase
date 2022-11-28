
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScrollView : MonoBehaviour ,IBeginDragHandler, IDragHandler, IScrollHandler
{

    private Vector2 lastPos;
    private bool drag;
    private ScrollRect scrollRect;
    public ScrollContent scrollContent;
    public float outOfBoundsThreshold;

    //public float test;
    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        drag = eventData.position.y > lastPos.y;
        lastPos = eventData.position;
    }

    public void OnScroll(PointerEventData eventData)
    {
        drag = eventData.scrollDelta.y > 0;
    }
    
    public void OnViewScroll()
    {
        HandleVerticalScroll();
    }

    void HandleVerticalScroll()
    {
        int currentItemIndex = drag ? scrollRect.content.childCount - 1 : 0;
        var currentItem = scrollRect.content.GetChild(currentItemIndex);
        if (!ReachedThres(currentItem))
        {
            return;
        }
        
        int endItemIndex = drag ? 0 : scrollRect.content.childCount - 1;
        
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (drag)
        {
            newPos.y = endItem.position.y - scrollContent.childHeight * 2f + scrollContent.itemSpacing;
        }
        else
        {
            newPos.y = endItem.position.y + scrollContent.childHeight * 2f - scrollContent.itemSpacing;
        }

        currentItem.position = newPos;
        currentItem.SetSiblingIndex(endItemIndex);
    }

    bool ReachedThres(Transform item)
    {
       
        float posYThresHold = transform.position.y + scrollContent.height * .6f + outOfBoundsThreshold;
        float negYThresHold = transform.position.y - scrollContent.height * .6f - outOfBoundsThreshold;
       
        return drag
            ? item.position.y - scrollContent.childHeight * .6f > posYThresHold
            : item.position.y + scrollContent.childHeight * .6f < negYThresHold;
            
        
      
    }
    
    
  
}
