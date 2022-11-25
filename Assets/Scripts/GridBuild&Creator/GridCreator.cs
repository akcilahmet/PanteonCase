﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class GridCreator : MonoBehaviour
{
    
    public int width;
    public int height;
    public int cellSize;
    public Transform createdGridStartPos;
   
    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    public Pathfinding pathfinding;
  
    private void Start() {
        pathfinding = new Pathfinding(width, height,cellSize,createdGridStartPos.position);
        
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }

    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsMethod.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            characterPathfinding.SetTargetPosition(mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsMethod.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
    }




   
}
