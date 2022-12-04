using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildData",menuName = "Build")]
public class BuildingSO : ScriptableObject
{
    public string name;
    public Sprite uıImage;
    public string typeOfSoldierName;
    public GameObject ınformationSoldierObj;
    public Transform soldierPrefab;
    public Transform buildingObj;
    public Transform visual;
    public int width;
    public int height;
    public Type type;
    public enum Type
    {
        barrack,
        powerPlant
    }
    
    
    public enum Dir
    {
        Down,
        Left,
        Up,
        Right
    }

    public List<Vector2Int> GetGridPositionList(Vector2Int offSet, Dir dir)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir)
        {
            default:
                case Dir.Down:
                case Dir.Up:
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            gridPositionList.Add(offSet+new Vector2Int(x,y));
                        }
                    }
                    break;
                case Dir.Left:
                case Dir.Right:
                    for (int x = 0; x < height; x++)
                    {
                        for (int y = 0; y < width; y++)
                        {
                            gridPositionList.Add(offSet+new Vector2Int(x,y));
                        }
                    }
                    break;
        }

        return gridPositionList;
    }
}
