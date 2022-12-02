

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

    public List<Transform> characterList=new List<Transform>();
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
    
    

    public void SetCharacter(GameObject newCharacter,BuildingSO buildingSo)
    {
       
        characterList.Add(newCharacter.transform);
        this.buildSo = buildingSo;
        grid.TriggerGridObjectChanged(x, y);
        CharacterPathfindingMove.Instance.soldiers.Add(newCharacter);
    }

    public void ClearCharacter()
    {
        this.characterList.RemoveAt(characterList.Count-1);
        if (characterList.Count <= 0)
        {
            this.buildSo = null;
        }
        
        grid.TriggerGridObjectChanged(x, y);
    }
    public Transform GetCharacter()
    {
        if (this.characterList.Count>0)
        {
            return this.characterList[characterList.Count-1];
        }

        return null;
    }

    public bool CanCharacter()
    {
        return characterList.Count == 0;
    }

    public Vector3 GetWorldPosition(float cellSize,Vector3 originPosition) {
        return new Vector3(x, y) * cellSize + originPosition;
    }
}
