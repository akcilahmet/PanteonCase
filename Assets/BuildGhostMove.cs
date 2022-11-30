using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildGhostMove : MonoBehaviour
{
    public GridBuilding _gridBuilding;
    [SerializeField] private Transform ghostObj;
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

        if (EventSystem.current.IsPointerOverGameObject())
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
        
        if (BuildingManager.Instance.buildingSo != null)
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            ghostObj = Instantiate(BuildingManager.Instance.buildingSo.visual, transform.position, Quaternion.identity);
            ghostObj.transform.SetParent(transform);
           
        }
       
    }
}
