using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrackController : MonoBehaviour
{
    private int activeBarrackRandomIndex;
    private BuildingManager buildingManager;
    private ProduceSoldiers produceSoldiers;

    private void Start()
    {
        buildingManager=BuildingManager.Instance;
        produceSoldiers = GetComponent<ProduceSoldiers>();
    }

    private void LateUpdate()
    {
        FindPowerPlant();
    }

    void FindPowerPlant()
    {
        if (!produceSoldiers.ProduceStart)
        {
            if (buildingManager.GetPowerPlantListCount() > 0)
            {
                int randomPowerPlantIndex =  buildingManager.GetPowerPlantListCount()-1;
                produceSoldiers.ProduceStart = true;
                buildingManager.RemovePowerPlantList(buildingManager.GetPowerPlantList()[randomPowerPlantIndex]);
            }

        }
        
    }
}
