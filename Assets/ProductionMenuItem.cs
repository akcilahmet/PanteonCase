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

    private void Awake()
    {
        LevelCreator.Instance.AddProductionMenuItemList(this);
    }

    public void ProductionMenuItemUpdate(Sprite targetSprite, BuildingSO buildingSO)
    {
        ItemImageUpdate(targetSprite);
        ProductionMenuSOUpdate(buildingSO);
    }

    public GameObject GetBuildingPrefabObject()
    {
        if (buildingSO != null)
        {
            return buildingSO.prefab.gameObject;
        }

        return null;
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
