using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(2900)]
public class InformationController : MonoBehaviour
{
    public RectTransform InformationUpRectTransform;
    public Image InformationUpImage;
    public TMP_Text InformationUptext; 
    
    public Image InformationDownImage;
    public TMP_Text InformationDowntext;
    
    public Transform ─▒nformationPanelRawImageSoldierCreatedPoint;

    [SerializeField] private TMP_Text canNotBeBuildTxt;

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

    public void SetInformationPanel(Sprite upImageSprite,string upTextString,GameObject ─▒nformationSoldier,string downTextString)
    {
        ClearInformationPanelRawImageSoldierCreatedPoint();
        DOScale(InformationUpRectTransform.transform,new Vector3(1.2f, 1.2f, 1.2f),Vector3.one,"one");

        DOScale(InformationDownImage.transform,new Vector3(1.2f, 1.2f, 1.2f),Vector3.one,"two");

        InformationUpImage.sprite = upImageSprite;

        InformationUptext.text = upTextString.ToString();

        InformationDowntext.text = downTextString.ToString();

        /*if (─▒nformationSoldier.gameObject == null)
        {
            BuildingManager.Instance.testText.text = "btn click  8";

            InformationDownImage.gameObject.SetActive(false);
            BuildingManager.Instance.testText.text = "btn click  9";

            ClearInformationPanelRawImageSoldierCreatedPoint();
            BuildingManager.Instance.testText.text = "btn click  10";

        }*/
       // if (─▒nformationSoldier.gameObject != null)
        {
            ClearInformationPanelRawImageSoldierCreatedPoint();

            var go = Instantiate(─▒nformationSoldier, ─▒nformationPanelRawImageSoldierCreatedPoint.transform.position, Quaternion.identity);

            go.transform.SetParent(─▒nformationPanelRawImageSoldierCreatedPoint);

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

    void ClearInformationPanelRawImageSoldierCreatedPoint()
    {
        foreach (Transform child in ─▒nformationPanelRawImageSoldierCreatedPoint.transform)
        {
            if (child.childCount > 0)
            {
                Destroy(child.gameObject);

            }
        }
    }


    public IEnumerator  CanNotBuildTxtState(Vector3 target)
    {
        
        canNotBeBuildTxt.transform.localPosition = target;
        canNotBeBuildTxt.transform.DOScale(Vector3.one, .3f).OnComplete((() =>
        {
            canNotBeBuildTxt.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .6f).SetLoops(-1, LoopType.Yoyo).SetId("cannottext");

        }));
        yield return new WaitForSeconds(1f);
        DOTween.Kill("cannottext");
        canNotBeBuildTxt.transform.DOScale(Vector3.zero, .3f);
    }
   
}
