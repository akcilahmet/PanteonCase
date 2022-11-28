using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Utils;
using UnityEngine.EventSystems;

public class GridBuilding : MonoBehaviour
{
    public BuildingSO buildingSo;
    private GridCreator GridCreator;

    private void Start()
    {
        GridCreator = GetComponent<GridCreator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            
            Vector3 mousePosition = UtilsMethod.GetMouseWorldPosition();
            GridCreator.pathfinding.GetGrid().GetXY(mousePosition, out int x, out int z);
            Vector3 placedObjectWorldPosition =   GridCreator.pathfinding.GetGrid().GetWorldPosition(x, z);//build objenin inşa edilecek tıklama konumu

            List<Vector2Int> buildObjectgridPosList= buildingSo.GetGridPositionList(new Vector2Int(x, z), BuildingSO.Dir.Down);//build objenin kaplayacağı alanlar listesi

            bool canBuild = true;
            foreach (var VARIABLE in buildObjectgridPosList)
            {
                if ( !GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y).CanBuild())
                {
                    //cannot build here
                    canBuild = false;
                    break;
                }
            }
            
            if (canBuild)
            {
                var build=Instantiate(buildingSo.prefab,placedObjectWorldPosition,quaternion.identity);
                
                foreach (var VARIABLE in buildObjectgridPosList)
                {
                    GridCreator.pathfinding.GetNode(VARIABLE.x, VARIABLE.y).SetBuilding(build.transform);
                }
                //GridCreator.pathfinding.GetNode(x, z).SetBuilding(build.transform);
            }
            else
            {
                Debug.Log("build varr");
            }
           
           
        }
    }

    
 
}
