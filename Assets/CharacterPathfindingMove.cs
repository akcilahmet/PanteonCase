using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.EventSystems;

public class CharacterPathfindingMove : MonoBehaviour
{
    public CharacterPathfindingMovementHandler CharacterPathfindingMovementHandler;

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
            CharacterPathfindingMovementHandler.SetTargetPosition(mouseWorldPosition);
        }
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePosition = UtilsMethod.GetMouseWorldPosition();
            GridCreator.Instance.pathfinding.GetGrid().GetXY(mousePosition, out int x, out int z);
            BuildingSO tempSO = GridCreator.Instance.pathfinding.GetNode(x, z).GetBuilding();
            
            if (tempSO != null )
            {
                if (GridCreator.Instance.pathfinding.GetNode(x, z).GetCharacter()!=null)
                {
                    CharacterPathfindingMovementHandler =
                        GridCreator.Instance.pathfinding.GetNode(x, z).GetCharacter().gameObject.GetComponent<CharacterPathfindingMovementHandler>();
                }
               
                Debug.Log("move" + CharacterPathfindingMovementHandler);
                InformationController.Instance.SetInformationPanel(tempSO.uıImage,tempSO.name,
                    tempSO.typeOfSoldierProducedSprite,tempSO.typeOfSoldierName);
                Debug.Log("VAR   "+GridCreator.Instance.pathfinding.GetNode(x,z).GetBuilding());
                
            }
            
          

        }
    }
    
}
