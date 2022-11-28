using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContent : MonoBehaviour
{

    private RectTransform rectTransform;
    private RectTransform[] rectChild;

    public float childHeight;
    public float height;
  
    public float verticalMargin=10,horizontalMargin;
    public float itemSpacing;
    
   
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectChild = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rectChild[i]=rectTransform.GetChild(i).GetComponent<RectTransform>();
        }
        
        //width = rectTransform.rect.width - (2 * horizontalMargin);
        height = rectTransform.rect.height - (2 * verticalMargin);
        
       // childWidth = rectChild[0].rect.width;
        childHeight = rectChild[0].rect.height;

       
        ContentVertical();
        
        

    }


    void ContentVertical()
    {
        float originY = 0 - (height * 0.5f);
        float posOffSet = childHeight * .5f;

        for (int i = 0; i < rectChild.Length; i++)
        {
            Vector2 childPos = rectChild[i].localPosition;
            childPos.y = originY + posOffSet + i * (childHeight+itemSpacing);
            rectChild[i].localPosition=childPos;
        }
    }
}
