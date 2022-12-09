using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(3000)]
public class ProductionMenuItem : MonoBehaviour
{
    [SerializeField] private Image ımage;
    [SerializeField] private BuildingSO buildingSO;
    
    [Header("BtnClick")][Space(10)]
    private Button button;
    [SerializeField] private AnimationCurve curve = null;

   
    private void Awake()
    {
        button = GetComponent<Button>();
        LevelCreator.Instance.AddProductionMenuItemList(this);
    }

    private void Start()
    {
       
        button.onClick.AddListener(() => GetBuildingPrefabObject());
    }
    
    public BuildingSO GetBuildingPrefabObject()
    {
        BuildingManager.Instance.SetGetActiveBuildingSo(buildingSO);
        
        InformationController.Instance.SetInformationPanel(buildingSO.uıImage,buildingSO.name,
                buildingSO.ınformationSoldierObj,buildingSO.typeOfSoldierName);
        
        BuildingManager.Instance.DeselectGhostObj();
       
        
        return BuildingManager.Instance.GetActiveBuildingSo() ;
      
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
