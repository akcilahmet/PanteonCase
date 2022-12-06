using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Utils;
using UnityEngine.EventSystems;

public class GridBuilding : MonoBehaviour
{
    private GridCreator GridCreator;
    
    private void Start()
    {
        GridCreator = GetComponent<GridCreator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && BuildingManager.Instance.GetActiveBuildingSo()!=null)
        {
            Vector3 mousePosition = UtilsMethod.GetMouseWorldPosition();
            GridCreator.pathfinding.GetGrid().GetXY(mousePosition, out int x, out int z);
            Vector3 placedObjectWorldPosition =   GridCreator.pathfinding.GetGrid().GetWorldPosition(x, z);
            List<Vector2Int> buildObjectgridPosList= BuildingManager.Instance.GetActiveBuildingSo().GetGridPositionList(new Vector2Int(x, z), BuildingSO.Dir.Down);
            bool canBuild = true;
            foreach (var VARIABLE in buildObjectgridPosList)
            {
                if (GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y) == null)
                {
                    canBuild = false;
                   
                    break;
                }
                if ( !GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y).CanBuild() || !GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y).CanCharacter())
                {
                    //cannot build here
                    canBuild = false;
                    break;
                }
                if (!GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y).isWalkable)
                {
                    canBuild = false;
                    break;
                }
              
            }
            if (canBuild   && BuildingManager.Instance.GetActiveBuildingSo().buildingObj!=null)
            {
              
                var build=Instantiate(BuildingManager.Instance.GetActiveBuildingSo().buildingObj,placedObjectWorldPosition,quaternion.identity);
                if (build.gameObject.GetComponent<ProduceSoldiers>() != null)
                {
                    ProduceSoldiers produceSoldiers = build.gameObject.GetComponent<ProduceSoldiers>();
                    produceSoldiers.SetSoldier(BuildingManager.Instance.GetActiveBuildingSo().soldierPrefab.gameObject);
                    
                }
                BuildingManager.Instance.AddPowerPlantList(build.gameObject,BuildingManager.Instance.GetActiveBuildingSo());
                
               
                foreach (var VARIABLE in buildObjectgridPosList)
                {
                    GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y).SetBuilding(build.transform,BuildingManager.Instance.GetActiveBuildingSo());
                    GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y).SetIsWalkable(!GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y).isWalkable);
                }
                BuildingManager.Instance.ClearBuildingSO();
                
            }
            else if(!canBuild )
            {
                Debug.Log("build varr");
            }
           
           
        }
        
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePosition = UtilsMethod.GetMouseWorldPosition();
            GridCreator.pathfinding.GetGrid().GetXY(mousePosition, out int x, out int z);
            BuildingSO tempSO = GridCreator.pathfinding.GetNode(x, z).GetBuilding();
            
            if (tempSO != null)
            {
                InformationController.Instance.SetInformationPanel(tempSO.uıImage,tempSO.name,
                    tempSO.ınformationSoldierObj,tempSO.typeOfSoldierName);
               
                
            }
            
          

        }
    }

    
    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = UtilsMethod.GetMouseWorldPosition();
        GridCreator.pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);

        if (BuildingManager.Instance.GetActiveBuildingSo()!=null )
        {

            Vector3 placedObjectWorldPosition = GridCreator.pathfinding.GetGrid().GetWorldPosition(x, y);
                                                
            return placedObjectWorldPosition;
        } else {
            return mousePosition;
        }
    }


  
 
}
