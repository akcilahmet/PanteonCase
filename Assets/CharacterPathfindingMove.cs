using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.EventSystems;

public class CharacterPathfindingMove : MonoBehaviour
{
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    private GridCreator gridCreator;

    public bool soldierUret;
    private void Start()
    {
        gridCreator = GetComponent<GridCreator>();
        
      
    }

    private void Update() {
        /*if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject()) {
            Vector3 mouseWorldPosition = UtilsMethod.GetMouseWorldPosition();
            gridCreator.pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
           
            
            gridCreator.pathfinding.GetNode(x,y).SetCharacter(characterPathfinding.gameObject);
            characterPathfinding.SetTargetPosition(mouseWorldPosition);
            //Debug.Log("VAR222"+ gridCreator.pathfinding.GetNode(x,y).GetCharacter().gameObject.name);;
        }*/

        if (soldierUret)
        {
            /*soldierUret = false;

           /// soldier created method set

            Vector3 createdPos = Pathfinding.Instance.GetNode(4, 5)
                .GetWorldPosition( Pathfinding.Instance.grid.GetCellSize(), Vector3.zero);
            var soldier = Instantiate(characterPathfinding.gameObject, createdPos
                , Quaternion.identity);
            gridCreator.pathfinding.GetNode(4,5).SetCharacter(characterPathfinding.gameObject,BuildingManager.Instance.GetActiveBuildingSo());
            Debug.Log("asdasassa"+gridCreator.pathfinding.GetNode(4,5).GetCharacter().name);*/
            /*Vector3 placedObjectWorldPosition =   gridCreator.pathfinding.GetNode(25,35);
            var soldier = Instantiate(characterPathfinding.gameObject,placedObjectWorldPosition
                , Quaternion.identity);//hatalııı*/
        }
       
    }

   
    
}
