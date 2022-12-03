using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore;

public class BuildGhostMove : MonoBehaviour
{
    public GridBuilding _gridBuilding;
    [SerializeField] private Transform ghostObj;
    [SerializeField] private bool itemSelected;
    private void Start()
    {
        BuildingManager.Instance.ghostEvent += Instance_OnSelectedItemGhost;
    }

    private void LateUpdate()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 targetPos = _gridBuilding.GetMouseWorldSnappedPosition();
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 15f);
            if(ghostObj!=null)
                ghostObj.gameObject.SetActive(true);
        }

        if (EventSystem.current.IsPointerOverGameObject() || BuildingManager.Instance.GetActiveBuildingSo()==null)
        { 
            if(ghostObj!=null)
                ghostObj.gameObject.SetActive(false);
        }

        
       
    }

    
    private void Instance_OnSelectedItemGhost() {
        GhostBuildCreate();
        
    }
    void GhostBuildCreate()
    {
        
        if (BuildingManager.Instance.GetActiveBuildingSo() != null)
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            ghostObj = Instantiate(BuildingManager.Instance.GetActiveBuildingSo().visual, transform.position, Quaternion.identity);
            ghostObj.transform.SetParent(transform);
            itemSelected = true;

        }
       
    }
}