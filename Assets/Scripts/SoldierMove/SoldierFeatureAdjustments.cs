using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoldierFeatureAdjustments : MonoBehaviour
{
   

   private void Start()
   {
      foreach (Transform child in transform)
      {
         if (child.GetComponent<SpriteRenderer>() != null)
         {
            child.GetComponent<SpriteRenderer>().sortingLayerName = "build";
         }
      } 
      foreach (Transform child in transform)
      {
         if (child.GetComponent<SortingGroup>() != null)
         {
            child.GetComponent<SortingGroup>().sortingLayerName = "build";
         }
      }
   }
}
