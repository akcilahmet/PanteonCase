using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationController : MonoBehaviour
{
    public Image InformationUpImage;
    public TMP_Text InformationUptext; 
    
    public Image InformationDownImage;
    public TMP_Text InformationDowntext;


    #region Singleton

    public static InformationController Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("EXTRA : " + this + "  SCRIPT DETECTED RELATED GAME OBJ DESTROYED");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
      
        
    }

    #endregion

    public void SetInformationPanel(Sprite upImageSprite,string upTextString,Sprite downImageSprite,string downTextString)
    {
        DOScale(InformationUpImage.transform,new Vector3(1.2f, 1.2f, 1.2f),Vector3.one,"one");
        DOScale(InformationDownImage.transform,new Vector3(1.2f, 1.2f, 1.2f),Vector3.one,"two");
        
        InformationUpImage.sprite = upImageSprite;
        InformationUptext.text = upTextString.ToString();
        
        InformationDownImage.sprite = downImageSprite;
        InformationDowntext.text = downTextString.ToString();
        if (InformationDownImage.sprite == null)
        {
            InformationDownImage.gameObject.SetActive(false);
        }
        else
        {
            InformationDownImage.gameObject.SetActive(true);

        }


    }

    private void DOScale(Transform temp, Vector3 targetVec, Vector3 originVec, string id)
    {
        temp.transform.DOScale(targetVec, .5f).OnComplete((() =>
        {
            temp.transform.DOScale(originVec, .5f).SetId(id);
        })).SetId(id);
    }
        
   

   
}
