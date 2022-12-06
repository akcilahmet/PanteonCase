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
    [SerializeField] private float firsCreatedTime = 3f;
    private float firsTime ;
    private bool produceStart;
    public bool ProduceStart
    {
        get { return produceStart; }
        set { produceStart = value; }
    }
    private GameObject soldierGameObject;
    private bool soldierProduceTimerStart;

    [Header("SoldierCreatPoint")]
    private int randomGridIndexGet;
    private bool randomGridPointSet;
    private Vector2 randomSoldierCreatedGridPoints;
    public bool SoldierProduceTimerStart
    {
        get { return soldierProduceTimerStart; }
        set { soldierProduceTimerStart = value;}
    }

    private void Start()
    {
        firsTime = time;
        time = firsCreatedTime;
    }

    private void Update()
    {
        if (ProduceStart)
        {
            if (!soldierProduceTimerStart)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    soldierProduceTimerStart = true;
                    time = firsTime;
                    readyProduSoldier = true;
                }
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
        if (!randomGridPointSet)
        {
            randomGridPointSet = true;
            randomGridIndexGet = Random.Range(0, GridCreator.Instance.walkableGridList.Count);
            randomSoldierCreatedGridPoints = GridCreator.Instance.walkableGridList[randomGridIndexGet];
        }
       
        
        Vector3 createdPos = Pathfinding.Instance.GetNode((int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y)
            .GetWorldPositionSoldier( (int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y,Pathfinding.Instance.grid.GetCellSize())+new Vector3(Pathfinding.Instance.grid.GetCellSize(),Pathfinding.Instance.grid.GetCellSize())*.5f;
        
        var soldier = Instantiate(soldierGameObject.gameObject, createdPos
            , Quaternion.identity);
        
        GridCreator.Instance.pathfinding.GetNode((int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y).SetCharacter(soldier.gameObject,BuildingManager.Instance.GetActiveOldBuildingSo());
        GridCreator.Instance.pathfinding.GetNode((int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y)
            .SetIsWalkable(!GridCreator.Instance.pathfinding.GetNode((int)randomSoldierCreatedGridPoints.x, (int)randomSoldierCreatedGridPoints.y).isWalkable);
        soldierProduceTimerStart = false;

    }

    public void SetSoldier(GameObject temp)
    {
        soldierGameObject = temp;
       
    }
}
