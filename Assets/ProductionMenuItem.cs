using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class ProductionMenuItem : MonoBehaviour
{
    [SerializeField] private Image ımage;
    [SerializeField] private BuildingSO buildingSO;
    
    [Header("BtnClick")][Space(10)]
    private Button button;
    [SerializeField] private AnimationCurve curve = null;
   
   
    private void Awake()
    {
        LevelCreator.Instance.AddProductionMenuItemList(this);
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => GetBuildingPrefabObject());
    }
    
    public BuildingSO GetBuildingPrefabObject()
    {
        if (buildingSO != null)
        {
            Debug.Log(buildingSO.name);
            
            InformationController.Instance.SetInformationPanel(buildingSO.uıImage,buildingSO.name,
                buildingSO.typeOfSoldierProducedSprite,buildingSO.typeOfSoldierName);
           
            BuildingManager.Instance.SetGetActiveBuildingSo(buildingSO);
            BuildingManager.Instance.DeselectGhostObj();
            return BuildingManager.Instance.GetActiveBuildingSo() ;
        }

        return null;
    }
    
    
    public void ProductionMenuItemUpdate(Sprite targetSprite, BuildingSO buildingSO)
    {
        ItemImageUpdate(targetSprite);
        ProductionMenuSOUpdate(buildingSO);
    }
    void ItemImageUpdate(Sprite targetSprite)
    {
        ımage.sprite = targetSprite;
    }

    void ProductionMenuSOUpdate(BuildingSO buildingSO)
    {
        this.buildingSO = buildingSO;
    }
}
