﻿

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
        return this.buildSo;
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

}
