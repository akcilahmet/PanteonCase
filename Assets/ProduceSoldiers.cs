using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
using Random = UnityEngine.Random;

public class ProduceSoldiers : MonoBehaviour
{
    [SerializeField] private bool readyProduSoldier;
   
    [Header("SoldierCreatedTime")]
    [SerializeField] private float time = 5f;
    private float firsTime ;
    
    private GameObject soldierGameObject;
    private bool soldierProduceTimerStart;

    [Header("SoldierCreatPoint")]
    private int randomGridIndexGet;
    private Vector2 randomSoldierCreatedGridPoints;
    public bool SoldierProduceTimerStart
    {
        get { return soldierProduceTimerStart; }
        set { soldierProduceTimerStart = value;}
    }

    private void Start()
    {
        firsTime = time;
        randomGridIndexGet = Random.Range(0, GridCreator.Instance.walkableGridList.Count);
        randomSoldierCreatedGridPoints = GridCreator.Instance.walkableGridList[randomGridIndexGet];
    }

    private void Update()
    {
        if (soldierProduceTimerStart)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                soldierProduceTimerStart = false;
                time = firsTime;
                readyProduSoldier = true;
            }
        }
        if (readyProduSoldier)
        {
            readyProduSoldier = false;
            SoldierProduce();
            
        }
    }

    void SoldierProduce()
    {
        Vector3 createdPos = Pathfinding.Instance.GetNode((int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y)
            .GetWorldPosition( Pathfinding.Instance.grid.GetCellSize(), Vector3.zero);
        
        var soldier = Instantiate(soldierGameObject.gameObject, createdPos
            , Quaternion.identity);
        
        GridCreator.Instance.pathfinding.GetNode((int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y).SetCharacter(soldier.gameObject,BuildingManager.Instance.GetActiveOldBuildingSo());
        GridCreator.Instance.pathfinding.GetNode((int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y)
            .SetIsWalkable(!GridCreator.Instance.pathfinding.GetNode((int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y).isWalkable);
        soldierProduceTimerStart = true;

    }

    public void SetSoldier(GameObject temp)
    {
        soldierGameObject = temp;
       
    }
}
