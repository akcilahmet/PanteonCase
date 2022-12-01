

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public Transform build;
    public BuildingSO buildSo;
    public PathNode cameFromNode;

    public GameObject character;
    public PathNode(Grid<PathNode> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
        build = null;
    }

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }  
    public void SetBuilding(Transform transform,BuildingSO buildingSo) {
        this.build = transform;
        this.buildSo = buildingSo;
       
        grid.TriggerGridObjectChanged(x, y);
    }

    public BuildingSO GetBuilding()
    {
        if (this.buildSo != null)
        {
            return this.buildSo;
        }

        return null;
    }
    
    public bool CanBuild()
    {
        return build == null;
    }
    
    public void ClearBuild()
    {
        build = null;
        grid.TriggerGridObjectChanged(x, y);
    }
    
    public override string ToString() {
        return x + "," + y;
    }
    
    

    public void SetCharacter(GameObject charecter,BuildingSO buildingSo)
    {
        this.character = charecter;
        this.buildSo = buildingSo;
        grid.TriggerGridObjectChanged(x, y);
    }
    public GameObject GetCharacter()
    {
        if (this.character != null)
        {
            return this.character;
        }

        return null;
    }

    public bool CanCharacter()
    {
        return character == null;
    }

    public Vector3 GetWorldPosition(float cellSize,Vector3 originPosition) {
        return new Vector3(x, y) * cellSize + originPosition;
    }
}
