using System.Collections;
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
  
    public Pathfinding pathfinding;
  
    #region Singleton

    public static GridCreator Instance { get; private set; }
    
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
    private void Start() {
        pathfinding = new Pathfinding(width, height,cellSize,createdGridStartPos.position);
        
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
        
    }

    
  




   
}
