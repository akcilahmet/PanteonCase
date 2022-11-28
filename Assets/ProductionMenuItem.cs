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
    private float time;
    public bool selectedItem;
    private void Awake()
    {
        LevelCreator.Instance.AddProductionMenuItemList(this);
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => GetBuildingPrefabObject());
    }

    private void Update()
    {
        if(selectedItem)
            ClickedAnimationItem();
    }

    public BuildingSO GetBuildingPrefabObject()
    {
        if (buildingSO != null)
        {
            Debug.Log(buildingSO.name);
            selectedItem = true;
            
            return BuildingManager.Instance.buildingSo=buildingSO;
        }

        return null;
    }

    public void ClickedAnimationItem()
    {
        time += Time.deltaTime;
        transform.localScale = new Vector3(curve.Evaluate(time), curve.Evaluate(time), 1);
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
