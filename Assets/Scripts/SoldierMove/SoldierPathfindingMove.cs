using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.EventSystems;

public class SoldierPathfindingMove : MonoBehaviour
{
    public SoldierPathfindingMovementHandler soldierPathfindingMovementHandler;
    public Vector2 selectedSoldierGridXY;
    #region Singleton

    public static SoldierPathfindingMove Instance { get; private set; }
    
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
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsMethod.GetMouseWorldPosition();
            Pathfinding.Instance.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = Pathfinding.Instance.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }

            if (soldierPathfindingMovementHandler != null )
            {
                soldierPathfindingMovementHandler.SetTargetPosition(mouseWorldPosition);
                GridCreator.Instance.pathfinding.GetNode(x,y).SetCharacter( soldierPathfindingMovementHandler.gameObject,BuildingManager.Instance.GetActiveOldBuildingSo());
                GridCreator.Instance.pathfinding.GetNode((int)selectedSoldierGridXY.x,(int)selectedSoldierGridXY.y).ClearCharacter();
            }

        }
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePosition = UtilsMethod.GetMouseWorldPosition();
            GridCreator.Instance.pathfinding.GetGrid().GetXY(mousePosition, out int x, out int z);
            BuildingSO tempSO = GridCreator.Instance.pathfinding.GetNode(x, z).GetBuilding();
            selectedSoldierGridXY = new Vector2(x, z);
            if (tempSO != null )
            {
               
                if (GridCreator.Instance.pathfinding.GetNode(x, z).GetCharacter()!=null)
                {
                    soldierPathfindingMovementHandler =
                        GridCreator.Instance.pathfinding.GetNode(x, z).GetCharacter().gameObject.GetComponent<SoldierPathfindingMovementHandler>();
                }
                
                InformationController.Instance.SetInformationPanel(tempSO.uıImage,tempSO.name,
                    tempSO.ınformationSoldierObj,tempSO.typeOfSoldierName);
            }
            
          

        }
    }
    public void ClearCharacterPathfinding()
    {
        soldierPathfindingMovementHandler = null;
    }

    
}
