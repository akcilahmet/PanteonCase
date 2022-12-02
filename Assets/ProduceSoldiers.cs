using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class ProduceSoldiers : MonoBehaviour
{
    [SerializeField] private bool readyProduSoldier;
    [SerializeField] private float time = 5f;
     private float firsTime ;
    public GameObject soldierType;
    private bool soldierProduceTimerStart;
    public bool SoldierProduceTimerStart
    {
        get { return soldierProduceTimerStart; }
        set { soldierProduceTimerStart = value;}
    }

    private void Start()
    {
        firsTime = time;
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
        Vector3 createdPos = Pathfinding.Instance.GetNode(4, 5)
            .GetWorldPosition( Pathfinding.Instance.grid.GetCellSize(), Vector3.zero);
        var soldier = Instantiate(soldierType.gameObject, createdPos
            , Quaternion.identity);
        GridCreator.Instance.pathfinding.GetNode(4,5).SetCharacter(soldier.gameObject,BuildingManager.Instance.GetActiveOldBuildingSo());
        Debug.Log("list   " +  GridCreator.Instance.pathfinding.GetNode(4, 5).characterList.Count);
        soldierProduceTimerStart = true;

    }
}
