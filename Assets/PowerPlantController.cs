using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerPlantController : MonoBehaviour
{
    private int activeBarrackRandomIndex;
    private BuildingManager buildingManager;
    private void Start()
    {
        buildingManager=BuildingManager.Instance;

        activeBarrackRandomIndex = Random.Range(0, buildingManager.GetBarrackListCount() - 1);
        buildingManager.GetBarrackProduceSoldiersList()[activeBarrackRandomIndex].SoldierProduceTimerStart = true;
        buildingManager.RemoveBarrackProduceSoldiersList( buildingManager.GetBarrackProduceSoldiersList()[activeBarrackRandomIndex]);
    }
}
