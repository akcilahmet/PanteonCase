using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridProperties : MonoBehaviour
{
    [SerializeField] private SpriteRenderer InnerSprite;
    [SerializeField] private SpriteRenderer OuterSprite;
    public GridCell GridCell;

    public void SetInnerColor(Color col)
    {
        InnerSprite.color = col;
    } 
    public void SetOuterColor(Color col)
    {
        OuterSprite.color = col;
    }
}
