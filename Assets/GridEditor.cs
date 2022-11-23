using System;
using System.Collections;
using System.Collections.Generic;
using GameAI.PathFinding;
using UnityEngine;

public class GridEditor : MonoBehaviour
{
  public int mX;//max number columns
  public int mY;//max number rows
  
  
  [Header("GridCreate")][Space(10)]
  [SerializeField] GameObject RectGridCell_Prefab;
  GameObject[,] GridCellGameObjects;
  protected Vector2Int[,] mIndices;

  // the 2d array of the RectGridCell.
  protected GridCell[,] mRectGridCells;

 [Header("grid cell walkable & non-walkable")]
  public Color COLOR_WALKABLE = new Color(42/255.0f, 99/255.0f, 164/255.0f, 1.0f);
  public Color COLOR_NON_WALKABLE = new Color(0.0f, 0.0f, 0.0f, 1.0f);

  //public NpcMovement NpcMovement;
 // public Transform Destination;
  
  
  private void Start()
  {
    // Constryct the grid and the cell game objects.
    Construct(mX, mY);

    // Reset the camera to a proper size and position.
    ResetCamera();
  }
  
  // Construct a grid with the max cols and rows.
  protected void Construct(int numX, int numY)
  {
    mX = numX;
    mY = numY;

    mIndices = new Vector2Int[mX, mY];
    GridCellGameObjects = new GameObject[mX, mY];
    mRectGridCells = new GridCell[mX, mY];

    
    for (int i = 0; i < mX; ++i)
    {
      for (int j = 0; j < mY; ++j)
      {
        mIndices[i, j] = new Vector2Int(i, j);
        GridCellGameObjects[i, j] = Instantiate(
          RectGridCell_Prefab,
          new Vector3(i, j, 0.0f),
          Quaternion.identity);

        // Set the parent for the grid cell to this transform.
        GridCellGameObjects[i, j].transform.SetParent(transform);

        // set a name to the instantiated cell.
        GridCellGameObjects[i, j].name = "grid" + i + "_" + j;

        // create the RectGridCells
        mRectGridCells[i, j] = new GridCell(this, mIndices[i, j]);

        
        GridProperties gridProperties =
          GridCellGameObjects[i, j].GetComponent<GridProperties>();
        if (gridProperties != null)
        {
          gridProperties.GridCell = mRectGridCells[i, j];
        }
      }
    }
  }

  
  void ResetCamera()//camera adjust according to grid cell settings
  {
    Camera.main.orthographicSize = mY / 2.0f ;
    Camera.main.transform.position = new Vector3(mX / 2.0f - 0.5f, mY / 2.0f - 0.5f, -100.0f);
  }

  

  // get neighbour cells for a given cell.
  #region GetNeighbourCell

  public List<Node<Vector2Int>> GetNeighbourCells(Node<Vector2Int> loc)
  {
    List<Node<Vector2Int>> neighbours = new List<Node<Vector2Int>>();

    int x = loc.Value.x;
    int y = loc.Value.y;

    // Check up.
    if (y < mY - 1)
    {
      int i = x;
      int j = y + 1;

      if (mRectGridCells[i, j].IsWalkable)
      {
        neighbours.Add(mRectGridCells[i, j]);
      }
    }
    // Check top-right
    if (y < mY - 1 && x < mX - 1)
    {
      int i = x + 1;
      int j = y + 1;

      if (mRectGridCells[i, j].IsWalkable)
      {
        neighbours.Add(mRectGridCells[i, j]);
      }
    }
    // Check right
    if (x < mX - 1)
    {
      int i = x + 1;
      int j = y;

      if (mRectGridCells[i, j].IsWalkable)
      {
        neighbours.Add(mRectGridCells[i, j]);
      }
    }
    // Check right-down
    if (x < mX - 1 && y > 0)
    {
      int i = x + 1;
      int j = y - 1;

      if (mRectGridCells[i, j].IsWalkable)
      {
        neighbours.Add(mRectGridCells[i, j]);
      }
    }
    // Check down
    if (y > 0)
    {
      int i = x;
      int j = y - 1;

      if (mRectGridCells[i, j].IsWalkable)
      {
        neighbours.Add(mRectGridCells[i, j]);
      }
    }
    // Check down-left
    if (y > 0 && x > 0)
    {
      int i = x - 1;
      int j = y - 1;

      if (mRectGridCells[i, j].IsWalkable)
      {
        neighbours.Add(mRectGridCells[i, j]);
      }
    }
    // Check left
    if (x > 0)
    {
      int i = x - 1;
      int j = y;

      Vector2Int v = mIndices[i, j];

      if (mRectGridCells[i, j].IsWalkable)
      {
        neighbours.Add(mRectGridCells[i, j]);
      }
    }
    // Check left-top
    if (x > 0 && y < mY - 1)
    {
      int i = x - 1;
      int j = y + 1;

      if (mRectGridCells[i, j].IsWalkable)
      {
        neighbours.Add(mRectGridCells[i, j]);
      }
    }

    return neighbours;
  }

  #endregion
  
  #region Grid walkable-nonwalkable
  
  public void RayCastAndToggleWalkable()
  {
    Vector2 rayPos = new Vector2(
      Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
      Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

    if (hit)
    {
      GameObject obj = hit.transform.gameObject;
      GridProperties sc = obj.GetComponent<GridProperties>();
      ToggleWalkable(sc);
    }
  }

  public void ToggleWalkable(GridProperties sc)
  {
    if (sc == null)
      return;

    int x = sc.GridCell.Value.x;
    int y = sc.GridCell.Value.y;

    sc.GridCell.IsWalkable = !sc.GridCell.IsWalkable;

    if (sc.GridCell.IsWalkable)
    {
      sc.SetInnerColor(COLOR_WALKABLE);
    }
    else
    {
      sc.SetInnerColor(COLOR_NON_WALKABLE);
    }
  }


  

  #endregion
  
 
  
  
  //
  public static float GetManhattanCost(
    Vector2Int a, 
    Vector2Int b)
  {
    return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
  }

  public static float GetEuclideanCost(
    Vector2Int a, 
    Vector2Int b)
  {
    return GetCostBetweenTwoCells(a, b);
  }

  public static float GetCostBetweenTwoCells(
    Vector2Int a, 
    Vector2Int b)
  {
    return Mathf.Sqrt(
      (a.x - b.x) * (a.x - b.x) +
      (a.y - b.y) * (a.y - b.y)
    );
  }

  public GridCell GetRectGridCell(int x, int y)
  {
    if(x >= 0 && x < mX && y >=0 && y < mY)
    {
      return mRectGridCells[x, y];
    }
    return null;
  }
}
