using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2000)]
public class LevelCreator : MonoBehaviour
{
    [SerializeField] private ProductionMenuSO productionMenuData;
    [SerializeField] private List<ProductionMenuItem> productionMenuItems; 
    #region Singleton

    public static LevelCreator Instance { get; private set; }
    
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
    
    private void Start()
    {
        ProductionMenuCreator();
    }

    void ProductionMenuCreator()
    {
        for (int i = 0; i < productionMenuItems.Count; i++)
        {
            productionMenuItems[i].ProductionMenuItemUpdate(productionMenuData.productionProperties[i].buildingSo.uıImage,productionMenuData.productionProperties[i].buildingSo);
        }
       
    }
    public void AddProductionMenuItemList(ProductionMenuItem temp)
    {
        productionMenuItems.Add(temp);
    }
}
