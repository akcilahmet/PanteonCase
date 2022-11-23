using System.Collections.Generic;
using UnityEngine;
using GameAI.PathFinding;



// GameAI.Pathfinding.Node generic class.
public class GridCell : Node<Vector2Int>
{
    // Is this cell walkable?
    public bool IsWalkable { get; set; }
    
    private GridEditor _mGridEditor;
    
    public GridCell(GridEditor gridMap, Vector2Int value)
        : base(value)
    {
        _mGridEditor = gridMap;
        
        IsWalkable = true;
    }

    
    // get the neighbours for this cell
    public override List<Node<Vector2Int>> GetNeighbours()
    {
        return _mGridEditor.GetNeighbourCells(this);
    }


}

